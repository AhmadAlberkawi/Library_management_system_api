using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.Errors
{
    public class ApiException
    {
        public int SatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
  
        public ApiException(int satusCode, string message = null, string details = null)
        {
            SatusCode = satusCode;
            Message = message;
            Details = details;
        }
    }
}
