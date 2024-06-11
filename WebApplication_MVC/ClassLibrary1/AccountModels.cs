using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AccountModels
    {
        private ShopOnlineDbcontext context = null;
        public AccountModels() 
        {
            context = new ShopOnlineDbcontext();
        }
        public bool Login(string userName, string password)
        {
            object[] sqlParameters =
            {
               new SqlParameter("@UserName", userName),
               new SqlParameter("Password", password)
            };
            var res = context.Database.SqlQuery<bool>("Sp_Acount_Login @UserName,@Password",sqlParameters).SingleOrDefault();
            return res;
        }
    }
}
