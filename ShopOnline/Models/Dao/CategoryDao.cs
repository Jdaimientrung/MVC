using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CategoryDao
    {
        ShopOnlineDbContext db=null;
        public CategoryDao() 
        {
            db = new ShopOnlineDbContext();
           
        }
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> query = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(u => u.CategoryName.Contains(searchString) || u.CategoryName.Contains(searchString));
            }
            return query.OrderBy(x => x.CategoryID).ToPagedList(page, pageSize);

            // return db.Users.OrderByDescending(x => x.UserName).ToPagedList(page, pageSize);
        }
    }
}
