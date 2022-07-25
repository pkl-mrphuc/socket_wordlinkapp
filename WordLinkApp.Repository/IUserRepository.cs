using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLinkApp.Model;

namespace WordLinkApp.Repository
{
    public interface IUserRepository
    {
        public ServiceResult Login(User user);

        
    }
}
