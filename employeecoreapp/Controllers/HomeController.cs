using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using employeecoreapp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace employeecoreapp.Controllers
{
    public class HomeController : Controller
    {
        public string APIurl = "http://localhost:8080";
        public IActionResult Index()
        {
            List<Employee> lstEmployee = new List<Employee>();

            using (HttpClient client = new HttpClient())
            {
                        client.BaseAddress = new Uri
                (APIurl);
                        MediaTypeWithQualityHeaderValue contentType =
                new MediaTypeWithQualityHeaderValue("application/json");
                        client.DefaultRequestHeaders.Accept.Add(contentType);
                        HttpResponseMessage response = client.GetAsync
                ("/api/Employee").Result;
                        string stringData = response.Content.
                ReadAsStringAsync().Result;
                        List<Employee> data = JsonConvert.DeserializeObject
                <List<Employee>>(stringData);
                        return View(data);
            }


        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri
                             (APIurl);
                    MediaTypeWithQualityHeaderValue contentType =
            new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);

                        string stringData = JsonConvert.
                        SerializeObject(employee);
                        var contentData = new StringContent
                    (stringData, System.Text.Encoding.UTF8,
                    "application/json");
                        HttpResponseMessage response = client.PostAsync
                    ("/api/Employee", contentData).Result;
                        ViewBag.Message = response.Content.
                    ReadAsStringAsync().Result;
                }

               

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
