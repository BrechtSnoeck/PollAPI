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

            context.Polls.AddRange(
                new Poll
                {
                    Naam = "test1"
                },
                new Poll
                {
                    Naam = "test2"
                }
                );
            context.SaveChanges();

            context.PollGebruikers.AddRange(
                new PollGebruiker
                {
                    PollID = 1,
                    GebruikerID = 1
                },
                new PollGebruiker
                {
                    PollID = 1,
                    GebruikerID = 2
                }
                );
            context.SaveChanges();

            context.Antwoorden.AddRange(
                new Antwoord
                {
                    Uitkomst = "Antwoord 1: test",
                    PollID = 1
                },
                new Antwoord
                { 
                    Uitkomst = "Antwoord 2: werkt het",
                    PollID = 1
                }
                );
            context.SaveChanges();

            context.Stemmen.AddRange(
                new Stem
                {
                    AntwoordID = 1,
                    GebruikerID = 1
                },
                new Stem
                {
                    AntwoordID = 2,
                    GebruikerID = 2
                }
                );
            context.SaveChanges();
        }
    }
}
