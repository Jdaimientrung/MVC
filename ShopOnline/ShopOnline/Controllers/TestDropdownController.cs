using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ShopOnline.Controllers
{
    public class TestDropdownController : Controller
    {
        // GET: TestDropdown
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadProvince()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));
            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null; ; ;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.ID = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
        public JsonResult LoadDistrict(int ProvinceID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));
            var xElement = xmlDoc.Element("Root").Elements("Item")
                .Single(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value)==ProvinceID);
            var list = new List<DistrictModel>();
            DistrictModel district = null; ; ;
            foreach (var item in xElement.Elements("Item").Where(x=>x.Attribute("type").Value=="district"))
            {
                district = new DistrictModel();
                district.ID = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = int.Parse(xElement.Attribute("id").Value);
                list.Add(district);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
        public JsonResult LoadPrecinct(int DistrictID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_Data.xml"));
            var xElement = xmlDoc.Element("Root").Elements("Item").Elements("Item")
                .Single(x => x.Attribute("type").Value == "district" && int.Parse(x.Attribute("id").Value) == DistrictID);
            var list = new List<PrecinctModel>();
            PrecinctModel precinct = null; 
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "precinct"))
            {
                precinct = new PrecinctModel();
                precinct.ID = int.Parse(item.Attribute("id").Value);
                precinct.Name = item.Attribute("value").Value;
                precinct.DistrictID = int.Parse(xElement.Attribute("id").Value);
                list.Add(precinct);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
    }
}