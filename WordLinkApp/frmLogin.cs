using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordLinkApp.Bussiness;
using WordLinkApp.Model;
using WordLinkApp.Repository;

namespace WordLinkApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private UserRepository _userRepo;
        private string _CONNECTION_STRING = System.Configuration.ConfigurationManager.ConnectionStrings["WordLinkApp_DB"].ConnectionString;
        private void frmLogin_Load(object sender, EventArgs e)
        {
            _userRepo = new UserRepository(_CONNECTION_STRING);
        }

        /// <summary>
        /// Hành vi sau khi click nút login
        /// 1. Kiểm tra đăng nhập
        /// 2. Mở form trang chủ nếu đăng nhập thành công
        /// 3. Nếu đăng nhập không thành công thông báo lỗi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User()
                {
                    UserName = inpUsername.Text,
                    Password = inpPassword.Text
                };
                HandleLoginBussiness(user);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Xử lý nghiệp vụ sau khi bấm nút đăng nhập
        /// </summary>
        /// <param name="userInfo">Thông tin người dùng</param>
        private void HandleLoginBussiness(User userInfo)
        {
            ServiceResult result = _userRepo.Login(userInfo);
            MessageBox.Show(result.Message);
            if (result.Success && result.Data != null)
            {
                // thông tin user sau khi lấy từ db 
                userInfo = JsonConvert.DeserializeObject<User>(result.Data.ToString());
                CloseFormLoginAfterLoginSuccess();
                InitFormHomeAfterLoginSuccess(userInfo);
            }
        }

        /// <summary>
        /// Đóng form login
        /// </summary>
        private void CloseFormLoginAfterLoginSuccess()
        {
            this.Visible = false;
        }

        /// <summary>
        /// Khởi tạo form trang chủ sau khi login thành công
        /// </summary>
        /// <param name="user"></param>
        private void InitFormHomeAfterLoginSuccess(User user)
        {
            SocketClient socket = new SocketClient(user, this);
            frmHome home = new frmHome(user, socket);
            socket._currentView = home;
            OpenFormHomeAfterInitSuccess(home);
        }

        /// <summary>
        /// Mở form sau khi khởi tạo thành công
        /// </summary>
        /// <param name="form"></param>
        private void OpenFormHomeAfterInitSuccess(Form form)
        {
            form.Visible = true;
        }
    }
}
