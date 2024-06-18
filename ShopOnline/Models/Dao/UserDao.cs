using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PagedList;
using System.Web;

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
        public User GetByID(string userName)
        {
            return db.Users.SingleOrDefault(u => u.UserName == userName);
        }
        public User ViewDeltail(int id)
        {
            return db.Users.Find(id);// tìm kiếm theo khóa chính
        }
        public User GetUserByName(string userName)
        {
            return db.Users.SingleOrDefault(u => u.UserName == userName);
        }
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<User> query = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(u => u.UserName.Contains(searchString) || u.Name.Contains(searchString));
            }
            return query.OrderBy(x => x.UserID).ToPagedList(page, pageSize);

            // return db.Users.OrderByDescending(x => x.UserName).ToPagedList(page, pageSize);
        }
        public bool Update(User emtity)
        {
            try
            {
                var user = db.Users.Find(emtity.UserID);
                user.Name = emtity.Name;
                if (!string.IsNullOrEmpty(emtity.Password))
                {
                    user.Password = emtity.Password;
                }
                user.Address = emtity.Address;
                user.Email = emtity.Email;
                user.Phone = emtity.Phone;
                user.Status = emtity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Log the error
                return false;
            }
        }
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public int Login(string userName, string password)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
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