using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSirKarl.Models
{



    public class CredentialsModel
    {
        public string Server { get; set; }
        public string ServerType { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

    }
}