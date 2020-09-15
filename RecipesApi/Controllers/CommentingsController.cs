using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesApi.Data;
using RecipesApi.Models;

namespace RecipesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentingsController : ControllerBase
    {
        private readonly RecipeContext _context;

        public CommentingsController(RecipeContext context)
        {
            _context = context;
        }

        // GET: api/Commentings
        [HttpGet]
        public async Task<ActionResult> GetCommenting()
        {
            var commentItems = await _context.Commenting.ToListAsync();
            return Ok(commentItems);
        }

        // GET: api/Commentings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commenting>> GetCommenting(int id)
        {
            var commenting = await _context.Commenting.FindAsync(id);

            if (commenting == null)
            {
                return NotFound();
            }

            return commenting;
        }

        // PUT: api/Commentings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommenting(int id, Commenting commenting)
        {
            if (id != commenting.CommentingId)
            {
                return BadRequest();
            }

            _context.Entry(commenting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentingExists(id))
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

        // POST: api/Commentings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Commenting>> PostCommenting(Commenting commenting)
        {
            _context.Commenting.Add(commenting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommenting", new { id = commenting.CommentingId }, commenting);
        }

        // DELETE: api/Commentings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Commenting>> DeleteCommenting(int id)
        {
            var commenting = await _context.Commenting.FindAsync(id);
            if (commenting == null)
            {
                return NotFound();
            }

            _context.Commenting.Remove(commenting);
            await _context.SaveChangesAsync();

            return commenting;
        }

        private bool CommentingExists(int id)
        {
            return _context.Commenting.Any(e => e.CommentingId == id);
        }
    }
}
