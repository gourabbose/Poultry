using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poultry.Models;
using Poultry.DbContexts;

namespace Poultry.Controllers
{
    public class FarmerController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();

        #region CRUD
        public ActionResult Index()
        {
            ViewBag.Title = "Farmers";
            return View(_dbContext.Farmer.Where(t => t.IsDeleted != true).ToList());
        }
        public ActionResult ActiveFarmers()
        {
            ViewBag.Title = "Active Farmers";
            return View("Index", _dbContext.Farmer.Where(t => t.IsDeleted != true && t.IsActive).ToList());
        }
        public ActionResult InactiveFarmers()
        {
            ViewBag.Title = "Inactive Farmers";
            return View("Index", _dbContext.Farmer.Where(t => t.IsDeleted != true && !t.IsActive).ToList());
        }
        public ActionResult Details(int id = 0)
        {
            Farmer farmer = _dbContext.Farmer.Find(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Farmer.Add(farmer);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(farmer);
        }
        public ActionResult Edit(int id = 0)
        {
            Farmer farmer = _dbContext.Farmer.Find(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }
        [HttpPost]
        public ActionResult Edit(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(farmer).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Messege"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(farmer);
        }
        public ActionResult Delete(int id = 0)
        {
            Farmer farmer = _dbContext.Farmer.Find(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Farmer farmer = _dbContext.Farmer.Find(id);
            farmer.IsDeleted = true;
            _dbContext.Entry(farmer).State = EntityState.Modified;
            _dbContext.SaveChanges();
            TempData["Messege"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
        #endregion

        #region Delivery
        [HttpGet]
        public ActionResult Delivery()
        {
            var farmers = _dbContext.Farmer.Where(t => !t.IsDeleted).ToList();
            ViewBag.FarmerList = new SelectList(farmers, "Id", "Name", farmers.FirstOrDefault());
            var items = _dbContext.Stock.Where(t => t.Item.Type == StockType.VendorItem || t.Item.Type == StockType.FoodItem).Include("Item").ToList();
            ViewBag.ItemList = new SelectList(items, "Id", "Item.Name", items.FirstOrDefault());
            ViewBag.AvailableChicken = _dbContext.Stock.Where(t => t.Item.Type == StockType.Chicken).FirstOrDefault().Quantity;
            return View();
        }
        [HttpPost]
        public ActionResult Delivery(int? ChickCount, int AdvancePayment, string PaymentMode, IEnumerable<Stock> Stocks, int FarmerId, DateTime Date)
        {
            var farmer = _dbContext.Farmer.Find(FarmerId);
            if (farmer.IsActive && ChickCount.HasValue)
            {
                TempData["Messege"] = "Farmer Already Have Chicks. Delivery of Chicks Not Allowed to Farmer Already Active. Transaction Cancelled";
                TempData["MessegeType"] = "error";
                return RedirectToAction("Delivery");
            }
            var log = new FarmerLog { Date = Date, Farmer = farmer, Items = new List<ItemTransaction>(), Payment = AdvancePayment, PaymentMethod = PaymentMode };
            foreach (var s in Stocks)
            {
                var stock = _dbContext.Stock.Include("Item").Where(t => t.Item.Id == s.Item.Id).First();
                log.Items.Add(new ItemTransaction { Item = stock.Item, Qty = s.Quantity });
                stock.Quantity -= s.Quantity;
                _dbContext.Entry(stock).State = EntityState.Modified;
            }
            if (ChickCount.HasValue)
            {
                log.ActivityFlag = true;
                farmer.IsActive = true;
                _dbContext.Entry(farmer).State = EntityState.Modified;
                var chickItem = _dbContext.Item.Where(t => t.Type == StockType.Chicken).FirstOrDefault();
                log.Items.Add(new ItemTransaction { Item = chickItem, Qty = ChickCount.Value });
                var chickenStock = _dbContext.Stock.Where(t => t.Item.Type == StockType.Chicken).FirstOrDefault();
                chickenStock.Quantity -= ChickCount.Value;
                _dbContext.Entry(chickenStock).State = EntityState.Modified;

                var report = new SupervisionReport { Log = log, Reports = new List<WeeklyReport>() };
                report.Reports.Add(new WeeklyReport(1));
                report.Reports.Add(new WeeklyReport(2));
                report.Reports.Add(new WeeklyReport(3));
                report.Reports.Add(new WeeklyReport(4));
                report.Reports.Add(new WeeklyReport(5));
                report.Reports.Add(new WeeklyReport(6));
                report.Reports.Add(new WeeklyReport(7));
                report.ExtraData = new ExtraData();
                _dbContext.Reports.Add(report);
            }
            _dbContext.FarmerLog.Add(log);
            _dbContext.SaveChanges();
            TempData["Messege"] = "Delivery to Farmer Successful";
            return RedirectToAction("Delivery");
        }
        #endregion

        #region API
        public JsonResult AvailableQty(int stockId)
        {
            var qty = _dbContext.Stock.Find(stockId).Quantity;
            return Json(new { Success = true, Qty = qty });
        }
        #endregion
    }
}