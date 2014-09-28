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



        #region Current Stock Info
        public ActionResult InStockVendorItems()
        {
            var stock = _dbContext.Stock.Include("Item").Where(t => t.Type == StockType.VendorItem && t.Item.IsDeleted != true).ToList();
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

    }
}
