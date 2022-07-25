using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLinkApp.DL;
using WordLinkApp.Model;

namespace WordLinkApp.BL
{
    public class UserBussinessBL : BaseBL
    {
        private UserBussinessDL _userDL;
        public UserBussinessBL(string connectionString) : base(connectionString)
        {
            _userDL = new UserBussinessDL(connectionString);
        }

        public ServiceResult Login(User user)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                bool isExistUserName = _userDL.CheckExistUserName(user.UserName);
                if (isExistUserName)
                {
                    user = _userDL.Login(user);
                    if(user != null && !user.IsActive)
                    {
                        _userDL.UpdateActiveStatusUser(user.UserId, UserStatus.Active);
                        user.IsActive = true;
                        result.Data = JsonConvert.SerializeObject(user);
                        result.Message = "Đăng nhập thành công";
                        return result;
                    }
                }
                if (user != null && user.IsActive)
                {
                    result.Success = false;
                    result.ErrorCode = UserErrorCode.NotFoundUserName.ToString();
                    result.Message = "Người dùng đang hoạt động. Vui lòng kiểm tra lại!";
                }
                else
                {
                    result.Success = false;
                    result.ErrorCode = UserErrorCode.NotFoundUserName.ToString();
                    result.Message = "Thông tin tài khoản, mật khẩu không chính xác";
                }
            }
            catch (Exception ex)
            {
                CommonFunction.HandleException(ref result, "Login", ex);
            }
            return result;
        }

        public ServiceResult Logout(Guid userId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _userDL.UpdateActiveStatusUser(userId, UserStatus.InActive);
            }
            catch (Exception ex)
            {
                CommonFunction.HandleException(ref result, "Logout", ex);
            }
            return result;
        }
    }
}
