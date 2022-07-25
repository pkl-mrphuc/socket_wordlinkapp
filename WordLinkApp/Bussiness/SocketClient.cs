using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WebSocketSharp;
using WordLinkApp.Model;
using WordLinkApp.Repository;
using WordLinkApp.SocketModel;

namespace WordLinkApp.Bussiness
{
    public class SocketClient
    {
        public WebSocket _socket;
        public WebSocket _socketBroadcast;
        public User _user;
        public Form _currentView;
        public RoomGameRepository _roomGameRepo;
        List<KeyValuePair<int, KeyValuePair<string, bool>>> users;
        Timer tCounter;
        int counter = 20;
        public SocketClient(User user, Form form)
        {
            _user = user;
            _currentView = form;
            _roomGameRepo = new RoomGameRepository(System.Configuration.ConfigurationManager.ConnectionStrings["WordLinkApp_DB"].ConnectionString);
            _socket = new WebSocket($"ws://127.0.0.1:1011/Echo?username={user.UserName}");
            _socketBroadcast = new WebSocket($"ws://127.0.0.1:1011/EchoAll?username={user.UserName}");
            _socket.OnMessage += (sender, e) => {
                Ws_OnMessage(sender, e);
            };
            _socketBroadcast.OnMessage += (sender, e) => {
                Ws_OnMessage(sender, e);
            }; ;
            _socket.Connect();
            _socketBroadcast.Connect();
        }

