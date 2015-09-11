using System;
using System.Collections.Generic;
using System.Linq;
using OAuth.Models;

namespace OAuth
{
    public class Users
    {
        private static List<User> UsersCollection = new List<User> { new User("jramey", "password") };
        private static List<UserInformation> UserInformation = new List<UserInformation> { new UserInformation("jramey", "jramey@email.com", "Jordan Ramey") };

        public static Boolean Authenticate(String loginName, String password)
        {
            return UsersCollection.Any(u => u.LoginName == loginName && u.Password == password);
        }

        public static UserInformation GetUserInformation(String loginName)
        {
            return UserInformation.FirstOrDefault(u => u.LoginName == loginName);
        }
    }
}