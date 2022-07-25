using System;

namespace WordLinkApp.Model
{
    public class User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool IsActive { get; set; }
    }
}
