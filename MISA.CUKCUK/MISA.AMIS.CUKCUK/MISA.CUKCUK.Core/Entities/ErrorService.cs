using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class ErrorService
    {
        public string ErrorCode { get; set; }
        public string DevMsg { get; set; }
        public string UserMsg { get; set; }
        public Object Data { get; set; }
    }
}
