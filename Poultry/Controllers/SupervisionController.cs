using Poultry.DbContexts;
using Poultry.Models;
using Poultry.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poultry.Controllers
{
    public class SupervisionController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();

        public ActionResult ActiveFarmers()
        {
            List<FarmerActivity> activities = new List<FarmerActivity>();
            var farmers = _dbContext.Farmer.Where(t => t.IsDeleted != true && t.IsActive).ToList();
            foreach (var farmer in farmers)
            {
                var log = _dbContext.FarmerLog.Where(t=>t.Farmer.Id==farmer.Id && t.ActivityFlag).OrderByDescending(t=>t.Date).First();
                var activity = new FarmerActivity
                {
                    Farmer=farmer,
                    ActivityDate=log.Date,
                    NoOfChicks=log.Quantity
                };
                activities.Add(activity);
            }
            activities = activities.OrderBy(t => t.ActivityDate).ToList();
            return View(activities);
        }

        public ActionResult AddReport(int id)
        {
            var farmerlog = _dbContext.FarmerLog.Where(t => t.Farmer.Id == id && t.Item.Type == StockType.Chicken).OrderByDescending(t => t.Date).First();
            return View(farmerlog);
        }
    }
}
