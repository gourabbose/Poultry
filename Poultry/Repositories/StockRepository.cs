using Poultry.DbContexts;
using Poultry.Models;
using Poultry.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Poultry.Repositories
{
    public class StockRepository : RepositoryBase<Stock>
    {
        private readonly DataBaseContext _context;

        public Stock Chickens()
        {
            var stock = _context.Stock.First(t => t.Type==StockType.Chicken);
            return stock;
        }
        public IEnumerable<Stock> VendorItems()
        {
            var stocks = _context.Stock.Where(t => t.Type == StockType.VendorItem);
            return stocks;
        }
        public IEnumerable<Stock> Foods()
        {
            var stocks = _context.Stock.Where(t => t.Type == StockType.FoodItem);
            return stocks;
        }

    }
}