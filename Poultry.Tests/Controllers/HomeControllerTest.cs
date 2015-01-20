using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poultry;
using Poultry.Controllers;
using Poultry.Models;

namespace Poultry.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new StockController();
            for (int i = 1; i < 50; i++)
            {
                Item item = new Item {Name="Test"+i,Type=StockType.VendorItem,Unit="Kg",IsDeleted=false  };
                controller.CreateItem(item);
            }
        }



        [TestMethod]
        public void PopulateVendor()
        {
            var controller = new VendorController();
            for (int i = 1; i < 50; i++)
            {
                Vendor item = new Vendor { Name = "Test" + i, Address = "ASD", ContactNo = "098098", IsDeleted = false };
                controller.Create(item);
            }
        }

        [TestMethod]
        public void PopulateFarmer()
        {
            var controller = new FarmerController();
            for (int i = 1; i < 50; i++)
            {
                Farmer item = new Farmer { Name = "Test Farmer" + i, Address = "ASD", ContactNo = "098098", IsDeleted = false };
                controller.Create(item);
            }
        }


        [TestMethod]
        public void PopulateTrader()
        {
            var controller = new TraderController();
            for (int i = 1; i < 50; i++)
            {
                Trader item = new Trader { Name = "Test Farmer" + i, Address = "ASD", ContactNo = "098098", IsDeleted = false };
                controller.Create(item);
            }
        }


        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
