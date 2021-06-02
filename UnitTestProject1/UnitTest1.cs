using Microsoft.VisualStudio.TestTools.UnitTesting;
using DelpinWebApi;
using DelpinWebApi.Models;
using DelpinWebApi.Controllers;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange

            var oc = new OrdersController();
            //Act



            //var mockRepo = new Mock<TodoContext>();
            Order o1 = new Order();
            o1.RessourceId = 1;
            o1.Date = DateTime.Today;
            o1.BookingStart = DateTime.Today;
            o1.BookingEnd = new DateTime(2021, 06, 21);
            var expected = oc.CheckDate(o1);

            Assert.IsFalse(expected);

        }
    }
}