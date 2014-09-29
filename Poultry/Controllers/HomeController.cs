using Poultry.DbContexts;
using Poultry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Poultry.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Init()
        {
            try
            {
                DataBaseContext _dbContext = new DataBaseContext();
                var item = new Item { Name = "Chicken", Type = StockType.Chicken, IsDeleted = false, Unit = "Nos" };
                _dbContext.Item.Add(item);
                _dbContext.SaveChanges();
                var stock = new Stock { Item = _dbContext.Item.Where(t => t.Type == (StockType.Chicken)).First(), Quantity = 0, Type = StockType.Chicken };
                _dbContext.Stock.Add(stock);
                _dbContext.SaveChanges();
                return Content("Success");
            }
            catch
            {
                return Content("Already Initialized");
            }
        }
    }
}
