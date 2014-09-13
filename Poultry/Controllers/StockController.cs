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

namespace Poultry.Controllers
{
    [InitializeSimpleMembership]
    public class StockController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();

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
        public ActionResult AddVendorSupply(int VendorId, IEnumerable<Stock> supply)
        {
            var vendor = _dbContext.Vendor.Find(VendorId);
            foreach (var stock in supply)
            {
                var item = _dbContext.Item.Find(stock.Item.Id);
                var log = new VendorLog { Item = item, Date = DateTime.Now, Quantity = stock.Quantity, Vendor = vendor };
                var currentStock = _dbContext.Stock.Where(t => t.Item.Id == stock.Item.Id).First();
                currentStock.Quantity += stock.Quantity;
                _dbContext.Entry(currentStock).State = EntityState.Modified;
                _dbContext.VendorLog.Add(log);
            }
            _dbContext.SaveChanges();
            return null;
        }
        #endregion

        public ActionResult ItemAdd()
        {
            //var item = new Item { Name = "Chicken", Type = StockType.Chicken };
            //_dbContext.Item.Add(item);
            var stock = new Stock { Item = _dbContext.Item.Where(t => t.Type == (StockType.Chicken)).First(), Quantity = 0 };
            _dbContext.Stock.Add(stock);
            //var items = _dbContext.Item.ToList();
            //var stocks = _dbContext.Stock.ToList();
            //foreach (var item in items)
            //{
            //    var stock = new Stock() { Item = item, Quantity = 0, Type = StockType.VendorItem };
            //    _dbContext.Stock.Add(stock);
            //}
            _dbContext.SaveChanges();

            return Content("Success");
        }

        #region Current Stock Details
        public ActionResult InStockVendorItems()
        {
            var stock = _dbContext.Stock.Include("Item").Where(t => t.Type == StockType.VendorItem).ToList();
            return View(stock);
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
            return View(_dbContext.Item.Where(t => t.Type == StockType.VendorItem));
        }
        public ActionResult FoodItemTypes()
        {
            return View(_dbContext.Item.Where(t => t.Type == StockType.FoodItem));
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
                _dbContext.SaveChanges();
                return RedirectToAction(item.Type == StockType.VendorItem ? "VendorItemTypes" : "FoodItemTypes");
            }
            else
            {
                return View(item);
            }
        }
        #endregion

    }
}
