using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Delpin_Booking.Data;
using Delpin_Booking.Models;
using System.Net.Http;

namespace Delpin_Booking.Controllers
{
    public class OrdersController : Controller
    {
       private IEnumerable<Customer> customer = null;
       private IEnumerable<Ressource> ressource = null;
        

        // GET: Orders
        public ActionResult Index()
        {
            IEnumerable<Order> order = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync("Orders");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Order>>();
                    readJob.Wait();
                    order = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    order = Enumerable.Empty<Order>();
                    ModelState.AddModelError(string.Empty, "Server fejl rip.");
                }
                return View(order);

            }
        }


        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Order order = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync($"Orders/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Order>();
                    readJob.Wait();
                    order = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    //customer = EnumerableEmpty<Customer>();
                    ModelState.AddModelError(string.Empty, "Server fejl rip.");
                }
                return View(order);
            }

            
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");

                var responseTask2 = client.GetAsync($"Customers");
                var responseTask3 = client.GetAsync($"Ressources");
                responseTask3.Wait();


                var result2 = responseTask2.Result;
                var result3 = responseTask3.Result;
                if (result2.IsSuccessStatusCode)
                {
                    var readJob2 = result2.Content.ReadAsAsync<IList<Customer>>();
                    readJob2.Wait();
                    customer = readJob2.Result;
                }
                else
                {
                    //Return the error code here

                    ModelState.AddModelError(string.Empty, "Responsetask 2 fejl rip.");
                }
                if (result3.IsSuccessStatusCode)
                {
                    var readJob3 = result3.Content.ReadAsAsync<IList<Ressource>>();
                    readJob3.Wait();
                    ressource = readJob3.Result;
                }
                else
                {
                    //Return the error code here

                    ModelState.AddModelError(string.Empty, "Responsetask 3 fejl rip.");
                }
                ViewData["CustomerId"] = new SelectList(customer, "CustomerId", "CustomerId");
                ViewData["RessourceId"] = new SelectList(ressource, "RessourceId", "RessourceId");
                return View();
            }
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,RessourceId,Date,BookingStart,BookingEnd,Price")] Order order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var postJob = client.PostAsJsonAsync<Order>("Orders", order);
                postJob.Wait();
                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "yo recked its crashed fool");
                }
                
                return View(order);
            }
 
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            Order order = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync($"Orders/{id}");
                var responseTask2 = client.GetAsync($"Customers");
                var responseTask3 = client.GetAsync($"Ressources");
                responseTask3.Wait();

                var result = responseTask.Result;
                var result2 = responseTask2.Result;
                var result3 = responseTask3.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Order>();
                    readJob.Wait();
                    order = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    
                    ModelState.AddModelError(string.Empty, "Responsetask 1 fejl rip.");
                }
                if (result2.IsSuccessStatusCode)
                {
                    var readJob2 = result2.Content.ReadAsAsync<IList<Customer>>();
                    readJob2.Wait();
                    customer = readJob2.Result;
                }
                else
                {
                    //Return the error code here
                    
                    ModelState.AddModelError(string.Empty, "Responsetask 2 fejl rip.");
                }
                if (result3.IsSuccessStatusCode)
                {
                    var readJob3 = result3.Content.ReadAsAsync<IList<Ressource>>();
                    readJob3.Wait();
                    ressource = readJob3.Result;
                }
                else
                {
                    //Return the error code here
                    
                    ModelState.AddModelError(string.Empty, "Responsetask 3 fejl rip.");
                }
                ViewData["CustomerId"] = new SelectList(customer, "CustomerId", "CustomerId", order.CustomerId);
                ViewData["RessourceId"] = new SelectList(ressource, "RessourceId", "RessourceId", order.RessourceId);
                return View(order);
            }
            
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,BookingStart,BookingEnd,RessourceId,Date,Price")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var postJob = client.PutAsJsonAsync<Order>($"Orders/{order.OrderId}", order);
                postJob.Wait();
                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "yo recked its crashed fool");
                }
            }
            
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            Order order = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync($"Orders/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Order>();
                    readJob.Wait();
                    order = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    //customer = EnumerableEmpty<Customer>();
                    ModelState.AddModelError(string.Empty, "Server fejl rip.");
                }
                return View(order);

            }
            
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Order order = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.DeleteAsync($"Orders/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Order>();
                    readJob.Wait();
                    order = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    //customer = EnumerableEmpty<Customer>();
                    ModelState.AddModelError(string.Empty, "Server fejl rip.");
                }

                return RedirectToAction(nameof(Index));
            }

        }

        //private bool OrderExists(int id)
        //{
        //    return _context.Orders.Any(e => e.OrderId == id);
        //}
    }
}
