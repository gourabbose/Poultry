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
    public class TraderController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();

        #region CRUD
        public ActionResult Index()
        {
            return View(_dbContext.Traders.Where(t => t.IsDeleted != true).ToList());
        }
        public ActionResult Details(int id = 0)
        {
            Trader Trader = _dbContext.Traders.Find(id);
            if (Trader == null)
            {
                return HttpNotFound();
            }
            return View(Trader);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Trader Trader)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Traders.Add(Trader);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Trader);
        }
        public ActionResult Edit(int id = 0)
        {
            Trader Trader = _dbContext.Traders.Find(id);
            if (Trader == null)
            {
                return HttpNotFound();
            }
            return View(Trader);
        }
        [HttpPost]
        public ActionResult Edit(Trader Trader)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(Trader).State = EntityState.Modified;
                _dbContext.SaveChanges();
                TempData["Messege"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(Trader);
        }
        public ActionResult Delete(int id = 0)
        {
            Trader Trader = _dbContext.Traders.Find(id);
            if (Trader == null)
            {
                return HttpNotFound();
            }
            return View(Trader);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Trader Trader = _dbContext.Traders.Find(id);
            Trader.IsDeleted = true;
            _dbContext.Entry(Trader).State = EntityState.Modified;
            _dbContext.SaveChanges();
            TempData["Messege"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
        #endregion
    }
}