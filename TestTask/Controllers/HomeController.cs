using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestTask.Models;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        static string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json.txt");
        IRepository ob = new PersonRepository(@destPath);
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //Saving data from table to TXT file in JSON format
        [HttpPost]
        public ActionResult Setdata(Person data)
        {

            //write string to file
            ob.Add(data);
            return Json(new object());
        }
        //Read data to table from TXT file
        public ActionResult Readdata()
        {
            //ob.GetAll();
            return Json(ob.GetAll(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Remove(Person data)
        {
            ob.Remove(data);
            return Json(new object());
        }
    }
}