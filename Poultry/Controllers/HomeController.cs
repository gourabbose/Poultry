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
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.Stock = _dbContext.Stock.Include("Item").Where(t => t.Item.Type == StockType.FoodItem).ToList();
            try
            {
                ViewBag.ChickStock = _dbContext.Stock.Include("Item").Where(t => t.Item.Type == StockType.Chicken).FirstOrDefault().Quantity;
            }
            catch
            {
                return RedirectToAction("Init");
            }
            var farmers = _dbContext.Farmer.Where(t => t.IsDeleted != true && t.IsActive).ToList();
            var matureChicks = 0;
            var currentWt = 0;
            foreach (var farmer in farmers)
            {
                var log = _dbContext.FarmerLog
                                    .Include("Items")
                                    .Include("Items.Item")
                                    .Where(t => t.Farmer.Id == farmer.Id && t.ActivityFlag)
                                    .OrderByDescending(t => t.Date)
                                    .ToList()
                                    //.Where(t => (DateTime.Now - t.Date).Days > 40)
                                    .FirstOrDefault();
                if (log == null) continue;
                else
                {
                    var report = _dbContext.Reports.Include("Reports").Where(t => t.Log.Id == log.Id).First();
                    currentWt += report.CurrentWeight;
                    matureChicks += log.Items.Where(t => t.Item.Type == Poultry.Models.StockType.Chicken).First().Qty - (log.Lifted + log.TotalDeath);
                }
            }
            ViewBag.MatureChicks = matureChicks;
            ViewBag.CurrentWeight = currentWt;
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

        [AllowAnonymous]
        public ActionResult Init()
        {
            DataBaseContext _dbContext = new DataBaseContext();
            if (_dbContext.Item.Where(t => t.Type == StockType.Chicken).ToList().Count > 0)
            {
                TempData["Messege"] = "Pre-Seed data is already Initialized !!!";
            }
            else
            {
                try
                {
                    //Insert Chicken Item
                    var chick_item = new Item { Name = "Chicken", Type = StockType.Chicken, IsDeleted = false, Unit = "Nos" };
                    _dbContext.Item.Add(chick_item);

                    //Insert Chicken Stock
                    var stock = new Stock { Item = chick_item, Quantity = 0 };
                    _dbContext.Stock.Add(stock);

                    //Insert Food Items
                    var f_item_1 = new Item { Name = "B/PS", Type = StockType.FoodItem, IsDeleted = false, Unit = "Kgs." };
                    var f_item_2 = new Item { Name = "B/S", Type = StockType.FoodItem, IsDeleted = false, Unit = "Kgs." };
                    var f_item_3 = new Item { Name = "B/F", Type = StockType.FoodItem, IsDeleted = false, Unit = "Kgs." };
                    _dbContext.Item.Add(f_item_1);
                    _dbContext.Item.Add(f_item_2);
                    _dbContext.Item.Add(f_item_3);

                    //Insert Food Stocks

                    var f_item_1_s = new Stock { Item = f_item_1, Quantity = 0 };
                    var f_item_2_s = new Stock { Item = f_item_2, Quantity = 0 };
                    var f_item_3_s = new Stock { Item = f_item_3, Quantity = 0 };
                    _dbContext.Stock.Add(f_item_1_s);
                    _dbContext.Stock.Add(f_item_2_s);
                    _dbContext.Stock.Add(f_item_3_s);

                    TempData["Messege"] = "Initialization Succeeded !!! ";
                    _dbContext.SaveChanges();
                }
                catch
                {
                    TempData["Messege"] = "Error Initializing. Please contact developer !!!";
                }
            }
            try
            {
                Roles.CreateRole("Admin");
                Roles.CreateRole("Supervisor");
                Roles.CreateRole("DEO");
                WebSecurity.CreateUserAndAccount("admin", "password");
                Roles.AddUserToRole("admin", "Admin");
            }
            catch
            {
                TempData["Messege"] = TempData["Messege"].ToString() + "    Accounts are already initialized";
            }
            return View("Index");
        }
    }
}
