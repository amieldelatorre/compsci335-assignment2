using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Dtos;
using Assignment2.Models;


namespace Assignment2.Data
{
    public class DBWebAPIRepo : IWebAPIRepo
    {
        public readonly WebAPIDBContext _dbContext;
        public DBWebAPIRepo(WebAPIDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool ValidLogin(string userName, string password)
        {
            User u = _dbContext.Users.FirstOrDefault
                (e => e.UserName == userName && e.Password == password);
            if (u == null)
                return false;
            else
                return true;
        }

        public User GetUserByUserName(string userName)
        {
            User u = _dbContext.Users.FirstOrDefault(e => e.UserName == userName);
            return u;
        }

        public string RegisterUser(User user)
        {
            User existingUser = GetUserByUserName(user.UserName);
            if (existingUser == null)
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return "User successfully registered.";
            }
            else
                return "Username not available.";
        }

        public Order GetOrder(int Id)
        {
            Order order = _dbContext.Orders.FirstOrDefault(e => e.Id == Id);
            return order;
        }

        public Order AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return GetOrder(order.Id);
        }
    }
}
