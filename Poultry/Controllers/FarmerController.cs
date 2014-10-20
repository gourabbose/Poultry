﻿using System;
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
            var farmers = _dbContext.Farmer.Where(t => !t.IsActive).ToList();
            ViewBag.FarmerList = new SelectList(farmers, "Id", "Name", farmers.FirstOrDefault());
            var items = _dbContext.Stock.Where(t => t.Item.Type == StockType.VendorItem || t.Item.Type == StockType.FoodItem).Include("Item").ToList();
            ViewBag.ItemList = new SelectList(items, "Id", "Item.Name", items.FirstOrDefault());
            ViewBag.AvailableChicken = _dbContext.Stock.Where(t => t.Item.Type == StockType.Chicken).FirstOrDefault().Quantity;
            return View();
        }
        [HttpPost]
        public ActionResult Delivery(int? ChickCount, IEnumerable<Stock> Stocks, int FarmerId, DateTime Date)
        {
            var farmer = _dbContext.Farmer.Find(FarmerId);
            if (ChickCount.HasValue)
            {
                farmer.IsActive = true;
                _dbContext.Entry(farmer).State = EntityState.Modified;
                var chickItem = _dbContext.Item.Where(t => t.Type == StockType.Chicken).First();
                var farmerLog = new FarmerLog { Date = Date, Farmer = farmer, Item = chickItem, Quantity = ChickCount.Value, ActivityFlag = true };
                _dbContext.FarmerLog.Add(farmerLog);
                var chickenStock = _dbContext.Stock.Where(t => t.Item.Type == StockType.Chicken).FirstOrDefault();
                chickenStock.Quantity -= ChickCount.Value;
                _dbContext.Entry(chickenStock).State = EntityState.Modified;
            }
            foreach (var s in Stocks)
            {
                var log = new FarmerLog { Date = Date, Farmer = farmer, Item = _dbContext.Item.Find(s.Item.Id), Quantity = s.Quantity, ActivityFlag = false };
                _dbContext.FarmerLog.Add(log);
                var stock = _dbContext.Stock.Where(t => t.Item.Id == s.Item.Id).First();
                stock.Quantity -= s.Quantity;
                _dbContext.Entry(stock).State = EntityState.Modified;
            }
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