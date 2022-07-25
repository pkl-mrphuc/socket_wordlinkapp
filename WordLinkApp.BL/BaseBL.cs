using System;
using WordLinkApp.DL;

namespace WordLinkApp.BL
{
    public class BaseBL
    {
        private BaseDL _baseDL;
        public BaseBL(string connectionString)
        {
            _baseDL = new BaseDL(connectionString);
        }
    }
}
