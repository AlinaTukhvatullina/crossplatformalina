using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlinaApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace AlinaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipesController : ControllerBase
    {
        private readonly AlinaAppContext _context;

        public PipesController(AlinaAppContext context)
        {
            _context = context;
        }
        //функция, которая возвращает только секретные трубы
        [HttpGet("onlydefective")]
        [Authorize(Roles ="admin")]
        public IEnumerable<Pipe> GetOnlyDefectivePipes()
        {
            return _context.GetDefectivePipes();
        }

        //функция, которая возвращает несекретные трубы
        [HttpGet]
        [Authorize]
        public  IEnumerable<Pipe> Getpipes()
        {
            return _context.GetPipes();
        }

        //функция, которая возвращает все трубы
        [HttpGet("defective")]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<IEnumerable<Pipe>>> GetAllPipes()
        {
            return await _context.Pipes.ToListAsync();
        }

        // GET: api/Pipes/5
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Pipe>> GetPipe(long id)
        {
            var pipe = await _context.Pipes.FindAsync(id);

            if (pipe == null)
            {
                return NotFound();
            }

            return pipe;
        }

        // PUT: api/Pipes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPipe(long id, Pipe pipe)
        {
            if (id != pipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(pipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PipeExists(id))
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

        // POST: api/Pipes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("defective")]
        [Authorize(Roles ="admin")]
        public string PostDefectivePipe(Pipe pipe)
        {
            pipe.defective = true;
           
            return _context.PostPipe(pipe);

            
        }

        [HttpPost]
        [Authorize]
        public string PostPipe(Pipe pipe)
        {
            pipe.defective = false;
            
            return _context.PostPipe(pipe);

        }

        

        // DELETE: api/Pipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pipe>> DeletePipe(long id)
        {
            var pipe = await _context.Pipes.FindAsync(id);
            if (pipe == null)
            {
                return NotFound();
            }

            _context.Pipes.Remove(pipe);
            await _context.SaveChangesAsync();

            return pipe;
        }

        private bool PipeExists(long id)
        {
            return _context.Pipes.Any(e => e.Id == id);
        }
    }
}
