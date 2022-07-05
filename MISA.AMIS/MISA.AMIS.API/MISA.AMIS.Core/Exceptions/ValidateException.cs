using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.Core.Exceptions
{
    public class ValidateException: Exception
    {
        public string ErrorMsg { get; set; }
        public string ErrorCode { get; set; }

        public ValidateException(string errorMsg, string errorCode)
        {
            ErrorMsg = errorMsg;
            ErrorCode = errorCode;
        }

        public override string Message => this.ErrorMsg;
        
    }
}
