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
using System.Web.Security;
using WebMatrix.WebData;

namespace Poultry.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private DataBaseContext _dbContext = new DataBaseContext();

        #region User Administration
        public ActionResult Supervisors()
        {

            return View(GetUserInRole("Supervisor"));
        }
        public ActionResult DEOs()
        {
            return View(GetUserInRole("DEO"));
        }
        List<UserList> GetUserInRole(string role)
        {
            var users = Roles.GetUsersInRole(role);
            var members = new List<UserList>();
            foreach (var user in users)
            {
                var member = Membership.GetUser(user);
                var usr = new UserList() { UserId = user, Membership = member };
                members.Add(usr);
            }
            return members;
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View("/Views/Account/Register.cshtml");
        }
        [HttpPost]
        public ActionResult CreateUser(RegisterModel model, string role)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    //WebSecurity.Login(model.UserName, model.Password);
                    Roles.AddUserToRole(model.UserName, role);
                    var user = _dbContext.UserProfiles.Where(t => t.UserName == model.UserName).First();
                    _dbContext.Entry(user).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(role + "s");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            // If we got this far, something failed, redisplay form
            return View("/Views/Account/Register.cshtml", model);
        }
        public ActionResult ResetPass(string UserName, string redirect)
        {
            var token = WebSecurity.GeneratePasswordResetToken(UserName);
            WebSecurity.ResetPassword(token, "password");
            TempData["Messege"] = "Password Resest Successful for User : " + UserName + ". Default is 'password'";
            return RedirectToAction(redirect == "DEO" ? "DEOs" : "Supervisors");
        }
        #endregion

        #region Logs
        public ActionResult DailyConsumptionLog()
        {
            var log = _dbContext.ConsumptionLogs.Include("Item").Include("For").OrderByDescending(t => t.Date).ToList();
            return View(log);
        }
        public ActionResult VendorLogs()
        {
            var log = _dbContext.VendorLog.Include("Vendor").Include("Items").Include("Items.Item").OrderByDescending(t => t.Date).ToList();
            return View(log);
        }
        public ActionResult GetVendorLogById(int id)
        {
            var log = _dbContext.VendorLog.Include("Items.Item").Where(t => t.Id == id).First();
            return PartialView("VendorLogPages", log);
        }
        public ActionResult FarmerLogs()
        {
            var log = _dbContext.FarmerLog.Include("Farmer").Include("Item").OrderByDescending(t => t.Date).ToList();
            return View(log);
        }
        #endregion

        #region Cost Sheet
        [HttpGet]
        public ActionResult AddCostSheet()
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
        public ActionResult AddCostSheet(DateTime Date, IEnumerable<CostSheetViewModel> model)
        {
            var flag = false;
            foreach (var item in model)
            {
                if (item.Qty1 > 0 || item.Qty2 > 0 || item.Qty3 > 0)
                {
                    flag = true;
                    var costsheet = new CostSheet
                    {
                        Date = Date,
                        Item = _dbContext.Item.Find(item.Items.Id),
                        BPS_Amt = item.Amount1,
                        BS_Amt = item.Amount2,
                        BF_Amt = item.Amount3,
                        BPS_Qty = item.Qty1,
                        BS_Qty = item.Qty2,
                        BF_Qty = item.Qty3,
                        RatePerKg = item.RatePerKg,
                        Remarks = item.Remarks
                    };
                    _dbContext.CostSheets.Add(costsheet);
                }
            }
            if (flag)
            {
                _dbContext.SaveChanges();
                TempData["Messege"] = "Submission Successful";
            }
            TempData["Messege"] = "Nothing to Save !!";
            return RedirectToAction("AddCostSheet");
        }
        public ActionResult CostSheets()
        {
            var list = _dbContext.CostSheets.Include("Item").ToList();
            return View(list);
        }
        #endregion
    }
}
