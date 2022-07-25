using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLinkApp.Model
{
    public class RoomGame
    {
        public Guid RoomGameId { get; set; }
        public string RoomName { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
        public string BossId { get; set; }
    }
}
