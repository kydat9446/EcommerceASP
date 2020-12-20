using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Areas.Admin.Data;
using Shop.Areas.Admin.Models;
using Newtonsoft.Json;
namespace Shop.Areas.API.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeProductsAPIController : ControllerBase
    {
        private readonly DPContext _context;

        public TypeProductsAPIController(DPContext context)
        {
            _context = context;
        }

        [HttpGet]


        // GET: api/TypeProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeProduct>>> GettypeProducts()
        {
            return await _context.typeProduct.ToListAsync();
        }

        // GET: api/TypeProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeProduct>> GetTypeProduct(int id)
        {
            var typeProduct = await _context.typeProduct.FindAsync(id);

            if (typeProduct == null)
            {
                return NotFound();
            }

            return typeProduct;
        }

        // PUT: api/TypeProducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeProduct(int id, TypeProduct typeProduct)
        {
            if (id != typeProduct.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeProductExists(id))
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

        // POST: api/TypeProducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TypeProduct>> PostTypeProduct(TypeProduct typeProduct)
        {
            _context.typeProduct.Add(typeProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeProduct", new { id = typeProduct.Id }, typeProduct);
        }

        // DELETE: api/TypeProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeProduct>> DeleteTypeProduct(int id)
        {
            var typeProduct = await _context.typeProduct.FindAsync(id);
            if (typeProduct == null)
            {
                return NotFound();
            }

            _context.typeProduct.Remove(typeProduct);
            await _context.SaveChangesAsync();

            return typeProduct;
        }

        private bool TypeProductExists(int id)
        {
            return _context.typeProduct.Any(e => e.Id == id);
        }
    }
}
