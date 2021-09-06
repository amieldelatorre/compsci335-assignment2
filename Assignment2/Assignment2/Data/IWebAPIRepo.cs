using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Data
{
    public interface IWebAPIRepo
    {
        public bool ValidLogin(string userName, string password);
        public User GetUserByUserName(string userName);
        public string RegisterUser(User user);
    }
}
