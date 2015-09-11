using System;

namespace OAuth.Models
{
    public class UserInformation
    {
        public String LoginName { get; set; }
        public String EmailAddress { get; set; }
        public String FullName { get; set; }

        public UserInformation(String loginName, String emailAdress, String fullName)
        {
            this.LoginName = loginName;
            this.EmailAddress = emailAdress;
            this.FullName = fullName;                
        }
    }
}