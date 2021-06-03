using Microsoft.VisualStudio.TestTools.UnitTesting;
using DelpinWebApi;
using DelpinWebApi.Models;
using DelpinWebApi.Controllers;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        TodoContext context;

        [TestInitialize()]
        public void Initialize() { 
            var options = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
            context = new TodoContext(options);
        }

        [TestCleanup()]
        public void Cleanup()
        {

            // Cleanup Context DB
            // await context.DisposeAsync();
            context.Database.EnsureDeleted();
            context.SaveChanges();
        }




        [TestMethod]
        public void TestCheckDateExpectedFalse()
        {

            //Arrange 
            var oc = new OrdersController(context);
            
            //Act
            Order o1 = new Order();
            o1.RessourceId = 1;
            o1.Date = DateTime.Today;
            o1.BookingStart = DateTime.Today;
            o1.BookingEnd = new DateTime(2021, 06, 21);
            context.Add(o1);
            context.SaveChanges();
            Order o2 = new Order();
            o2.RessourceId = 1;
            o2.Date = DateTime.Today;
            o2.BookingStart = DateTime.Today;
            o2.BookingEnd = new DateTime(2021, 06, 22);
            var expected = oc.CheckDate(o2);

            //Assert
            Assert.IsFalse(expected);
        }
        [TestMethod]
        public void TestCheckDateBeforeCurrentDateExpectedFalse()
        {

            //Arrange 
            var oc = new OrdersController(context);

            //Act
            Order o1 = new Order();
            o1.RessourceId = 1;
            o1.Date = DateTime.Today;
            o1.BookingStart = DateTime.Today;
            o1.BookingEnd = new DateTime(2021, 06, 21);
            context.Add(o1);
            context.SaveChanges();
            Order o2 = new Order();
            o2.RessourceId = 1;
            o2.Date = DateTime.Today;
            o2.BookingStart = new DateTime(2021, 06, 01);
            o2.BookingEnd = new DateTime(2021, 06, 02);
            var expected = oc.CheckDate(o2);

            //Assert
            Assert.IsFalse(expected);
        }
        [TestMethod]
        public void TestingIfBookingPeriodIsPriorToExistingBookingPeriodExpectedTrue()
        {

            //Arrange 
            var oc = new OrdersController(context);

            //Act
            Order o1 = new Order();
            o1.RessourceId = 1;
            o1.Date = DateTime.Today;
            o1.BookingStart = new DateTime(2021, 07, 01);
            o1.BookingEnd = new DateTime(2021, 07, 02);
            context.Add(o1);
            context.SaveChanges();
            Order o2 = new Order();
            o2.RessourceId = 1;
            o2.Date = DateTime.Today;
            o2.BookingStart = DateTime.Today;
            o2.BookingEnd = new DateTime(2021, 06, 22);
            var expected = oc.CheckDate(o2);

            //Assert
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void TestingIfBookingPeriodIsLaterThanExistingBookingPeriodExpectedTrue()
        {

            //Arrange 
            var oc = new OrdersController(context);

            //Act
            Order o1 = new Order();
            o1.RessourceId = 1;
            o1.Date = DateTime.Today;
            o1.BookingStart = new DateTime(2021, 07, 01);
            o1.BookingEnd = new DateTime(2021, 07, 02);
            context.Add(o1);
            context.SaveChanges();
            Order o2 = new Order();
            o2.RessourceId = 1;
            o2.Date = DateTime.Today;
            o2.BookingStart = new DateTime(2021, 07, 11);
            o2.BookingEnd = new DateTime(2021, 07, 22);
            var expected = oc.CheckDate(o2);

            //Assert
            Assert.IsTrue(expected);
        }

        [TestMethod]
        public void TestingIfNewBookingStartsWithinExistingBookingExpectedFalse()
        {

            //Arrange 
            var oc = new OrdersController(context);

            //Act
            Order o1 = new Order();
            o1.RessourceId = 1;
            o1.Date = DateTime.Today;
            o1.BookingStart = new DateTime(2021, 07, 01);
            o1.BookingEnd = new DateTime(2021, 07, 05);
            context.Add(o1);
            context.SaveChanges();
            Order o2 = new Order();
            o2.RessourceId = 1;
            o2.Date = DateTime.Today;
            o2.BookingStart = new DateTime(2021, 07, 2);
            o2.BookingEnd = new DateTime(2021, 07, 22);
            var expected = oc.CheckDate(o2);

            //Assert
            Assert.IsFalse(expected);
        }

        [TestMethod]
        public void TestingIfNewBookingEndsWithinExistingBookingExpectedFalse()
        {

            //Arrange 
            var oc = new OrdersController(context);

            //Act
            Order o1 = new Order();
            o1.RessourceId = 1;
            o1.Date = DateTime.Today;
            o1.BookingStart = new DateTime(2021, 07, 01);
            o1.BookingEnd = new DateTime(2021, 07, 05);
            context.Add(o1);
            context.SaveChanges();
            Order o2 = new Order();
            o2.RessourceId = 1;
            o2.Date = DateTime.Today;
            o2.BookingStart = new DateTime(2021, 06, 27);
            o2.BookingEnd = new DateTime(2021, 07, 03);
            var expected = oc.CheckDate(o2);

            //Assert
            Assert.IsFalse(expected);
        }
    }
}