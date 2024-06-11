using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_MVC.Areas.Admin.Code
{
    public class SessionHelper
    {
        public static void SetSession(UserSession session)
        {
            HttpContext.Current.Session["loginSession"] = session;//là nơi lưu trữ các thông tin phiên của người dùng trong ASP.NET
        }
        public static UserSession GetSession()
        {
            var session = HttpContext.Current.Session["loginSession"];
            if (session == null)
            {
                return null;
            }
            else
            {
                return session as UserSession;
            }
        }
    }
}