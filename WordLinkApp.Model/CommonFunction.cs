using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLinkApp.Model
{
    public static class CommonFunction
    {
        public static void HandleException(ref ServiceResult serviceResult, string funcName, Exception exception)
        {
            serviceResult.Success = false;
            serviceResult.Message = $"Exception {funcName}: {exception.Message}";
            serviceResult.ErrorCode = "Exception";
        }
    }
}
