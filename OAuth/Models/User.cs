using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth.Models
{
    public class User
    {
        public String LoginName { get; set; }
        public String Password { get; set; }

        public User(String loginName, String password)
        {
            this.LoginName = loginName;
            this.Password = password;
        }
    }
}