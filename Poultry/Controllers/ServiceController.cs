using Poultry.DbContexts;
using Poultry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Poultry.Helpers;

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
        public List<VendorLog> VendorLogById(int vendorId)
        {
            var vendor = _dbContext.Vendor.Find(vendorId);
            var log = _dbContext.VendorLog.Include("Vendor").Include("Items").Where(t => t.Vendor == vendor).OrderByDescending(t => t.Date).ToList();
            return log;
        }

        [HttpGet]
        public ServiceResult<Stock> GetCurrentStock(int ItemId)
        {
            try
            {
            var item = _dbContext.Item.Find(ItemId); 
            var stock = _dbContext.Stock.Include("Item").Where(t => t.Item.Id == item.Id).First();
            return new ServiceResult<Stock> { Success = true, Data = new List<Stock> { stock } };
            }
            catch(Exception ex)
            {
                return new ServiceResult<Stock> { Success = false, Messege = ex.InnerException.ToString() };
            }
        }

        public ServiceResult<Stock> GetChickStock()
        {
            try
            {
                var stock = _dbContext.Stock.Where(t => t.Item.Type == StockType.Chicken);
                return new ServiceResult<Stock> { Success = true, Data = stock };
            }
            catch (Exception ex)
            {
                return new ServiceResult<Stock> { Success = false, Messege = ex.InnerException.ToString() };
            }
        }

    }
}
