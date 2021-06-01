﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DelpinWebApi.Models;

namespace DelpinWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TodoContext _context;

        public OrdersController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            if (order.BookingStart.CompareTo(order.BookingEnd) > 0)
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
            }
            else
            {
                return NoContent();
            }
            //_context.Orders.Add(order);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
        public bool CheckDate(Order order)
        {

            if (CompareRessourceIdToAllOrders(order.RessourceId))
            {
                
                List<Order> ordreListe = _context.Orders.ToList();
                order.BookingStart.CompareTo(ordreListe[ReturnOrderId()].);
                //int id = ReturnOrderId(order.OrderId);
                return true;
            }
            
           
        }
        public bool CompareRessourceIdToAllOrders(int ressourceId)
        {
            
            List<Order> ordreListe = _context.Orders.ToList();
            for (int i = 0; i < ordreListe.Count; i++)
            {
                if (ressourceId == ordreListe[i].RessourceId)
                {
                    ReturnOrderId(ordreListe[i].OrderId);
                    return true;
                   
                }
                
            }
            return false;
           
        }
        public int ReturnOrderId(int ordreId)
        {
            int eksisterendeOrdre = ordreId;
            return eksisterendeOrdre;
        }
    }
}
