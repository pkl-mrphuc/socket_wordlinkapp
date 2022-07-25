using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordLinkApp.Bussiness;
using WordLinkApp.Model;
using WordLinkApp.Repository;
using WordLinkApp.SocketModel;

namespace WordLinkApp
{
    public partial class frmRoomGame : Form
    {
        public frmRoomGame(SocketClient socket, User user, RoomGame roomGame, bool isBoss)
        {
            InitializeComponent();
            _socket = socket;
            _user = user;
            _roomGame = roomGame;
            _isBoss = isBoss;
            _usersInRoom = new List<UserGame>();
        }

        // kết nối server
        private SocketClient _socket;
        // thông tin người dùng
        private User _user;
        // thông tin phòng game
        private RoomGame _roomGame;
        // danh sách người dùng trong phòng game
        private List<UserGame> _usersInRoom;
        // nghiệp vụ phòng game
        private RoomGameRepository _roomGameRepo;
        
        string _connection = System.Configuration.ConfigurationManager.ConnectionStrings["WordLinkApp_DB"].ConnectionString;
        // cờ kiểm tra chủ phòng
        public bool _isBoss;

        /// <summary>
        /// Hành xử khi load form phòng game
        /// 1. Nếu là chủ phòng thì gửi thông điệp cho tất cả người dùng load lại màn hình trang chủ
        /// 2. Lấy danh sách người chơi trong db
        /// 3. Load thông tin phòng + danh sách người chơi
        /// 4. Ẩn hiện nút với từng vai trò người chơi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRoomGame_Load(object sender, EventArgs e)
        {
            _roomGameRepo = new RoomGameRepository(_connection);

            if (_isBoss)
            {
                SendMessageBroadCast(BussinessGame.CREATE_GAME, null);
            }
            GetUsersInRoomGameFromDB();
            LoadRoomGameInfoInLoadingProcess();
            if (_isBoss) LoadViewIsBoss();
            else LoadViewIsGuess();
        }

        /// <summary>
        /// Lấy danh sách người chơi trong phòng game
        /// </summary>
        private void GetUsersInRoomGameFromDB()
        {
            ServiceResult usersInRoomService = _roomGameRepo.GetUsersInRoomGame(_roomGame.RoomGameId);
            if (usersInRoomService.Success && usersInRoomService.Data != null)
            {
                _usersInRoom = JsonConvert.DeserializeObject<List<UserGame>>(usersInRoomService.Data.ToString());
            }
        }

        /// <summary>
        /// Gửi thông điệp cho toàn bộ người dùng đang hoạt động
        /// </summary>
        private void SendMessageBroadCast(BussinessGame key, string messageData)
        {
            _socket.SendBroadcastMessage(new MessageData()
            {
                Key = (int)key,
                SenderId = _user.UserName,
                Data = messageData
            });
        }

        private void btnOutGame_Click(object sender, EventArgs e)
        {
            if (DeleteUserFromRoomGame())
            {
                if (DeleteRoomGame())
                {
                    _socket.SendBroadcastMessage(new MessageData() { 
                        Key = (int)BussinessGame.REFRESH_HOME,
                        SenderId = _user.UserName
                    });

                    for (int i = 0; i < lstUser.Items.Count; i++)
                    {
                        if(lstUser.Items[i].Text != _user.UserName)
                        {
                            _socket.SendMessage(new MessageData()
                            {
                                Key = (int)BussinessGame.REFRESH_ROOM,
                                SenderId = _user.UserName,
                                ReceiverId = lstUser.Items[i].Text,
                                Data = JsonConvert.SerializeObject(_roomGame)
                            });
                        }
                    }

                    this.Visible = false;
                    frmHome home = new frmHome(_user, _socket);
                    _socket._currentView = home;
                    home.Visible = true;
                }
            } 
        }

