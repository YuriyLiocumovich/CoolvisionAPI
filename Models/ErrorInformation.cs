
using System;
using System.Net;

namespace CoolvisionAPI.Models
{
    public class ErrorInformation
    {
        public HttpStatusCode HttpStatus { get; set; }
        public string Message { get; set; }
        private DateTime _cretedDate;
        public DateTime ErrorDate { get { return _cretedDate; } }

        public ErrorInformation() { }

        public ErrorInformation(HttpStatusCode httpStatusCode,string message)
        {
            HttpStatus = httpStatusCode;
            Message = message;
            _cretedDate = DateTime.UtcNow;
        }
    }
}