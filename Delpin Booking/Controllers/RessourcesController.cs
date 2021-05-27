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
    public class RessourcesController : Controller
    {
        
        private IEnumerable<Department> department = null;

        // GET: Ressources
        public async Task<IActionResult> Index()
        {
            IEnumerable<Ressource> ressource = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync("Ressources");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Ressource>>();
                    readJob.Wait();
                    ressource = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    ressource = Enumerable.Empty<Ressource>();
                    ModelState.AddModelError(string.Empty, "Server fejl rip.");
                }
            }   return View(ressource);
            
        }

        // GET: Ressources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ressource ressource = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync($"Ressources/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Ressource>();
                    readJob.Wait();
                    ressource = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    
                    ModelState.AddModelError(string.Empty, "Server fejl rip.");
                }
                return View(ressource);
            }
        }

        // GET: Ressources/Create
        public IActionResult Create()
        {
            IEnumerable<Department> department = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync("Departments");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<IList<Department>>();
                    readJob.Wait();
                    department = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    
                    ModelState.AddModelError(string.Empty, "Server fejl rip.");
                }
                ViewData["DepartmentId"] = new SelectList(department, "DepartmentId", "DepartmentId");
                return View();
            }
        }

        // POST: Ressources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RessourceId,DepartmentId,Name,Link,Type,Location,Price")] Ressource ressource)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var postJob = client.PostAsJsonAsync<Ressource>("Ressources", ressource);
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
                return View(ressource);
            }
            
        }

        // GET: Ressources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ressource ressource = null;
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync($"Ressources/{id}");
                var responseTask2 = client.GetAsync($"Departments");
                
                responseTask2.Wait();

                var result = responseTask.Result;
                var result2 = responseTask2.Result;
               
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Ressource>();
                    readJob.Wait();
                    ressource = readJob.Result;
                }
                else
                {
                    //Return the error code here

                    ModelState.AddModelError(string.Empty, "Responsetask 1 fejl rip.");
                }
                if (result2.IsSuccessStatusCode)
                {
                    var readJob2 = result2.Content.ReadAsAsync<IList<Department>>();
                    readJob2.Wait();
                    department = readJob2.Result;
                }
                else
                {
                    //Return the error code here

                    ModelState.AddModelError(string.Empty, "Responsetask 2 fejl rip.");
                }
               
                ViewData["DepartmentId"] = new SelectList(department, "DepartmentId", "DepartmentId", ressource.DepartmentId);
                
                return View(ressource);
            }
            
        }

        // POST: Ressources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RessourceId,DepartmentId,Name,Link,Type,Location,Price")] Ressource ressource)
        {
            if (id != ressource.RessourceId)
            {
                return NotFound();
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var postJob = client.PutAsJsonAsync<Ressource>($"Ressources/{ressource.RessourceId}", ressource);
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
                ViewData["DepartmentId"] = new SelectList(department, "DepartmentId", "DepartmentId", ressource.DepartmentId);
                return View(ressource);
            }

        }

        // GET: Ressources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ressource ressource = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.GetAsync($"Ressources/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Ressource>();
                    readJob.Wait();
                    ressource = readJob.Result;
                }
                else
                {
                    //Return the error code here
                    //customer = EnumerableEmpty<Customer>();
                    ModelState.AddModelError(string.Empty, "Server fejl rip.");
                }
                return View(ressource);

            }

        }

        // POST: Ressources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Ressource ressource = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44362/api/");
                var responseTask = client.DeleteAsync($"Ressources/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readJob = result.Content.ReadAsAsync<Ressource>();
                    readJob.Wait();
                    ressource = readJob.Result;
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
        //// GET: Ressources/Book/5
        //public async Task<IActionResult> Book(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Ressource ressource = null;
        //    IEnumerable<Department> department = null;
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:44362/api/");
        //        var responseTask = client.GetAsync($"Ressources/{id}");
        //        var responseTask2 = client.GetAsync($"Departments");

        //        responseTask2.Wait();

        //        var result = responseTask.Result;
        //        var result2 = responseTask2.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readJob = result.Content.ReadAsAsync<Ressource>();
        //            readJob.Wait();
        //            ressource = readJob.Result;
        //        }
        //        else
        //        {
        //            //Return the error code here

        //            ModelState.AddModelError(string.Empty, "Responsetask 1 fejl rip.");
        //        }
        //        if (result2.IsSuccessStatusCode)
        //        {
        //            var readJob2 = result2.Content.ReadAsAsync<IList<Department>>();
        //            readJob2.Wait();
        //            department = readJob2.Result;
        //        }
        //        else
        //        {
        //            //Return the error code here

        //            ModelState.AddModelError(string.Empty, "Responsetask 2 fejl rip.");
        //        }

        //        ViewData["DepartmentId"] = new SelectList(department, "DepartmentId", "DepartmentId", ressource.DepartmentId);

        //        return View();
        //    }

        //}

        //private bool RessourceExists(int id)
        //{
        //    return _context.Ressources.Any(e => e.RessourceId == id);
        //}
    }
}
