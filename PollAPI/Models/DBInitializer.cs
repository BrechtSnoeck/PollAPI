using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class DBInitializer
    {
        public static void Initialize(PollContext context)
        {
            context.Database.EnsureCreated();
            // Look for any verkiezingen.
            if (context.Gebruikers.Any())
            {
                return;   // DB has been seeded
            }
            context.Gebruikers.AddRange(
              new Gebruiker
              {
                  Email = "brecht@thomasmore.be",
                  Wachtwoord = "123",
                  Gebruikersnaam = "brecht"
              },
              new Gebruiker
              {
                  Email = "ruben@thomasmore.be",
                  Wachtwoord = "123",
                  Gebruikersnaam = "ruben"
              }
              );
            
            context.SaveChanges();
        }
    }
}
