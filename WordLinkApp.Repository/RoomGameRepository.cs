using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLinkApp.BL;
using WordLinkApp.Model;

namespace WordLinkApp.Repository
{
    public class RoomGameRepository : IRoomGameRepository
    {
        private RoomGameBussinessBL _roomGameBL;
        public RoomGameRepository(string connectionString)
        {
            _roomGameBL = new RoomGameBussinessBL(connectionString);
        }
        public ServiceResult CreateRoomGame(RoomGame roomGame, User user)
        {
            return _roomGameBL.CreateRoomGame(roomGame, user);
        }

        public ServiceResult GetUsersInRoomGame(Guid roomGameId)
        {
            return _roomGameBL.GetUsersInRoomGame(roomGameId);
        }

        public ServiceResult GetRoomGamesIsActive()
        {
            return _roomGameBL.GetRoomGamesIsActive();
        }

        public ServiceResult DeleteRoomGame(Guid roomGameId)
        {
            return _roomGameBL.DeleteRoomGame(roomGameId);
        }

        public ServiceResult AddUserToRoomGame(UserGame userGame)
        {
            return _roomGameBL.AddUserToRoomGame(userGame);
        }
        public ServiceResult RemoveUserFromRoomGame(Guid roomGameId, string userId)
        {
            return _roomGameBL.RemoveUserFromRoomGame(roomGameId, userId);
        }
    }
}