        /// <summary>
        /// Xóa phòng
        /// </summary>
        /// <returns></returns>
        private bool DeleteRoomGame()
        {
            bool result = false;
            if (lstUser.Items.Count == 1)
            {
                ServiceResult serviceResult = _roomGameRepo.DeleteRoomGame(_roomGame.RoomGameId);
                result = serviceResult.Success;
            }
            else
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Xóa người dùng khỏi phòng
        /// </summary>
        /// <returns></returns>
        private bool DeleteUserFromRoomGame()
        {
            bool result = false;
            if (lstUser.Items.Count >= 1)
            {
                ServiceResult serviceResult = _roomGameRepo.RemoveUserFromRoomGame(_roomGame.RoomGameId, _user.UserName);
                result = serviceResult.Success;
            }
            return result;
        }

        /// <summary>
        /// Hiển thị nút bắt đầu với chủ phòng
        /// </summary>
        private void LoadViewIsBoss()
        {
            btnStarting.Visible = true;
        }

        /// <summary>
        /// Ẩn nút bắt đầu với người chơi
        /// </summary>
        private void LoadViewIsGuess()
        {
            btnStarting.Visible = false;
        }

        /// <summary>
        /// Load thông tin phòng game 
        /// 1. Load thông tin chung: tên phòng, thời gian, nút
        /// 2. Load danh sách người chơi trong phòng
        /// </summary>
        private void LoadRoomGameInfoInLoadingProcess()
        {
            LoadCommonInfoBeforeStartGame();
            LoadUsersInGame();
        }

        /// <summary>
        /// Load danh sách người dùng trong phòng game
        /// </summary>
        private void LoadUsersInGame()
        {
            lstUser.View = View.Details;
            ClearListView(lstUser);
            for (int i = 0; i < _usersInRoom.Count(); i++)
            {
                lstUser.Items.Add(new ListViewItem(_usersInRoom[i].DisplayNameUser));
            }
        }

        /// <summary>
        /// Xóa danh sách trên lstView
        /// </summary>
        /// <param name="lstView">Control lstView</param>
        private void ClearListView(ListView lstView)
        {
            lstView.Items.Clear();
        }

        /// <summary>
        /// Hiển thị thông tin chung trước khi bắt đầu game
        /// </summary>
        private void LoadCommonInfoBeforeStartGame()
        {
            lbNameRoom.Text = _roomGame.RoomName;
            lbTimeCounter.Text = "Thời gian";
            btnSendAnswer.Enabled = false;
        }

        /// <summary>
        /// Hành xử khi chọn nút bắt đầu chơi
        /// 1. Clear danh sách câu trả lại từ game trước
        /// 2. Disabled nút bắt đầu game
        /// 3. Lấy danh sách người chơi trên control lstview
        /// 4. Gửi thông điệp lượt cho người chơi trong phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStarting_Click(object sender, EventArgs e)
        {
            ClearListView(lstAnswer);
            DisabledButtonStartingGame();
            List<KeyValuePair<int, KeyValuePair<string, bool>>> users = GetUsersInRoomFromListView();
            SendMessageToUsersInRoom(BussinessGame.TURN_USER, JsonConvert.SerializeObject(users));
        }

        /// <summary>
        /// Lấy danh sách username trong lstView lstUser
        /// </summary>
        /// <returns></returns>
        private List<KeyValuePair<int, KeyValuePair<string, bool>>> GetUsersInRoomFromListView()
        {
            List<KeyValuePair<int, KeyValuePair<string, bool>>> users = new List<KeyValuePair<int, KeyValuePair<string, bool>>>();
            for (int i = 0; i < lstUser.Items.Count; i++)
            {
                users.Add(new KeyValuePair<int, KeyValuePair<string, bool>>(i, new KeyValuePair<string, bool>(lstUser.Items[i].Text, true)));
            }
            return users;
        }

        /// <summary>
        /// Disabled nút bắt đầu game
        /// </summary>
        private void DisabledButtonStartingGame()
        {
            btnStarting.Enabled = false;
            btnOutGame.Enabled = false;
        }

        /// <summary>
        /// Gửi thông điệp đến người chơi trong phòng
        /// </summary>
        /// <param name="key">Nghiệp vụ game</param>
        /// <param name="data">Dữ liệu thông điệp</param>
        private void SendMessageToUsersInRoom(BussinessGame key, string data)
        {
            for (int i = 0; i < lstUser.Items.Count; i++)
            {
                string receiverId = lstUser.Items[i].Text;
                _socket.SendMessage(new MessageData() { 
                    SenderId = _user.UserName,
                    ReceiverId = receiverId,
                    Data = data,
                    Key = (int)key
                });
            }
        }

        /// <summary>
        /// Hành xử khi gửi kết quả
        /// 1. Gửi thông điệp gửi kết quả cho người chơi trong phòng
        /// 2. Reset ô nhập kết quả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendAnswer_Click(object sender, EventArgs e)
        {
            if (IsValidAnswer())
            {
                SendMessageToUsersInRoom(BussinessGame.SEND_ANSWER, inpAnswer.Text);
                lbBeforeAnswer.Text = inpAnswer.Text;
                inpAnswer.Text = "";
            }
        }

        /// <summary>
        /// Kiểm tra kết quả
        /// 1. Kiểm tra có phải từ có nghĩa không
        /// 2. Kiểm tra từ thứ 1 = đuôi từ trước đó
        /// </summary>
        /// <returns></returns>
        private bool IsValidAnswer()
        {
            string answer = inpAnswer.Text;
            string beforeAnswer = lbBeforeAnswer.Text;
            if (!string.IsNullOrEmpty(answer))
            {
                string[] words = answer.Split(" ");
                if (string.IsNullOrEmpty(beforeAnswer))
                {
                    return true;
                }
                else
                {
                    string[] beforeWords = beforeAnswer.Split(" ");
                    if (words.Count() == 2 && beforeWords[1] == words[0])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
