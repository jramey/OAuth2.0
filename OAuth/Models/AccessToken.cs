using System;

namespace OAuth.Models
{
    public class AccessToken
    {
        public String Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public String User { get; set; }

        public AccessToken(String token, String user, DateTime expirationDate)
        {
            this.Token = token;
            this.User = user;
            this.ExpirationDate = expirationDate;
        }
    }
}