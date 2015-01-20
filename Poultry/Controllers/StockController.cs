using Poultry.DbContexts;
using Poultry.Filters;
using Poultry.Models;
using Poultry.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            var stock = _dbContext.Stock.Include("Item").Where(t => t.Item.Type == StockType.VendorItem).ToList();
            //return View(stock);
            var chick_stock = _dbContext.Stock.Include("Item").Where(t => t.Item.Type == StockType.Chicken).ToList();
            var result = new Tuple<IEnumerable<Stock>, IEnumerable<Stock>>(stock, chick_stock);
            return View(result);
        }
        public ActionResult InStockFoodItems()
        {
            var stock = _dbContext.Stock.Include("Item").Where(t => t.Item.Type == StockType.FoodItem).ToList();
            return View(stock);
        }
        public ActionResult InStockChicken()
        {
            var stock = _dbContext.Stock.Include("Item").Where(t => t.Item.Type == StockType.Chicken).ToList();
            return View(stock);
        }
        #endregion

        #region Item Types
        public ActionResult VendorItemTypes(int page = 1)
        {
            ViewBag.Paging = Boolean.Parse(ConfigurationManager.AppSettings["Pagination"].ToString());
            int pageSize = ViewBag.PazeSize = int.Parse(ConfigurationManager.AppSettings["Pagesize"].ToString());
            ViewBag.Page = page-1;
            ViewBag.Count = _dbContext.Item.Where(t => t.Type == StockType.VendorItem && t.IsDeleted != true).Count();
            return View(_dbContext.Item.Where(t => t.Type == StockType.VendorItem && t.IsDeleted != true)
                                        .OrderBy(t => t.Name).Skip((page - 1) * pageSize).Take(pageSize));
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
                var existing = _dbContext.Item.Where(t => t.Name == item.Name.ToUpper()).FirstOrDefault();
                if (existing != null)
                {
                    TempData["Messege"] = "Item already exists !!!";
                    return View(item);
                }
                item.Name = item.Name.ToUpperInvariant();
                var stock = new Stock { Item = item, Quantity = 0 };
                _dbContext.Item.Add(item);
                _dbContext.Stock.Add(stock);
                _dbContext.SaveChanges();
                TempData["Messege"] = "Item Added Successfully";
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
                var stock = _dbContext.Stock.Where(t => t.Item.Id == item.Id).First();
                _dbContext.SaveChanges();
                TempData["Messege"] = "Item Edited Successfully";
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
        public ActionResult Withdraw(IEnumerable<Consumption> withdrawal, DateTime Date)
        {
            try
            {
                var forItems = _dbContext.Item.Where(t => t.Type == StockType.FoodItem).ToList();
                foreach (var item in withdrawal)
                {
                    var wItem = _dbContext.Item.Find(item.Items.Id);
                    if (item.Qty1 > 0)
                    {
                        var log = new ConsumptionLog();
                        log.Date = Date;
                        log.For = (from f in forItems
                                   where f.Name == "B/PS"
                                   select f).First();
                        log.Item = wItem;
                        log.Quantity = item.Qty1;
                        log.Ratio = item.Ratio1;
                        _dbContext.ConsumptionLogs.Add(log);
                    }
                    if (item.Qty2 > 0)
                    {
                        var log = new ConsumptionLog();
                        log.Date = Date;
                        log.For = (from f in forItems
                                   where f.Name == "B/S"
                                   select f).First();
                        log.Item = wItem;
                        log.Quantity = item.Qty2;
                        log.Ratio = item.Ratio2;
                        _dbContext.ConsumptionLogs.Add(log);
                    }
                    if (item.Qty3 > 0)
                    {
                        var log = new ConsumptionLog();
                        log.Date = Date;
                        log.For = (from f in forItems
                                   where f.Name == "B/F"
                                   select f).First();
                        log.Item = wItem;
                        log.Quantity = item.Qty3;
                        log.Ratio = item.Ratio3;
                        _dbContext.ConsumptionLogs.Add(log);
                    }
                    var totalQty = (item.Qty1 + item.Qty2 + item.Qty3);
                    if (totalQty > 0)
                    {
                        var stock = _dbContext.Stock.Where(t => t.Item.Id == item.Items.Id).First();
                        stock.Quantity -= totalQty;
                        _dbContext.Entry(stock).State = EntityState.Modified;
                    }
                }
                _dbContext.SaveChanges();
                TempData["Messege"] = "Withdrawal Successful";
            }
            catch (Exception ex)
            {
                TempData["Messege"] = "Withdrawal Failed";
            }
            return RedirectToAction("Withdraw");
        }
        #endregion

        #region Add Stock(Production)
        public ActionResult AddStock()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStock(DateTime Date, int? BPS, int? BS, int? BF)
        {
            var foodStocks = _dbContext.Stock.Include("Item").Where(t => t.Item.Type == StockType.FoodItem && t.Item.IsDeleted != true);
            if (BPS.HasValue && BPS.Value > 0)
            {
                var _bps = foodStocks.Where(t => t.Item.Name == "B/PS").First();
                _bps.Quantity += BPS.Value;
                _dbContext.Entry(_bps).State = EntityState.Modified;
                var prodLog = new ProductionLog
                {
                    Date = Date,
                    Item = _bps.Item,
                    Qty = BPS.Value
                };
                _dbContext.ProductionLogs.Add(prodLog);
            }
            if (BS.HasValue && BS.Value > 0)
            {
                var _bs = foodStocks.Where(t => t.Item.Name == "B/S").First();
                _bs.Quantity += BS.Value;
                _dbContext.Entry(_bs).State = EntityState.Modified;
                var prodLog = new ProductionLog
                {
                    Date = Date,
                    Item = _bs.Item,
                    Qty = BS.Value
                };
                _dbContext.ProductionLogs.Add(prodLog);
            }
            if (BF.HasValue && BF.Value > 0)
            {
                var _bf = foodStocks.Where(t => t.Item.Name == "B/F").First();
                _bf.Quantity += BF.Value;
                _dbContext.Entry(_bf).State = EntityState.Modified;
                var prodLog = new ProductionLog
                {
                    Date = Date,
                    Item = _bf.Item,
                    Qty = BF.Value
                };
                _dbContext.ProductionLogs.Add(prodLog);
            }
            _dbContext.SaveChanges();
            TempData["Messege"] = "Stock Added Successfully";
            return RedirectToAction("AddStock");
        }
        #endregion

    }
}
