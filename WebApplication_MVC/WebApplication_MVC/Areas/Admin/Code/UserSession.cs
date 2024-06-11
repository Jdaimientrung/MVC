using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_MVC.Areas.Admin.Code
{
    [Serializable]//tuần tự hóa ra nhị phân
    public class UserSession
    {
        public string UserName { get; set; }
    }
}