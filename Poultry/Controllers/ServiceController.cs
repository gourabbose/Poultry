using Poultry.DbContexts;
using Poultry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poultry.Controllers
{
    public class ServiceController : ApiController
    {
        private DataBaseContext _dbContext = new DataBaseContext();

        [HttpGet]
        public IEnumerable<Vendor> VendorList()
        {
            var vendors = _dbContext.Vendor.ToList();
            return vendors;
        }

        [HttpGet]
        public IEnumerable<object> VendorLogById(int vendorId)
        {
            var vendor = _dbContext.Vendor.Find(vendorId);
            var log = _dbContext.VendorLog.Include("Vendor").Include("Item").Where(t => t.Vendor == vendor).OrderByDescending(t => t.Date).ToList();
            var result = from l in log
                         select new
                         {
                             Date = l.Date,
                             VendorName = l.Vendor.Name,
                             ItemName = l.Item.Name,
                             Quantity = l.Quantity
                         };
            return result;
        }
    }
}
