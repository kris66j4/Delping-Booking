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
        private readonly DelpinBookingContext _context;

        public RessourcesController(DelpinBookingContext context)
        {
            _context = context;
        }

        // GET: Ressources
        public async Task<IActionResult> Index()
        {
            var delpinBookingContext = _context.Ressources.Include(r => r.Department);
            return View(await delpinBookingContext.ToListAsync());
        }

        // GET: Ressources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ressource = await _context.Ressources
                .Include(r => r.Department)
                .FirstOrDefaultAsync(m => m.RessourceId == id);
            if (ressource == null)
            {
                return NotFound();
            }

            return View(ressource);
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
                    //department = Enumerable.Empty<Customer>();
                    ModelState.AddModelError(string.Empty, "Server fejl rip.");
                }
                ViewData["DepartmentId"] = new SelectList(department, "DepartmentId", "DepartmentId");
                return View();
                //return View(customer);

            }
        }

        // POST: Ressources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RessourceId,DepartmentId,Name,Type,Location,Price")] Ressource ressource)
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
            //if (ModelState.IsValid)
            //{
            //    _context.Add(ressource);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", ressource.DepartmentId);
            //return View(ressource);
        }

        // GET: Ressources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ressource = await _context.Ressources.FindAsync(id);
            if (ressource == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", ressource.DepartmentId);
            return View(ressource);
        }

        // POST: Ressources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RessourceId,DepartmentId,Name,Type,Location,Price")] Ressource ressource)
        {
            if (id != ressource.RessourceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ressource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RessourceExists(ressource.RessourceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", ressource.DepartmentId);
            return View(ressource);
        }

        // GET: Ressources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ressource = await _context.Ressources
                .Include(r => r.Department)
                .FirstOrDefaultAsync(m => m.RessourceId == id);
            if (ressource == null)
            {
                return NotFound();
            }

            return View(ressource);
        }

        // POST: Ressources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ressource = await _context.Ressources.FindAsync(id);
            _context.Ressources.Remove(ressource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RessourceExists(int id)
        {
            return _context.Ressources.Any(e => e.RessourceId == id);
        }
    }
}
