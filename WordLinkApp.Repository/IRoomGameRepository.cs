using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLinkApp.Model;

namespace WordLinkApp.Repository
{
    public interface IRoomGameRepository
    {
        ServiceResult CreateRoomGame(RoomGame roomGame, User user);

        ServiceResult GetUsersInRoomGame(Guid roomGameId);

        ServiceResult DeleteRoomGame(Guid roomGameId);
        ServiceResult AddUserToRoomGame(UserGame userGame);
        ServiceResult RemoveUserFromRoomGame(Guid roomGameId, string userId);

    }
}
