using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlinaApp.Models;

namespace AlinaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactorysController : ControllerBase
    {
        private readonly AlinaAppContext _context;

        public FactorysController(AlinaAppContext context)
        {
            _context = context;
        }

        // GET: api/Factorys
        [HttpGet]
        public IEnumerable<Factorys> GetFactoryses()
        {
            return _context.GetFactorys();
        }

        // GET: api/Factorys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factorys>> GetFactorys(long id)
        {
            var factorys = await _context.Factoryses.FindAsync(id);

            if (factorys == null)
            {
                return NotFound();
            }

            return factorys;
        }

        // PUT: api/Factorys/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactorys(long id, Factorys factorys)
        {
            if (id != factorys.Id)
            {
                return BadRequest();
            }

            _context.Entry(factorys).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactorysExists(id))
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

        // POST: api/Factorys
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public string PostFactorys(Factorys factorys)
        {
            try
            {
                _context.Factoryses.Add(factorys);
                _context.SaveChanges();
                return "Factory created";
            }
            catch
            {
                return "ERROR::Factory was not created";
            }
            
        }

        // DELETE: api/Factorys/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Factorys>> DeleteFactorys(long id)
        {
            var factorys = await _context.Factoryses.FindAsync(id);
            if (factorys == null)
            {
                return NotFound();
            }

            _context.Factoryses.Remove(factorys);
            await _context.SaveChangesAsync();

            return factorys;
        }

        private bool FactorysExists(long id)
        {
            return _context.Factoryses.Any(e => e.Id == id);
        }
    }
}
