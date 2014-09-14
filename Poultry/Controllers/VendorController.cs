using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poultry.Models;
using Poultry.DbContexts;
using Poultry.Filters;

namespace Poultry.Controllers
{
    [InitializeSimpleMembership]
    public class VendorController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();

        #region CRUD
        public ActionResult Index()
        {
            return View(_dbContext.Vendor.ToList());
        }
        public ActionResult Details(int id = 0)
        {
            Vendor vendor = _dbContext.Vendor.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Vendor.Add(vendor);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendor);
        }
        public ActionResult Edit(int id = 0)
        {
            Vendor vendor = _dbContext.Vendor.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }
        [HttpPost]
        public ActionResult Edit(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(vendor).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendor);
        }
        public ActionResult Delete(int id = 0)
        {
            Vendor vendor = _dbContext.Vendor.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = _dbContext.Vendor.Find(id);
            _dbContext.Vendor.Remove(vendor);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Vendor Supply
        [HttpGet]
        public ActionResult AddVendorSupply()
        {
            var items = _dbContext.Item.Where(t => t.Type == StockType.VendorItem).ToList();
            ViewBag.ItemList = new SelectList(items, "Id", "Name", items.FirstOrDefault());
            var vendors = _dbContext.Vendor.ToList();
            ViewBag.VendorList = new SelectList(vendors, "Id", "Name", vendors.FirstOrDefault());
            return View();
        }
        [HttpPost]
        public ActionResult AddVendorSupply(int VendorId, IEnumerable<Stock> supply, int? ChickCount)
        {
            var vendor = _dbContext.Vendor.Find(VendorId);
            if (ChickCount.HasValue && ChickCount > 0)
            {
                var chick = _dbContext.Item.Where(t => t.Type == StockType.Chicken).First();
                var CurrentStock = _dbContext.Stock.Where(t => t.Type == StockType.Chicken).First();
                var log = new VendorLog { Item = chick, Date = DateTime.Now, Quantity = ChickCount.Value, Vendor = vendor };
                CurrentStock.Quantity += ChickCount.Value;
                _dbContext.Entry(CurrentStock).State = EntityState.Modified;
                _dbContext.VendorLog.Add(log);
            }
            if (supply != null && supply.Count() > 0)
            {
                foreach (var stock in supply)
                {
                    var item = _dbContext.Item.Find(stock.Item.Id);
                    var log = new VendorLog { Item = item, Date = DateTime.Now, Quantity = stock.Quantity, Vendor = vendor };
                    var currentStock = _dbContext.Stock.Where(t => t.Item.Id == stock.Item.Id).First();
                    currentStock.Quantity += stock.Quantity;
                    _dbContext.Entry(currentStock).State = EntityState.Modified;
                    _dbContext.VendorLog.Add(log);
                }
            }
            _dbContext.SaveChanges();
            return null;
        }
        #endregion
    }
}