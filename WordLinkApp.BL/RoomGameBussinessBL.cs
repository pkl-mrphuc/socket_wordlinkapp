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
    public class RoomGameBussinessBL : BaseBL
    {
        private RoomGameBussinessDL _roomGameDL;
        public RoomGameBussinessBL(string connectionString) : base(connectionString)
        {
            _roomGameDL = new RoomGameBussinessDL(connectionString);
        }

        public ServiceResult CreateRoomGame(RoomGame roomGame, User user)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                bool isCreateRoomSuccess = _roomGameDL.CreateRoomGame(roomGame);
                if (!isCreateRoomSuccess)
                {
                    result.Success = false;
                    result.Message = "Tạo phòng không thành công";
                }
                else
                {
                    bool addUserToRoomSuccess = _roomGameDL.AddUserToRoom(roomGame.RoomGameId, user, 1);
                    if (!addUserToRoomSuccess) 
                    {
                        result.Success = false;
                        result.Message = "Thêm người dùng vào phòng không thành công";
                    }
                }
            }
            catch (Exception ex)
            {
                CommonFunction.HandleException(ref result, "CreateRoomGame", ex);
            }
            return result;
        }

        public ServiceResult GetUsersInRoomGame(Guid roomGameId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                List<UserGame> users = _roomGameDL.GetUsersInRoom(roomGameId);
                if(users.Count() > 0)
                {
                    result.Data = JsonConvert.SerializeObject(users);
                }
            }
            catch (Exception ex)
            {
                CommonFunction.HandleException(ref result, "GetUsersInRoomGame", ex);
            }
            return result;
        }

        public ServiceResult GetRoomGamesIsActive()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                List<RoomGame> roomGames = _roomGameDL.GetRoomGamesIsActive();
                if (roomGames.Count() > 0)
                {
                    result.Data = JsonConvert.SerializeObject(roomGames);
                }
            }
            catch (Exception ex)
            {
                CommonFunction.HandleException(ref result, "GetRoomGamesIsActive", ex);
            }
            return result;
        }

        public ServiceResult DeleteRoomGame(Guid roomGameId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                bool isRemoveRoomGame = _roomGameDL.DeleteRoomGame(roomGameId);
                if (!isRemoveRoomGame)
                {
                    result.Success = false;
                    result.Message = "Xóa phòng không thành công";
                }
            }
            catch (Exception ex)
            {
                CommonFunction.HandleException(ref result, "DeleteRoomGame", ex);
            }
            return result;
        }

        public ServiceResult AddUserToRoomGame(UserGame userGame)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                bool isSuccess = _roomGameDL.AddUserToRoom(userGame.RoomGameId, new User() { UserName = userGame.UserId }, userGame.SortOrder);
                if (!isSuccess)
                {
                    result.Success = false;
                    result.Message = "Thêm người chơi không thành công";
                }
            }
            catch (Exception ex)
            {
                CommonFunction.HandleException(ref result, "AddUserToRoomGame", ex);
            }
            return result;
        }

        public ServiceResult RemoveUserFromRoomGame(Guid roomGameId, string userId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                bool isSuccess = _roomGameDL.RemoveUserFromRoomGame(roomGameId, userId);
                if (!isSuccess)
                {
                    result.Success = false;
                    result.Message = "Rời phòng không thành công";
                }
            }
            catch (Exception ex)
            {
                CommonFunction.HandleException(ref result, "RemoveUserFromRoomGame", ex);
            }
            return result;
        }
    }
}
