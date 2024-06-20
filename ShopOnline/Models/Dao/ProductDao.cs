using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ProductDao
    {
        ShopOnlineDbContext db = null;
        public ProductDao()
        {
            db = new ShopOnlineDbContext();
        }
       
        public IEnumerable<Product> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Product> query = db.Products;

            return query.OrderBy(x => x.ProductID).ToPagedList(page, pageSize);
        }
        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }
    }
}
