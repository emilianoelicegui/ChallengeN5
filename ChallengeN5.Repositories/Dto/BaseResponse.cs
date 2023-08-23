using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeN5.Repositories.Dto
{
    public class BaseResponse
    {
        public BaseResponse(string _message)
        {
            Message = _message;
        }

        public BaseResponse(string _message, object _result)
        {
            Message = _message;
            Result = _result;
        }

        public string Message { get; set; } = null!;
        public object Result { get; set; } = null!;
    }
}
