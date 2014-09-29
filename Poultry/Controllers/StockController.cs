using Poultry.DbContexts;
using Poultry.Filters;
using Poultry.Models;
using Poultry.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Poultry.Controllers
{
    [InitializeSimpleMembership]
    public class StockController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();



        #region Current Stock Info
        public ActionResult InStockVendorItems()
        {
            var stock = _dbContext.Stock.Include("Item").Where(t => t.Type == StockType.VendorItem).ToList();
            //return View(stock);
            var chick_stock = _dbContext.Stock.Include("Item").Where(t => t.Type == StockType.Chicken).ToList();
            var result = new Tuple<IEnumerable<Stock>, IEnumerable<Stock>>(stock, chick_stock);
            return View(result);
        }
        public ActionResult InStockFoodItems()
        {
            var stock = _dbContext.Stock.Include("Item").Where(t => t.Type == StockType.FoodItem).ToList();
            return View(stock);
        }
        public ActionResult InStockChicken()
        {
            var stock = _dbContext.Stock.Include("Item").Where(t => t.Type == StockType.Chicken).ToList();
            return View(stock);
        }
        #endregion

        #region Item Types
        public ActionResult VendorItemTypes()
        {
            return View(_dbContext.Item.Where(t => t.Type == StockType.VendorItem && t.IsDeleted != true));
        }
        public ActionResult FoodItemTypes()
        {
            return View(_dbContext.Item.Where(t => t.Type == StockType.FoodItem && t.IsDeleted != true));
        }
        public ActionResult CreateItem()
        {
            //var t = from StockType s in Enum.GetValues(typeof(StockType))
            //        select new { Item = s.ToString() };
            ViewBag.TypeList = new SelectList(new List<string> { "VendorItem", "FoodItem" });
            return View();
        }
        [HttpPost]
        public ActionResult CreateItem(Item item)
        {
            if (ModelState.IsValid)
            {
                var stock = new Stock { Item = item, Quantity = 0, Type = item.Type };
                _dbContext.Item.Add(item);
                _dbContext.Stock.Add(stock);
                _dbContext.SaveChanges();
                return RedirectToAction(item.Type == StockType.VendorItem ? "VendorItemTypes" : "FoodItemTypes");
            }
            else
                return View(item);
        }
        public ActionResult EditItem(int Id)
        {
            var item = _dbContext.Item.Find(Id);
            ViewBag.TypeList = new SelectList(new List<string> { "VendorItem", "FoodItem" });
            return View(item);
        }
        [HttpPost]
        public ActionResult EditItem(Item item)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(item).State = EntityState.Modified;
                var stock = _dbContext.Stock.Where(t=>t.Item.Id==item.Id).First();
                stock.Type=item.Type;
                _dbContext.Entry(stock).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction(item.Type == StockType.VendorItem ? "VendorItemTypes" : "FoodItemTypes");
            }
            else
            {
                return View(item);
            }
        }
        public ActionResult DeleteItem(int Id)
        {
            var item = _dbContext.Item.Find(Id);
            item.IsDeleted = true;
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
            TempData["Messege"] = "Deleted Successfully";
            return RedirectToAction(item.Type == StockType.VendorItem ? "VendorItemTypes" : "FoodItemTypes");
        }
        #endregion

        #region Withdraw Stock
        [HttpGet]
        public ActionResult Withdraw()
        {
            var items = _dbContext.Item.Where(t => t.Type == StockType.VendorItem && t.IsDeleted != true).ToList();
            ViewBag.ItemList = new SelectList(items, "Id", "Name", items.FirstOrDefault());
            var vendors = _dbContext.Vendor.Where(t => t.IsDeleted != true).ToList();
            ViewBag.VendorList = new SelectList(vendors, "Id", "Name", vendors.FirstOrDefault());
            var foods = _dbContext.Item.Where(t => t.Type == StockType.FoodItem && t.IsDeleted != true).ToList();
            ViewBag.FoodList = new SelectList(foods, "Id", "Name", foods.FirstOrDefault());
            return View();
        }
        [HttpPost]
        public ActionResult Withdraw(IEnumerable<Consumption> withdrawal)
        {
            foreach (var item in withdrawal)
            {
                var stock = _dbContext.Stock.Where(t => t.Item.Id == item.Items.Id).First();
                stock.Quantity -= item.Qty;
                _dbContext.Entry(stock).State = EntityState.Modified;
                var log = new ConsumptionLog { Date = DateTime.Now, Item = _dbContext.Item.Find(item.Items.Id), Quantity = item.Qty, For=_dbContext.Item.Find(item.FoodItems.Id) };
                _dbContext.ConsumptionLogs.Add(log);
            }
            _dbContext.SaveChanges();
            TempData["Messege"] = "Withdraw Successful.";
            return RedirectToAction("Withdraw");
        }
        #endregion

    }
}
