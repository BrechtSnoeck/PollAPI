using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollAPI.Models;

namespace PollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly PollContext _context;

        public PollController(PollContext context)
        {
            _context = context;
        }

        // GET: api/Poll
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poll>>> GetPolls()
        {
            //return await _context.Polls.ToListAsync();

            var polls = _context.Polls
                 .Include(poll => poll.PollGebruikers)
                 .ThenInclude(pg => pg.Gebruiker);

            return await polls.ToListAsync();
        }


        // GET: api/Poll/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetPoll(int id)
        {
            var poll = await _context.Polls.FindAsync(id);

            if (poll == null)
            {
                return NotFound();
            }

            return poll;
        }

        // PUT: api/Poll/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoll(int id, Poll poll)
        {
            if (id != poll.PollID)
            {
                return BadRequest();
            }

            _context.Entry(poll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollExists(id))
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

        // POST: api/Poll
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Poll>> PostPoll(PollDto pollDto)
        {
            var poll = new Poll
            {
                Naam = pollDto.Naam
            };
            _context.Polls.Add(poll);

            await _context.SaveChangesAsync();

            foreach (var optie in pollDto.Opties)
            {
                _context.Antwoorden
                    .Add(
                    new Antwoord
                    {
                        PollID = poll.PollID,
                        Optie = optie.Optie
                    });
            }

            await _context.SaveChangesAsync();

            foreach (int item in pollDto.VriendenIDs)
            {
                _context.PollGebruikers
                    .Add(
                    new PollGebruiker
                    {
                        PollID = poll.PollID,
                        GebruikerID = item,
                        Gestemd = false,
                        IsAdmin = false,
                        IsActief = true
                    });
            }
            await _context.SaveChangesAsync();

            _context.PollGebruikers
                    .Add(
                    new PollGebruiker
                    {
                        PollID = poll.PollID,
                        GebruikerID = pollDto.MakerID,
                        Gestemd = false,
                        IsAdmin = true,
                        IsActief = true
                    });

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoll", new { id = poll.PollID }, poll);
        }

        // DELETE: api/Poll/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Poll>> DeletePoll(int id)
        {
            var poll = await _context.Polls.FindAsync(id);
            if (poll == null)
            {
                return NotFound();
            }

            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();

            return poll;
        }

        private bool PollExists(int id)
        {
            return _context.Polls.Any(e => e.PollID == id);
        }
    }
}
