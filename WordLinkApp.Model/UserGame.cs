using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLinkApp.Model
{
    public class UserGame
    {
        public Guid UserGameId { get; set; }
        public string UserId { get; set; }
        public Guid RoomGameId { get; set; }
        public string DisplayNameUser { get; set; }
        public int SortOrder { get; set; }
    }
}
