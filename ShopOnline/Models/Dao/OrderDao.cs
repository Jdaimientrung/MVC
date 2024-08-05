using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class OrderDao
    {
        ShopOnlineDbContext db = null;  
        public OrderDao() 
        {
            db = new ShopOnlineDbContext();
        }
        public int  Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.OrderID;
            
        }

    }
}
