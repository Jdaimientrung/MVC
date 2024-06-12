using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Models.Dao
{
    public class UserDao
    {
        ShopOnlineDbContext db = null; 
        public UserDao()
        {
            db = new ShopOnlineDbContext();
        }
        public long Insert(User emtity)
        {
            db.Users.Add(emtity);
            db.SaveChanges();
            return emtity.UserID;
        }
        public User GetById(string userName) 
        {
                return db.Users.SingleOrDefault(u => u.UserName == userName);
        }
        public bool Login(string userName, string password) 
        {
            var result = db.Users.Count(x=>x.UserName == userName && x.Password == password);
            if (result > 0) 
            {
                return true;

            }
            return false;
        }

    }
}
