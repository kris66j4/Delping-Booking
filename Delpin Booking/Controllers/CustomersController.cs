using Delpin_Booking.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Delpin_Booking.Controllers
{
    public class CustomersController : Controller
    {
        //GET: Customers
        public ActionResult Index()
        {
            IEnumerable<Customer> customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync("Customers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Customer>>();
                    readJob.Wait();
                    customer = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    customer = Enumerable.Empty<Customer>();
                    ModelState.AddModelError(string.Empty, "Something went wrong server side. Contact Admin");
                }
                return View(customer);
            }
        }
        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync($"Customers/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Customer>();
                    readJob.Wait();
                    customer = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    
                    ModelState.AddModelError(string.Empty, "Something went wrong server side. Contact Admin");
                }
                return View(customer);
            }
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Name,PhoneNumber")] Customer customer)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var postJob = client.PostAsJsonAsync<Customer>("Customers", customer);
                postJob.Wait();
                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong server side. Contact Admin");
                }
                return View(customer);
            }
        }


        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync($"Customers/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Customer>();
                    readJob.Wait();
                    customer = readJob.Result;
                }
                else
                {
                    //Return the error code here
                   
                    ModelState.AddModelError(string.Empty, "Something went wrong server side. Contact Admin");
                }
                return View(customer);
            }

        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Name,PhoneNumber")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var postJob = client.PutAsJsonAsync<Customer>($"Customers/{customer.CustomerId}", customer);
                postJob.Wait();
                var postResult = postJob.Result;
                if (postResult.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong server side. Contact Admin");
                }
                return View(customer);
            }
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Customer customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync($"Customers/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Customer>();
                    readJob.Wait();
                    customer = readJob.Result;
                }
                else
                {
                    //Return the error code here
                   
                    ModelState.AddModelError(string.Empty, "Something went wrong server side. Contact Admin");
                }
                return View(customer);
                
            }


        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Customer customer = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.DeleteAsync($"Customers/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Customer>();
                    readJob.Wait();
                    customer = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    //customer = EnumerableEmpty<Customer>();
                    ModelState.AddModelError(string.Empty, "Something went wrong server side. Contact Admin");
                }
               
                return RedirectToAction(nameof(Index));
            }

        }
        
    }
}

