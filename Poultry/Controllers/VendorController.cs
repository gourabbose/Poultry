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
using Poultry.Models.ViewModels;
using System.Configuration;

namespace Poultry.Controllers
{
    [InitializeSimpleMembership]
    public class VendorController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();

        #region CRUD
        public ActionResult Index(int page = 1)
        {
            ViewBag.Paging = Boolean.Parse(ConfigurationManager.AppSettings["Pagination"].ToString());
            int pageSize = ViewBag.PazeSize = int.Parse(ConfigurationManager.AppSettings["Pagesize"].ToString());
            ViewBag.Page = page - 1;
            ViewBag.Count = _dbContext.Vendor.Where(t => t.IsDeleted != true).Count();
            return View(_dbContext.Vendor.Where(t => t.IsDeleted != true).ToList()
                .OrderBy(t => t.Name).Skip((page - 1) * pageSize).Take(pageSize));
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
                TempData["Messege"] = "Updated Successfully";
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
            vendor.IsDeleted = true;
            _dbContext.Entry(vendor).State = EntityState.Modified;
            _dbContext.SaveChanges();
            TempData["Messege"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
        #endregion

        #region Vendor Supply
        [HttpGet]
        public ActionResult AddVendorSupply()
        {
            var items = _dbContext.Item.Where(t => t.Type == StockType.VendorItem).ToList();
            ViewBag.ItemList = new SelectList(items, "Id", "Name", items.FirstOrDefault());
            var vendors = _dbContext.Vendor.Where(t => t.IsDeleted != true).ToList();
            ViewBag.VendorList = new SelectList(vendors, "Id", "Name", vendors.FirstOrDefault());
            return View();
        }
        [HttpPost]
        public ActionResult AddVendorSupply(VendorLog supply, int? ChickCount, int? Payment, string PaymentMode)
        {
            try
            {
                if (supply.Items == null) supply.Items = new List<ItemTransaction>();
                var vendor = _dbContext.Vendor.Find(supply.Vendor.Id);
                int totalPrice = 0;
                supply.Vendor = vendor;
                if (ChickCount.HasValue && ChickCount > 0)
                {
                    var chick = _dbContext.Item.Where(t => t.Type == StockType.Chicken).First();
                    var CurrentStock = _dbContext.Stock.Where(t => t.Item.Type == StockType.Chicken).First();
                    supply.Items.Add(new ItemTransaction { Item = chick, Qty = ChickCount.Value });
                    CurrentStock.Quantity += ChickCount.Value;
                    _dbContext.Entry(CurrentStock).State = EntityState.Modified;
                }
                if (supply.Items.Count() > 0)
                {
                    foreach (var stock in supply.Items)
                    {
                        var item = _dbContext.Item.Find(stock.Item.Id);
                        totalPrice += stock.Price;
                        stock.Item = item;
                        var currentStock = _dbContext.Stock.Where(t => t.Item.Id == stock.Item.Id).First();
                        currentStock.Quantity += stock.Qty;
                        _dbContext.Entry(currentStock).State = EntityState.Modified;
                    }
                }
                if (Payment.HasValue && Payment.Value > 0)
                {
                    var payment = new VendorPaymentLog { Date = supply.Date, Amount = Payment.Value, Vendor = vendor };
                    supply.Payment = Payment.Value;

                    supply.PaymentMode = PaymentMode;
                    totalPrice -= Payment.Value;
                    _dbContext.VendorPayments.Add(payment);
                }
                if (totalPrice > 0)
                {
                    var log = new VendorPaymentLog { Date = supply.Date, Vendor = vendor, Amount = -totalPrice };
                    _dbContext.VendorPayments.Add(log);
                }
                vendor.Due += totalPrice;
                _dbContext.Entry(vendor).State = EntityState.Modified;
                _dbContext.VendorLog.Add(supply);
                _dbContext.SaveChanges();
                TempData["Messege"] = "Supply Added to Stock";
            }
            catch (Exception ex)
            {
                TempData["Messege"] = "Some error occured. Please contact developer.";
            }
            return RedirectToAction("AddVendorSupply");
        }
        #endregion

        public ActionResult History(int id)
        {
            var vendor = _dbContext.Vendor.Find(id);
            var vendorlogs = _dbContext.VendorLog.Include("Items").Where(t => t.Vendor.Id == vendor.Id).ToList();
            var vendorPaymentLogs = _dbContext.VendorPayments.Where(t => t.Vendor.Id == vendor.Id).ToList();
            var finalList = (from l in vendorlogs
                             select new VendorHistory { Date = l.Date, Type = "Cr", Amount = l.Items.Sum(t => t.Price), LogId = l.Id })
                             .Union(from l in vendorlogs
                                    where l.Payment > 0
                                    select new VendorHistory { Date = l.Date, Type = "Dr", Amount = l.Payment })
                            .Union(from p in vendorPaymentLogs
                                   select new VendorHistory { Date = p.Date, Type = "Dr", Amount = p.Amount })
                            .OrderByDescending(t => t.Date).ToList();
            var vm = new Tuple<Vendor, List<VendorHistory>>(vendor, finalList);
            return View(vm);
        }

        public ActionResult Payment(DateTime Date, int Amount, int Vendor)
        {
            var vendor = _dbContext.Vendor.Find(Vendor);
            var log = new VendorPaymentLog { Date = Date, Amount = Amount, Vendor = vendor };
            _dbContext.VendorPayments.Add(log);
            _dbContext.SaveChanges();
            TempData["Messege"] = "Payment Received Successfully";
            return RedirectToAction("Payment");
        }
        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult VendorPaymentHistory()
        {
            var list = _dbContext.Vendor.Where(t => t.IsDeleted != true);
            ViewBag.VendorList = new SelectList(list, "Name", "Id", "Select Vendor");
            return View();
        }


        [HttpGet]
        public ActionResult VendorPayments(int id)
        {
            var v = _dbContext.Vendor.Find(id);
            return View(v);
        }
        [HttpPost]
        public ActionResult VendorPayments(int VendorId, int Amount)
        {
            var vendor = _dbContext.Vendor.Find(VendorId);
            vendor.Due -= Amount;
            _dbContext.Entry(vendor).State = EntityState.Modified;
            _dbContext.SaveChanges();
            TempData["Messege"] = "Payment Paid Successfully";
            return RedirectToAction("Index");
        }

    }
}