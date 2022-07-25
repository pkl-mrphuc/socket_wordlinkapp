using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLinkApp.Model;

namespace WordLinkApp.DL
{
    public class RoomGameBussinessDL : BaseDL
    {
        public RoomGameBussinessDL(string connectionString) : base(connectionString)
        {

        }

        public bool CreateRoomGame(RoomGame roomGame)
        {
            bool result = false;
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                string sql = "insert into [RoomGame](RoomGameId, RoomName, Quantity, IsActive, BossId) values(@roomGameId, @roomName, @quantity, @isActive, @bossId)";
                _sqlConnection.Open();
                result = _sqlConnection.Execute(sql, new { roomGame.RoomGameId, roomGame.RoomName, roomGame.Quantity, roomGame.IsActive, roomGame.BossId }) > 0;
                _sqlConnection.Close();
            }
            return result;
        }

        public bool AddUserToRoom(Guid roomGameId, User user, int sortOrder)
        {
            bool result = false;
            Guid userGameId = Guid.NewGuid();
            string displayNameUser = user.UserName;
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                string sql = "insert into [UserGame](UserGameId, UserId, RoomGameId, DisplayNameUser, SortOrder) values(@userGameId, @userName, @roomGameId, @displayNameUser, @sortOrder)";
                string sqlUpdateQuantity = "update [RoomGame] set  Quantity = Quantity + 1 where RoomGameId = @roomGameId";
                _sqlConnection.Open();
                result = _sqlConnection.Execute(sql, new { userGameId, user.UserName, roomGameId, displayNameUser, sortOrder }) > 0;
                if (result)
                {
                    result = _sqlConnection.Execute(sqlUpdateQuantity, new { roomGameId }) > 0;
                }
                _sqlConnection.Close();
            }
            return result;
        }

        public List<UserGame> GetUsersInRoom(Guid roomGameId)
        {
            List<UserGame> result = new List<UserGame>();
            string sql = "select * from [UserGame] where RoomGameId = @roomGameId order by SortOrder";
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                result = _sqlConnection.Query<UserGame>(sql, new { roomGameId }).ToList();
                _sqlConnection.Close();
            }
            return result;
        }

        public List<RoomGame> GetRoomGamesIsActive()
        {
            List<RoomGame> result = new List<RoomGame>();
            string sql = "select * from [RoomGame] where IsActive = 1";
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                result = _sqlConnection.Query<RoomGame>(sql).ToList();
                _sqlConnection.Close();
            }
            return result;
        }

        public bool DeleteRoomGame(Guid roomGameId)
        {
            bool result = false;
            string sql = "delete from [RoomGame] where RoomGameId = @roomGameId";
            string sqlRemoveUserGame = "delete from [UserGame] where RoomGameId = @roomGameId";
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                result = _sqlConnection.Execute(sql, new { roomGameId }) > 0;
                if (result)
                {
                    result = _sqlConnection.Execute(sqlRemoveUserGame, new { roomGameId }) > 0;
                }
                _sqlConnection.Close();
            }
            return result;
        }

        public bool RemoveUserFromRoomGame(Guid roomGameId, string userId)
        {
            bool result = false;
            List<UserGame> usersInGame = GetUsersInRoom(roomGameId);
            usersInGame.RemoveAt(usersInGame.FindIndex(item => item.UserId == userId));
            string sql = "update [RoomGame] set Quantity = Quantity - 1, BossId = @userId where RoomGameId = @roomGameId";
            string sqlUpdateSortOrder = "update [UserGame] set SortOrder = @sortOrder where UserGameId = @userGameId";
            string sqlRemoveUserGame = "delete from [UserGame] where RoomGameId = @roomGameId and UserId = @userId";
            using (_sqlConnection = new SqlConnection(_connectionString))
            {
                _sqlConnection.Open();
                result = _sqlConnection.Execute(sqlRemoveUserGame, new { roomGameId, userId }) > 0;
                if (result)
                {
                    result = _sqlConnection.Execute(sql, new { usersInGame[0].UserId, roomGameId }) > 0;
                    if (result)
                    {
                        for (int i = 0; i < usersInGame.Count; i++)
                        {
                            int sortOrder = i + 1;
                            result = _sqlConnection.Execute(sqlUpdateSortOrder, new { sortOrder, usersInGame[i].UserGameId }) > 0;
                        }
                    }
                }
                _sqlConnection.Close();
            }
            return result;
        }
    }
}
