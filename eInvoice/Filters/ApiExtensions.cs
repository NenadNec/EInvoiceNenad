using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eInvoice.Filters
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public List<ValidationError> Errors { get; set; }

        public ApiException(string message, int statusCode = 500, List<ValidationError> errors = null) :
            base(message)
        {
            StatusCode = statusCode;
            Errors = errors;
        }
        public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }
}
