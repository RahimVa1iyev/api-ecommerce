using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Exceptions
{
    public class RestException : Exception
    {
        public RestException( HttpStatusCode code , string message)
        {
            Message = message;
            Code = code;
           
        }

        public RestException(HttpStatusCode code,string key,string errorMessage, string message = null )
        {
            Code = code;
            Errors = new List<RestExceptionErrorItems> { new RestExceptionErrorItems(key, errorMessage) };
        }

        public string Message { get; set; }

        public HttpStatusCode Code { get; set; }

        public List<RestExceptionErrorItems> Errors { get; set; }



    }

    public class RestExceptionErrorItems
    {
        public string Key { get; set; }
        public string ErrorMessage { get; set; }

        public RestExceptionErrorItems(string key, string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;
        }
    }
}
