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
        public int Login(string userName, string password) 
        {
            var result = db.Users.SingleOrDefault(x=>x.UserName == userName);
            if (result == null) 
            {
                return 0;

            }
            else
            {
                if (result.Status == false)
                {
                    return -1;
                }
                else
                {
                    if (result.Password == password)
                    {
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
                
            }
            
        }

    }
}
