using System;
using WordLinkApp.BL;
using WordLinkApp.Model;

namespace WordLinkApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserBussinessBL _userBL;
        public UserRepository(string connectionString)
        {
            _userBL = new UserBussinessBL(connectionString);
        }

        public ServiceResult Login(User user)
        {
            return _userBL.Login(user);
        }

        public ServiceResult Logout(Guid userId)
        {
            return _userBL.Logout(userId);
        }
    }
}
