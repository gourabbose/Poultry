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
    public class VendorController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();

        //
        // GET: /Vendor/

        public ActionResult Index()
        {
            return View(_dbContext.Vendor.ToList());
        }

        //
        // GET: /Vendor/Details/5

        public ActionResult Details(int id = 0)
        {
            Vendor vendor = _dbContext.Vendor.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // GET: /Vendor/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vendor/Create

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

        //
        // GET: /Vendor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Vendor vendor = _dbContext.Vendor.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // POST: /Vendor/Edit/5

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

        //
        // GET: /Vendor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Vendor vendor = _dbContext.Vendor.Find(id);
            if (vendor == null)
            {
                return HttpNotFound();
            }
            return View(vendor);
        }

        //
        // POST: /Vendor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Vendor vendor = _dbContext.Vendor.Find(id);
            _dbContext.Vendor.Remove(vendor);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}