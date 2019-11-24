using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollAPI.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatieController : ControllerBase
    {
        private readonly PollContext _context;

        public RelatieController(PollContext context)
        {
            _context = context;
        }

        // GET: api/Relatie
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relatie>>> GetRelaties()
        {
            return await _context.Relaties.ToListAsync();
            
        }

        // GET: api/Relatie/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Relatie>>> GetRelaties(int id)
        {
            var relaties = _context.Relaties
                .Include(r => r.Ontvanger)
                .Include(r => r.Zender)
                .Where(r => r.Ontvanger.GebruikerID == id || r.Zender.GebruikerID == id);

            if (relaties == null)
            {
                return NotFound();
            }

            return await relaties.ToListAsync();
        }

        [Authorize]
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VriendschapDto>>> GetRelatieVerzoeken(int id)
        {
            var relaties = _context.Relaties
                .Where(r => r.GebruikerID_2 == id || (r.GebruikerID_1 == id && r.status == true))
                .Include(r => r.Zender)
                .Select(r => 
                    new VriendschapDto
                    { 
                        RelatieID = r.RelatieID,
                        GebruikersnaamZender = r.Zender.Gebruikersnaam,
                        GebruikersIDZender = r.Zender.GebruikerID,
                        GebruikersnaamOntvanger = r.Ontvanger.Gebruikersnaam,
                        GebruikersIDOntvanger = r.Ontvanger.GebruikerID,
                        status = r.status
                    });
                

            if (relaties == null)
            {
                return NotFound();
            }

            return await relaties.ToListAsync();
        }

        // PUT: api/Relatie/5
        [Authorize]
        [Route("action/{id}")]
        [HttpPut]
        public async Task<ActionResult<Relatie>> PutRelatie(int id, Relatie relatie)
        {
            if (id != relatie.RelatieID)
            {
                return BadRequest();
            }

            _context.Entry(relatie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelatieExists(id))
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

        // POST: api/Relatie
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Relatie>> PostRelatie(RelatieDto relatieDto)
        {
            var relatie = new Relatie();
            var bestaatGebruiker = _context.Gebruikers.SingleOrDefault(g => g.Email == relatieDto.Email && g.GebruikerID != relatieDto.ZenderID);
            if (bestaatGebruiker == null)
            {
                return BadRequest(new { message = "Er is geen gebruiker met dit e-mailadres!(Of je voegde jezelf toe als vriend :) )" });

                //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                //var client = new SendGridClient(apiKey);
                //var from = new EmailAddress("r0621517@student.thomasmore.be", "Brecht Snoeck");
                //var subject = "Sending with SendGrid is Fun";
                //var to = new EmailAddress("brekke.snoeck@hotmail.com", "Brecht");
                //var plainTextContent = "and easy to do anywhere, even with C#";
                //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                //var response = await client.SendEmailAsync(msg);

                //return relatie;
            } 
            else
            {
                var bestaatRelatie1 = _context.Relaties.SingleOrDefault(r => r.GebruikerID_1 == relatieDto.ZenderID && r.GebruikerID_2 == bestaatGebruiker.GebruikerID);
                var bestaatRelatie2 = _context.Relaties.SingleOrDefault(r => r.GebruikerID_1 == bestaatGebruiker.GebruikerID && r.GebruikerID_2 == relatieDto.ZenderID);
                if (bestaatRelatie1 == null && bestaatRelatie2 == null)
                {
                    relatie = new Relatie
                    {
                        GebruikerID_1 = relatieDto.ZenderID,
                        GebruikerID_2 = bestaatGebruiker.GebruikerID,
                        status = false
                    };
                    _context.Relaties.Add(relatie);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetRelaties", new { id = relatie.RelatieID }, relatie);
                } 
                else
                {
                    if (bestaatRelatie2 == null)
                    {
                        return BadRequest(new { message = "Je bent al toegevoegd door deze persoon! Kijk of je hieronder een verzoek kreeg!" });
                    } else
                    {
                        return BadRequest(new { message = "Je hebt deze gebruiker al toegevoegd als vriend! Het verzoek moet nog geacepteerd worden!" });
                    }
                    
                }
               
            }
        }

        // DELETE: api/Relatie/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Relatie>> DeleteRelatie(int id)
        {
            var relatie = await _context.Relaties.FindAsync(id);
            if (relatie == null)
            {
                return NotFound();
            }

            _context.Relaties.Remove(relatie);
            await _context.SaveChangesAsync();

            return relatie;
        }

        private bool RelatieExists(int id)
        {
            return _context.Relaties.Any(e => e.RelatieID == id);
        }
    }
}
