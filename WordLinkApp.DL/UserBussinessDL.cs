using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLinkApp.Model;
using System.Threading;

namespace WordLinkApp.DL
{
    public class UserBussinessDL : BaseDL
    {
        public UserBussinessDL(string connectionString) : base(connectionString)
        {

        }

        public bool CheckExistUserName(string userName)
        {
            bool result = false;
            string sql = "select count(*) from [User] where UserName = @userName";
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                result = _sqlConnection.QueryFirst<int>(sql, new { userName }) > 0;
                _sqlConnection.Close();
            }
            return result;
        }

        public User Login(User user)
        {
            User result = null;
            string sql = "select * from [User] where UserName = @userName and Password = @password";
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                result = _sqlConnection.Query<User>(sql, new { user.UserName, user.Password }).FirstOrDefault();
                _sqlConnection.Close();
            }
            return result;
        }

        public void UpdateActiveStatusUser(Guid userId, UserStatus status)
        {
            Thread thread = new Thread(() => {
                using (_sqlConnection = new SqlConnection(_connectionString))
                {
                    int userStatus = 0;
                    switch (status)
                    {
                        case UserStatus.Active:
                            userStatus = 1;
                            break;
                        case UserStatus.InActive:
                            userStatus = 0;
                            break;
                    }
                    string sql = "update [User] set IsActive = @userStatus where UserId = @userId";
                    _sqlConnection.Open();
                    bool result = _sqlConnection.Execute(sql, new { userStatus, userId }) > 0;
                    _sqlConnection.Close();
                }
            });
            thread.Start();

        }
    }
}
