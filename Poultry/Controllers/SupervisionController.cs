using Poultry.DbContexts;
using Poultry.Models;
using Poultry.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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
                var log = _dbContext.FarmerLog.Include("Items").Include("Items.Item").Where(t => t.Farmer.Id == farmer.Id && t.ActivityFlag).OrderByDescending(t => t.Date).FirstOrDefault();
                if (log == null) continue;
                var report = _dbContext.Reports.Include("Reports").Where(t => t.Log.Id == log.Id).First();
                var activity = new FarmerActivity
                {
                    Farmer = farmer,
                    ActivityDate = log.Date,
                    NoOfChicks = log.Items.Where(t => t.Item.Type == StockType.Chicken).First().Qty - log.Lifted,
                    ChicksAlive = log.Items.Where(t => t.Item.Type == StockType.Chicken).First().Qty - log.Lifted - report.TotalDeath,
                    CurrentWeight = report.CurrentWeight
                };
                activities.Add(activity);
            }
            activities = activities.OrderBy(t => t.ActivityDate).ToList();
            return View(activities);
        }

        public ActionResult AddReport(int id)
        {
            var farmerlog = _dbContext.FarmerLog.Where(t => t.Farmer.Id == id && t.ActivityFlag).OrderByDescending(t => t.Date).FirstOrDefault();
            var report = _dbContext.Reports.Include("Reports").Include("ExtraData").Where(t => t.Log.Id == farmerlog.Id).FirstOrDefault();
            return View(report);
        }

        [HttpPost]
        public ActionResult AddReport(SupervisionReport report)
        {
            var _report = _dbContext.Reports.Include("Reports").Include("Log").Include("ExtraData").Where(t => t.Id == report.Id).First();
            _report.Log.TotalDeath = report.TotalDeath;
            var list = _report.Reports.ToList();
            list.ForEach(t => _dbContext.Entry(t).State = EntityState.Deleted);
            _dbContext.Entry(_report.ExtraData).State = EntityState.Detached;
            _report.Reports = report.Reports;
            _report.ExtraData = report.ExtraData;
            _report.CurrentWeight = report.CurrentWeight;
            _dbContext.Entry(_report.ExtraData).State = EntityState.Modified;
            _dbContext.Entry(_report).State = EntityState.Modified;
            _dbContext.SaveChanges();
            TempData["Messege"] = "Supervision Report Updated.";
            return RedirectToAction("ActiveFarmers");
        }

        public ActionResult MatureChicks()
        {
            List<FarmerActivity> activities = new List<FarmerActivity>();
            var farmers = _dbContext.Farmer.Where(t => t.IsDeleted != true && t.IsActive).ToList();
            foreach (var farmer in farmers)
            {
                var log = _dbContext.FarmerLog.Include("Items").Include("Items.Item").Where(t => t.Farmer.Id == farmer.Id && t.ActivityFlag).OrderByDescending(t => t.Date).FirstOrDefault();
                if (log == null) continue;
                var activity = new FarmerActivity
                {
                    Farmer = farmer,
                    ActivityDate = log.Date,
                    NoOfChicks = log.Items.Where(t => t.Item.Type == StockType.Chicken).First().Qty
                };
                activities.Add(activity);
            }
            activities = activities.Where(t => (DateTime.Today - t.ActivityDate.Date).Days > 40).OrderBy(t => t.ActivityDate).ToList();
            return View(activities);
        }

        public ActionResult Lifting()
        {
            var _traders = _dbContext.Traders.Where(t => t.IsDeleted != true).ToList();
            ViewBag.TraderList = new SelectList(_traders, "Id", "Name", _traders.FirstOrDefault());
            var farmers = _dbContext.Farmer.Where(t => t.IsDeleted != true && t.IsActive).ToList();
            var matureChicks = new List<FarmerLog>();
            foreach (var farmer in farmers)
            {
                var log = _dbContext.FarmerLog
                                    .Include("Items")
                                    .Include("Items.Item")
                                    .Where(t => t.Farmer.Id == farmer.Id && t.ActivityFlag)
                                    .OrderByDescending(t => t.Date)
                                    .FirstOrDefault();
                if (log == null) continue;
                else matureChicks.Add(log);
            }
            ViewBag.MatureChicks = matureChicks.OrderByDescending(t => t.Date);
            return View();
        }

        [HttpPost]
        public ActionResult Lifting(DateTime Date, int TraderId, IEnumerable<FarmerLog> log, int TotalPrice, int Payment, string PaymentMode)
        {
            var validLogs = log.Where(t => t.Payment > 0);
            var trader = _dbContext.Traders.Find(TraderId);
            var traderLog = new TraderLog { Trader = trader };
            var lifting = new Lifting { Date = Date, TraderLog = traderLog };
            var hydratedLog = new List<FarmerLog>();
            var Qty = 0;
            foreach (var v in validLogs)
            {
                var farmerlog = _dbContext.FarmerLog.Include("Items.Item").Where(t => t.Id == v.Id).First();
                if (farmerlog.Items.Where(t => t.Item.Type == StockType.Chicken).First().Qty - farmerlog.Lifted - farmerlog.TotalDeath == v.Payment)
                    farmerlog.ActivityFlag = false;
                else
                    farmerlog.Lifted += v.Payment;
                Qty += v.Payment;
                var report = _dbContext.Reports.Include("Reports").Where(t => t.Log.Id == v.Id).First();
                report.CurrentWeight -= v.TotalDeath;
                _dbContext.Entry(report).State = EntityState.Modified;
                _dbContext.Entry(farmerlog).State = EntityState.Modified;
                hydratedLog.Add(farmerlog);
            }
            lifting.FarmerLogs = hydratedLog;
            lifting.TraderLog.ChickenCount = Qty;
            lifting.TraderLog.Price = TotalPrice;
            lifting.TraderLog.Payment = Payment;
            lifting.TraderLog.PaymentMethod = PaymentMode;
            trader.PaymentDue += (TotalPrice - Payment);
            _dbContext.Lifting.Add(lifting);
            _dbContext.SaveChanges();
            TempData["Messege"] = "Lifting successful";
            return RedirectToAction("Lifting");
        }

        public ActionResult PastReports()
        {
            List<FarmerActivity> activities = new List<FarmerActivity>();
            var farmers = _dbContext.Farmer.Where(t => t.IsDeleted != true).ToList();
            foreach (var farmer in farmers)
            {
                var log = _dbContext.FarmerLog.Include("Items").Include("Items.Item").Where(t => t.Farmer.Id == farmer.Id).OrderByDescending(t => t.Date).FirstOrDefault();
                if (log == null) continue;
                try
                {
                    var activity = new FarmerActivity
                    {
                        Farmer = farmer,
                        ActivityDate = log.Date,
                        NoOfChicks = _dbContext.Reports.Where(t => t.Log.Id == log.Id).First().Id
                    };
                    activities.Add(activity);
                }
                catch
                {
                    continue;
                }
            }
            activities = activities.OrderBy(t => t.ActivityDate).ToList();
            return View(activities);
        }

        public ActionResult ViewReport(int id)
        {
            var report = _dbContext.Reports.Include("Reports").Include("Log").Include("ExtraData").Where(t => t.Id == id).First();
            return View(report);
        }



    }
}
