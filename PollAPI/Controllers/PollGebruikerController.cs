﻿using System;
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
    public class PollGebruikerController : ControllerBase
    {
        private readonly PollContext _context;

        public PollGebruikerController(PollContext context)
        {
            _context = context;
        }

        // GET: api/PollGebruiker
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollGebruiker>>> GetPollGebruikers()
        {
            //return await _context.PollGebruikers.ToListAsync();

            var pollGebruikers = _context.PollGebruikers
                .Include(p => p.Poll);
               

            return await pollGebruikers.ToListAsync();
        }

        // GET: api/PollGebruiker/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PollGebruiker>>> GetPollGebruiker(int id)
        {
            var pollGebruikers =  _context.PollGebruikers
                .Include(pg => pg.Poll)
                .ThenInclude(p => p.Antwoorden)
                .ThenInclude(a => a.Stemmen)
                .ThenInclude(g => g.Gebruiker)
                .Where(p => p.GebruikerID == id && p.IsActief == true);

            if (pollGebruikers == null)
            {
                return NotFound();
            }

            return await pollGebruikers.ToListAsync();
        }

        // PUT: api/PollGebruiker/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollGebruiker(int id, PollGebruiker pollGebruiker)
        {
            if (id != pollGebruiker.PollGebruikerID)
            {
                return BadRequest();
            }

            _context.Entry(pollGebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollGebruikerExists(id))
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

        // POST: api/PollGebruiker
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<PollGebruiker>> PostPollGebruiker(PollGebruiker pollGebruiker)
        {
            _context.PollGebruikers.Add(pollGebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollGebruiker", new { id = pollGebruiker.PollGebruikerID }, pollGebruiker);
        }

        // DELETE: api/PollGebruiker/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<PollGebruiker>> DeletePollGebruiker(int id)
        {
            var pollGebruiker = await _context.PollGebruikers.FindAsync(id);
            if (pollGebruiker == null)
            {
                return NotFound();
            }

            _context.PollGebruikers.Remove(pollGebruiker);
            await _context.SaveChangesAsync();

            return pollGebruiker;
        }

        private bool PollGebruikerExists(int id)
        {
            return _context.PollGebruikers.Any(e => e.PollGebruikerID == id);
        }
    }
}
