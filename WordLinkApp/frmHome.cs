using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordLinkApp.Bussiness;
using WordLinkApp.Model;
using WordLinkApp.Repository;
using WordLinkApp.SocketModel;

namespace WordLinkApp
{
    public partial class frmHome : Form
    {
        public frmHome(User user, SocketClient socket)
        {
            InitializeComponent();
            _user = user;
            _socket = socket;
        }

        // kết nối máy chủ
        private SocketClient _socket;
        // thông tin người dùng
        private User _user;
        // nghiệp vụ người dùng
        private UserRepository _userRepo;
        // nghiệp vụ phòng game
        private RoomGameRepository _roomGameRepo;
        // danh sách phòng game đang hoạt động
        private List<RoomGame> _roomGames = new List<RoomGame>();
        private string _connection = System.Configuration.ConfigurationManager.ConnectionStrings["WordLinkApp_DB"].ConnectionString;

        private void frmHome_Load(object sender, EventArgs e)
        {
            _userRepo = new UserRepository(_connection);
            _roomGameRepo = new RoomGameRepository(_connection);
            GetListRoomGamesActiveInDB();
            LoadInfoToControlInLoadingProcess();
        }

        /// <summary>
        /// Lấy danh sách phòng game đang hoạt động trong db
        /// </summary>
        private void GetListRoomGamesActiveInDB()
        {
            ServiceResult getRoomGamesService = _roomGameRepo.GetRoomGamesIsActive();
            if (getRoomGamesService.Success && getRoomGamesService.Data != null)
            {
                _roomGames = JsonConvert.DeserializeObject<List<RoomGame>>(getRoomGamesService.Data.ToString());
            }
        }

        /// <summary>
        /// Hành vi đóng form 
        /// 1. Đăng xuất tài khoản 
        /// 2. Ngắt kết nối socket
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            _socket.StopSocketClient();
            _userRepo.Logout(_user.UserId);
        }

        /// <summary>
        /// Hiển thị thông tin khi load form trang chủ
        /// 1. Hiển thị thông tin người đăng nhập
        /// 2. Load danh sách phòng game đang hoạt động
        /// </summary>
        private void LoadInfoToControlInLoadingProcess()
        {
            LoadUserInfoToControl();
            LoadListRoomGamesActive();
        }

        /// <summary>
        /// Load thông tin người chơi
        /// </summary>
        private void LoadUserInfoToControl()
        {
            lbDisplayName.Text = _user.DisplayName;
        }

        /// <summary>
        /// Load danh sách phòng game
        /// </summary>
        private void LoadListRoomGamesActive()
        {
            lstRoom.View = View.Details;
            lstRoom.Items.Clear();
            for (int i = 0; i < _roomGames.Count; i++)
            {
                lstRoom.Items.Add(new ListViewItem($"{_roomGames[i].RoomName} - {_roomGames[i].Quantity} thành viên"));
            }
        }

        /// <summary>
        /// Hành xử khi đăng xuất tài khoản
        /// 1. Đăng xuất
        /// 2. Ngắt kết nối socket
        /// 3. Đóng form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _userRepo.Logout(_user.UserId);
            _socket.StopSocketClient();
            CloseFormHome();
        }

        /// <summary>
        /// Đóng form khi click đăng xuất
        /// </summary>
        private void CloseFormHome()
        {
            this.Visible = false;
        }

        /// <summary>
        /// Hành xử khi tạo phòng game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            RoomGame roomGame = new RoomGame()
            {
                RoomGameId = Guid.NewGuid(),
                RoomName = $"Phòng {GenerateRoomName()}",
                Quantity = 0,
                IsActive = true,
                BossId = _user.UserName
            };
            HandleCreateRoomGameBussiness(roomGame);
        }

        /// <summary>
        /// Thêm thông tin phòng game vào db
        /// 1. Thêm phòng vào db
        /// 2. Khởi tạo form phòng game
        /// 3. Mở phòng game
        /// </summary>
        /// <param name="roomGame">Thông tin phòng game</param>
        private void HandleCreateRoomGameBussiness(RoomGame roomGame)
        {
            ServiceResult createRoomService = _roomGameRepo.CreateRoomGame(roomGame, _user);
            if (createRoomService.Success)
            {
                CloseFormHome();
                InitFormRoomGameWhenInsertRoomGameSuccess(roomGame);
            }
            else
            {
                MessageBox.Show(createRoomService.Message);
            }
        }

        /// <summary>
        /// Khởi tạo phòng game sau khi thêm db thành công
        /// </summary>
        /// <param name="roomGame">Thông tin phòng game</param>
        private void InitFormRoomGameWhenInsertRoomGameSuccess(RoomGame roomGame)
        {
            frmRoomGame room = new frmRoomGame(_socket, _user, roomGame, true);
            _socket._currentView = room;
            OpenFormRoomGame(room);
        }

        /// <summary>
        /// Mở phòng game
        /// </summary>
        /// <param name="form">Thông tin form phòng game</param>
        private void OpenFormRoomGame(Form form)
        {
            form.Visible = true;
        }

        /// <summary>
        /// Sinh tên phòng ngẫu nhiên
        /// </summary>
        /// <returns>Tên phòng</returns>
        private string GenerateRoomName()
        {
            return Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value;
        }

        /// <summary>
        /// Hành xử khi chọn nút tham gia game
        /// 1. Lấy thông tin phòng đã chọn
        /// 2. Gửi thông điệp tham gia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            if (lstRoom.SelectedItems.Count > 0)
            {
                RoomGame roomSelected = GetInfoRoomGameAfterChoose();
                if(roomSelected != null)
                {
                    if (!string.IsNullOrEmpty(roomSelected.BossId))
                    {
                        // gửi yêu cầu tham gia game cho chủ phòng
                        SendMessageTo(BussinessGame.JOIN_GAME, roomSelected.BossId, JsonConvert.SerializeObject(roomSelected));
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn cần chọn phòng trên danh sách. Để xin tham gia");
            }
        }

        /// <summary>
        /// Lấy thông tin phòng game muốn tham gia
        /// 1. Kiểm tra danh sách phòng mới nhất trong db
        /// 2. Lấy thông tin phòng đã chọn
        /// </summary>
        /// <returns></returns>
        private RoomGame GetInfoRoomGameAfterChoose()
        {
            int positionRoom = lstRoom.Items.IndexOf(lstRoom.SelectedItems[0]);
            if (lstRoom.Items.Count != _roomGames.Count)
            {
                GetListRoomGamesActiveInDB();
            }
            return _roomGames.Count > 0 && positionRoom >= 0 && positionRoom < _roomGames.Count ? _roomGames[positionRoom] : null;
        }

        /// <summary>
        /// Gửi thông điệp lên server
        /// </summary>
        /// <param name="key">Loại nghiệp vụ game</param>
        /// <param name="receiverName">Tên username người nhận</param>
        /// <param name="messageData">Dữ liệu thông điệp</param>
        private void SendMessageTo(BussinessGame key, string receiverName, string messageData)
        {
            _socket.SendMessage(new MessageData()
            {
                Key = (int)key,
                SenderId = _user.UserName,
                ReceiverId = receiverName,
                Data = messageData
            });
        }
    }
}
