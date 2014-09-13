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

        //
        // GET: /Farmer/

        public ActionResult Index()
        {
            return View(_dbContext.Farmer.ToList());
        }

        //
        // GET: /Farmer/Details/5

        public ActionResult Details(int id = 0)
        {
            Farmer farmer = _dbContext.Farmer.Find(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }

        //
        // GET: /Farmer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Farmer/Create

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

        //
        // GET: /Farmer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Farmer farmer = _dbContext.Farmer.Find(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }

        //
        // POST: /Farmer/Edit/5

        [HttpPost]
        public ActionResult Edit(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(farmer).State=EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(farmer);
        }

        //
        // GET: /Farmer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Farmer farmer = _dbContext.Farmer.Find(id);
            if (farmer == null)
            {
                return HttpNotFound();
            }
            return View(farmer);
        }

        //
        // POST: /Farmer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Farmer farmer = _dbContext.Farmer.Find(id);
            _dbContext.Farmer.Remove(farmer);
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