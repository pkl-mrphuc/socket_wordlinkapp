using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLinkApp.Model
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public object OtherData { get; set; }

        public ServiceResult()
        {
            Success = true;
            ErrorCode = null;
            Message = null;
            Data = null;
            OtherData = null;
        }
    }
}
