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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        //Saving data from table to TXT file in JSON format
        [HttpPost]
        public void Setdata(List<Person> data)
        {
            string json = JsonConvert.SerializeObject(data);
            //write string to file
            System.IO.File.WriteAllText(@"D:\\Json.txt", json);
        }
        //Read data to table from TXT file
        public ActionResult Readdata()
        {
            string json = System.IO.File.ReadAllText(@"D:\\Json.txt");
            var ob = (List<Person>)JsonConvert.DeserializeObject(json,typeof(List<Person>));
            return Json(ob,JsonRequestBehavior.AllowGet);
        }
    }
}