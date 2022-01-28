using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolMoney.Data;
using SchoolMoney.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolMoney.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dBContext)
        {
            _logger = logger;
            _dbContext = dBContext;

            //pridej testovaci data pokud lokalni db je nema
            if (!dBContext.Students.Any(x => x.Name == "Evzen"))
            {

                dBContext.Students.Add(new Student("Franta")
                {
                    Transactions = new List<Transaction>()
        {
            new Transaction("Lyzak", 1000, TypeOfPayment.Fund)
        },
                    Bills = new List<Bill>()
        {
            new Bill("lyzak", 900, TypeOfPayment.Fund),
            new Bill("skolne", 5000, TypeOfPayment.Tuition)
        }
                });
                dBContext.Students.Add(new Student("Evzen")
                {
                    Transactions = new List<Transaction>()
        {
            new Transaction("intr", 5000, TypeOfPayment.Accommodation),
            new Transaction("smazak", 10000, TypeOfPayment.Fund)
        },
                    Bills = new List<Bill>()
        {
            new Bill("rizek", 500, TypeOfPayment.Fund),
            new Bill("skolne", 500, TypeOfPayment.Tuition)
        }
                });
                dBContext.SaveChanges();
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }
        [Route("{StudentName?}")]
        public IActionResult Index(string StudentName)
        {
            if (!string.IsNullOrEmpty(StudentName) && _dbContext.Students.Any(x => x.Name.ToLower() == StudentName.ToLower()))
            {
                ViewBag.Student = _dbContext.Students.Include(x => x.Transactions).Include(x => x.Bills).First(x => x.Name.ToLower() == StudentName.ToLower());
            }
            else
            {
                ViewBag.Student = _dbContext.Students.Include(x => x.Transactions).Include(x => x.Bills).First();
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
