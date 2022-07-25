using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLinkApp.Bussiness
{
    public class Enumeration
    {
    }

    public enum BussinessGame
    {
        JOIN_GAME = 100,
        CREATE_GAME = 101,
        REMOVE_GAME = 102,
        ACCEPT_JOIN = 103,
        REFRESH_ROOM = 104,
        REFRESH_HOME = 105,
        SET_BOSS = 106,
        TURN_USER = 107,
        SEND_ANSWER = 108,
        END_GAME = 109
    }
}
