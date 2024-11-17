using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntertainmentApp.Models
{
    public class UserSession
    {
        public string UserName { get; set; }
        public string SessionId { get; set; }
        public string UserType { get; set; }
    }

    public class Response
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public UserSession CurrentSession { get; set; }
    }
}