        /// <summary>
        /// Hành xử khi server trả response về cho client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Data != null)
            {
                MessageData message = JsonConvert.DeserializeObject<MessageData>(e.Data);
                switch (message.Key)
                {
                    case (int)BussinessGame.CREATE_GAME:
                    case (int)BussinessGame.REMOVE_GAME:
                    case (int)BussinessGame.REFRESH_HOME:
                        if (_currentView.Name == "frmHome")
                        {
                            _currentView.Invoke(new Action(() => {
                                LoadListRoomAtHomeView();
                            }));
                        }
                        break;
                    case (int)BussinessGame.JOIN_GAME:
                        if (_currentView.Name == "frmRoomGame")
                        {
                            _currentView.Invoke(new Action(() => {
                                LoadDialogAfterUserSendRequestJoinRoom(message);
                            }));
                        }
                        break;
                    case (int)BussinessGame.ACCEPT_JOIN:
                        if(_currentView.Name == "frmHome")
                        {
                            _currentView.Invoke(new Action(() => {
                                LoadViewAfterUserJoinRoom(message);
                            }));
                        }
                        break;
                    case (int)BussinessGame.REFRESH_ROOM:
                        if(_currentView.Name == "frmRoomGame")
                        {
                            _currentView.Invoke(new Action(() => {
                                LoadListUserAtRoomView(message);
                            }));
                        }
                        break;
                    case (int)BussinessGame.SET_BOSS:
                        if(_currentView.Name == "frmRoomGame")
                        {
                            _currentView.Invoke(new Action(() => {
                                LoadRoomGameViewAfterChangeBoss();
                            }));
                        }
                        break;
                    case (int)BussinessGame.TURN_USER:
                        if(_currentView.Name == "frmRoomGame")
                        {
                            _currentView.Invoke(new Action(() => {
                                HandleChangeTurnGame(message);
                            }));
                        }
                        break;
                    case (int)BussinessGame.SEND_ANSWER:
                        if(_currentView.Name == "frmRoomGame")
                        {
                            _currentView.Invoke(new Action(() => {
                                HandleAfterUserSendAnswer(message);
                            }));
                        }
                        break;
                    case (int)BussinessGame.END_GAME:
                        if(_currentView.Name == "frmRoomGame")
                        {
                            _currentView.Invoke(new Action(() => {
                                HandleAfterEndGame(message);
                            }));
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Hành xử sau khi kết thúc game
        /// 1. Stop đếm thời gian
        /// 2. Thông báo người chiến thắng
        /// </summary>
        /// <param name="message"></param>
        private void HandleAfterEndGame(MessageData message)
        {
            tCounter.Stop();
            // người cuối thắng
            MessageBox.Show($"{message.Data} thắng");
            _currentView.Controls["btnStarting"].Enabled = true;
            _currentView.Controls["btnOutGame"].Enabled = true;
        }

        /// <summary>
        /// Hành xử sau khi người chơi gửi câu trả lời
        /// 1. Hiển thị kết quả trả lời của người gửi
        /// 2. Dừng đếm thời gian
        /// 3. Reset thời gian cho người mới chơi
        /// 4. Lấy tên người chơi tiếp theo
        /// 5. Gửi thông điệp lượt chơi tiếp theo cho người tiếp
        /// </summary>
        /// <param name="message">Thông điệp câu trả lời</param>
        private void HandleAfterUserSendAnswer(MessageData message)
        {
            ListView lstAnswer = _currentView.Controls["lstAnswer"] as ListView;
            ListView lstUser = _currentView.Controls["lstUser"] as ListView;
            Label beforeAnswer = _currentView.Controls["lbBeforeAnswer"] as Label;
            beforeAnswer.Text = message.Data;
            LoadListAnswerAtRoomView(lstAnswer, message.SenderId, message.Data);
            tCounter.Stop();
            ResetTimeCounter();
            if (_user.UserName == message.SenderId)
            {
                string nextUser = GetNameUserNextTurn(message);
                if (!string.IsNullOrEmpty(nextUser)) 
                {
                    for (int i = 0; i < lstUser.Items.Count; i++)
                    {
                        SendMessage(new MessageData()
                        {
                            SenderId = nextUser,
                            ReceiverId = lstUser.Items[i].Text,
                            Key = (int)BussinessGame.TURN_USER,
                            Data = JsonConvert.SerializeObject(users)
                        });
                    }
                }
                
            }
        }

        /// <summary>
        /// Lấy tên người chơi tiếp theo
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string GetNameUserNextTurn(MessageData message) {
            string nextUser = string.Empty;
            int posUserActiveFirst = users.FindIndex(item => item.Value.Value);
            for (int i = 0; i < users.Count; i++)
            {
                int key = users[i].Key;
                KeyValuePair<string, bool> value = users[i].Value;
                if (value.Key == message.SenderId && value.Value)
                {
                    if (i + 1 < users.Count)
                    {
                        value = users[i + 1].Value;
                        nextUser = value.Key;
                        break;
                    }
                    else
                    {
                        nextUser = users[posUserActiveFirst].Value.Key;
                    }
                }
            }
            return nextUser;
        }

        /// <summary>
        /// Hành xử khi chuyển lượt chơi cho người tiếp theo
        /// 1. Đọc danh sách người chơi từ thông điệp 
        /// 2. Kiểm tra có phải lượt chơi của tài khoản hiện tại ko?
        /// 3. Selected lượt người chơi tiếp theo
        /// 4. Run đếm thời gian
        /// </summary>
        private void HandleChangeTurnGame(MessageData message)
        {
            Label lbTimeCounter = _currentView.Controls["lbTimeCounter"] as Label;
            Button btnSendAnswer = _currentView.Controls["btnSendAnswer"] as Button;
            Button btnOutGame = _currentView.Controls["btnOutGame"] as Button;
            ListView lstUser = _currentView.Controls["lstUser"] as ListView;
            tCounter = new Timer();

            users = JsonConvert.DeserializeObject<List<KeyValuePair<int, KeyValuePair<string, bool>>>>(message.Data);
            if(users.Count > 0)
            {
                bool myTurn = message.ReceiverId == message.SenderId ? true : false;
                btnOutGame.Enabled = false;
                ResetTimeCounter();
                ActiveNextUserOnListUser(message.SenderId, lstUser);
                TimeCounterRun(lbTimeCounter, tCounter, btnSendAnswer, myTurn, lstUser, users);
            }
        }

        /// <summary>
        /// Active người chơi tiếp theo trên danh sách người chơi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="lstUser"></param>
        private void ActiveNextUserOnListUser(string sender, ListView lstUser)
        {
            for (int i = 0; i < lstUser.Items.Count; i++)
            {
                if (sender == lstUser.Items[i].Text)
                {
                    lstUser.Items[i].Selected = true;
                }
                else
                {
                    lstUser.Items[i].Selected = false;
                }
            }
        }

        /// <summary>
        /// Reset giá trị đếm thời gian
        /// </summary>
        private void ResetTimeCounter()
        {
            counter = 20;
        }

        /// <summary>
        /// Set giao diện khi chuyển chủ phòng
        /// </summary>
        private void LoadRoomGameViewAfterChangeBoss()
        {
            (_currentView as frmRoomGame)._isBoss = true;
            Button btnStarting = _currentView.Controls["btnStarting"] as Button;
            btnStarting.Visible = true;
            btnStarting.Enabled = true;
        }

        /// <summary>
        /// Load giao diện khi có người dùng tham gia game
        /// </summary>
        /// <param name="message">Thông điệp</param>
        private void LoadViewAfterUserJoinRoom(MessageData message)
        {
            RoomGame roomInfo = HandleAfterBossRoomAcceptJoin(message);
            if (roomInfo != null)
            {
                _currentView.Visible = false;
                _currentView = new frmRoomGame(this, _user, roomInfo, false);
                _currentView.Visible = true;
            }
        }

        /// <summary>
        /// Hành xử sau khi chủ phòng chấp nhận tham gia game
        /// 1. Thêm người dùng vào phòng
        /// 2. Lấy danh sách user trong phòng
        /// 3. Gửi thông điệp load lại màn hình game + màn hình danh sách phòng
        /// </summary>
        /// <param name="message">Thông điệp chấp nhận tham gia game</param>
        private RoomGame HandleAfterBossRoomAcceptJoin(MessageData message)
        {
            RoomGame roomInfo = JsonConvert.DeserializeObject<RoomGame>(message.Data);
            if (roomInfo != null)
            {
                if (AddUserToRoom(roomInfo))
                {
                    List<UserGame> users = GetUsersInRoom(roomInfo);
                    if(users.Count > 0)
                    {
                        for (int i = 0; i < users.Count(); i++)
                        {
                            SendMessage(new MessageData()
                            {
                                Key = (int)BussinessGame.REFRESH_ROOM,
                                SenderId = _user.UserName,
                                ReceiverId = users[i].UserId,
                                Data = message.Data
                            });
                        }
                        SendBroadcastMessage(new MessageData()
                        {
                            Key = (int)BussinessGame.REFRESH_HOME,
                            SenderId = _user.UserName
                        });
                        return roomInfo;
                    }
                    
                }

            }
            return null;
        }

        /// <summary>
        /// Lấy danh sách người chơi trong phòng
        /// </summary>
        /// <param name="roomInfo">Thông tin phòng game</param>
        /// <returns>Danh sách người chơi</returns>
        private List<UserGame> GetUsersInRoom(RoomGame roomInfo)
        {
            ServiceResult getUsersService = _roomGameRepo.GetUsersInRoomGame(roomInfo.RoomGameId);
            if (getUsersService.Success && getUsersService.Data != null)
            {
                return JsonConvert.DeserializeObject<List<UserGame>>(getUsersService.Data.ToString());
            }
            else
            {
                return new List<UserGame>();
            }
        }

        /// <summary>
        /// Thêm người chơi vào phòng insert db
        /// </summary>
        /// <param name="roomInfo">Thông tin phòng</param>
        private bool AddUserToRoom(RoomGame roomInfo)
        {
            ServiceResult addUserToRoomSerivce = _roomGameRepo.AddUserToRoomGame(new UserGame()
            {
                RoomGameId = roomInfo.RoomGameId,
                UserId = _user.UserName,
                DisplayNameUser = _user.UserName,
                SortOrder = roomInfo.Quantity + 1
            });
            return addUserToRoomSerivce.Success;
        }

        /// <summary>
        /// Load form dialog cho chủ phòng khi có người chơi muốn tham gia
        /// </summary>
        /// <param name="message">Thông điệp từ người chơi muốn tham gia</param>
        private void LoadDialogAfterUserSendRequestJoinRoom(MessageData message)
        {
            DialogResult result = MessageBox.Show($"{message.SenderId} muốn tham gia. Bạn có đồng ý không", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                // chủ phòng gửi thông điệp chấp nhận tham gia cho người chơi
                string bossRoomID = message.ReceiverId;
                string joinerRoomID = message.SenderId;
                SendMessage(new MessageData()
                {
                    Key = (int)BussinessGame.ACCEPT_JOIN,
                    SenderId = bossRoomID,
                    ReceiverId = joinerRoomID,
                    Data = message.Data
                });
            }
        }

        private void TimeCounterRun(Label lbTimeCounter, Timer tCounter, Button btnSendAnswer, bool myTurn, ListView users, List<KeyValuePair<int, KeyValuePair<string, bool>>> lstUsers)
        {
            
            if (myTurn) {
                btnSendAnswer.Enabled = true;
            }
            else
            {
                btnSendAnswer.Enabled = false;
            }
            tCounter.Tick += (sender, e) => {
                counter--;
                if (counter == 0)
                {
                    tCounter.Stop();
                    lbTimeCounter.Text = "Hết giờ";
                    btnSendAnswer.Enabled = false;
                    if (myTurn)
                    {
                        string nextUser = string.Empty;
                        bool isEndUser = false;
                        for (int i = 0; i < lstUsers.Count; i++)
                        {
                            int key = lstUsers[i].Key;
                            KeyValuePair<string, bool> value = lstUsers[i].Value;
                            if (value.Key == _user.UserName)
                            {
                                lstUsers[i] = new KeyValuePair<int, KeyValuePair<string, bool>>(i, new KeyValuePair<string, bool>(_user.UserName, false));
                            }
                            else
                            {
                                if (value.Value)
                                {
                                    if(i == lstUsers.Count - 1)
                                    {
                                        isEndUser = true;
                                    }
                                    nextUser = value.Key;
                                    break;
                                }
                            }
                        }
                        if (isEndUser)
                        {
                            for (int i = 0; i < users.Items.Count; i++)
                            {
                                SendMessage(new MessageData()
                                {
                                    Key = (int)BussinessGame.END_GAME,
                                    SenderId = nextUser,
                                    ReceiverId = users.Items[i].Text,
                                    Data = nextUser
                                });
                            } 
                        }
                        else
                        {
                            for (int i = 0; i < users.Items.Count; i++)
                            {
                                SendMessage(new MessageData()
                                {
                                    Key = (int)BussinessGame.TURN_USER,
                                    SenderId = nextUser,
                                    ReceiverId = users.Items[i].Text,
                                    Data = JsonConvert.SerializeObject(lstUsers)
                                });
                            }
                        }
                    }
                    
                }
                else
                {
                    lbTimeCounter.Text = counter.ToString();
                }
            };
            tCounter.Interval = 1000; // 1 second
            tCounter.Start();
            lbTimeCounter.Text = counter.ToString();
        }

        public void LoadListRoomAtHomeView()
        {
            ListView lstRoom = _currentView.Controls["lstRoom"] as ListView;
            lstRoom.Items.Clear();
            ServiceResult getRoomGamesService = _roomGameRepo.GetRoomGamesIsActive();
            if (getRoomGamesService.Success && getRoomGamesService.Data != null)
            {
                List<RoomGame> roomGames = JsonConvert.DeserializeObject<List<RoomGame>>(getRoomGamesService.Data.ToString());
                for (int i = 0; i < roomGames.Count(); i++)
                {
                    lstRoom.Items.Add(new ListViewItem($"{roomGames[i].RoomName} - {roomGames[i].Quantity} thành viên"));
                }
            }
        }

        /// <summary>
        /// Load danh sách người chơi trong phòng 
        /// </summary>
        /// <param name="message"></param>
        public void LoadListUserAtRoomView(MessageData message)
        {
            RoomGame roomInfo = JsonConvert.DeserializeObject<RoomGame>(message.Data);
            if(roomInfo != null)
            {
                ListView lstUser = _currentView.Controls["lstUser"] as ListView;
                lstUser.Items.Clear();
                List<UserGame> users = GetUsersInRoom(roomInfo);
                for (int i = 0; i < users.Count(); i++)
                {
                    lstUser.Items.Add(new ListViewItem($"{users[i].DisplayNameUser}"));
                }
            }
        }

        public void LoadListAnswerAtRoomView(ListView lstAnswer, string sender, string message)
        {
            lstAnswer.View = View.Details;
            ListViewItem mess = new ListViewItem(sender);
            ListViewItem.ListViewSubItem messData = new ListViewItem.ListViewSubItem(mess, message);
            mess.SubItems.Add(messData);
            lstAnswer.Items.Add(mess);
        }

        /// <summary>
        /// Gửi thông điệp cho toàn bộ người chơi đang hoạt động
        /// </summary>
        /// <param name="message">Nội dung thông điệp</param>
        public void SendBroadcastMessage(MessageData message)
        {
            _socketBroadcast.Send(JsonConvert.SerializeObject(message));
        }

        /// <summary>
        /// Gửi thông điệp cho một người
        /// </summary>
        /// <param name="message">Nội dung thông điệp</param>
        public void SendMessage(MessageData message)
        {
            _socket.Send(JsonConvert.SerializeObject(message));
        }

        /// <summary>
        /// Ngắt kết nối Server
        /// </summary>
        public void StopSocketClient()
        {
            _socket.Close();
        }
    }
}
