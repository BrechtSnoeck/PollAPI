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
                  Wachtwoord = "123456",
                  Gebruikersnaam = "brecht"
              },
              new Gebruiker
              {
                  Email = "ruben@thomasmore.be",
                  Wachtwoord = "123456",
                  Gebruikersnaam = "ruben"
              },
              new Gebruiker
              {
                  Email = "karen@thomasmore.be",
                  Wachtwoord = "123456",
                  Gebruikersnaam = "karen"
              },
              new Gebruiker
              {
                  Email = "boz@thomasmore.be",
                  Wachtwoord = "123456",
                  Gebruikersnaam = "Boz"
              }
              );
            context.SaveChanges();

            context.Polls.AddRange(
                new Poll
                {
                    Naam = "Gezelschapspelletjes"
                },
                new Poll
                {
                    Naam = "Games"
                }
                ,
                new Poll
                {
                    Naam = "Fastfood"
                }
                ,
                new Poll
                {
                    Naam = "Ijsjes"
                }
                );
            context.SaveChanges();

            context.PollGebruikers.AddRange(
                new PollGebruiker
                {
                    PollID = 1,
                    GebruikerID = 1,
                    Gestemd = true,
                    IsAdmin = true,
                    IsActief =true
                },
                new PollGebruiker
                {
                    PollID = 1,
                    GebruikerID = 2,
                    Gestemd = true,
                    IsAdmin = false,
                    IsActief = true
                },
                new PollGebruiker
                {
                    PollID = 2,
                    GebruikerID = 1,
                    Gestemd = true,
                    IsAdmin = true,
                    IsActief = true
                }
                ,
                new PollGebruiker
                {
                    PollID = 2,
                    GebruikerID = 2,
                    Gestemd = false,
                    IsAdmin = false,
                    IsActief = true
                }
                ,
                new PollGebruiker
                {
                    PollID = 3,
                    GebruikerID = 1,
                    Gestemd = false,
                    IsAdmin = true,
                    IsActief = true
                },
                new PollGebruiker
                {
                    PollID = 3,
                    GebruikerID = 2,
                    Gestemd = false,
                    IsAdmin = false,
                    IsActief = true
                },
                new PollGebruiker
                {
                    PollID = 4,
                    GebruikerID = 1,
                    Gestemd = false,
                    IsAdmin = false,
                    IsActief = true
                },
                new PollGebruiker
                {
                    PollID = 4,
                    GebruikerID = 2,
                    Gestemd = false,
                    IsAdmin = true,
                    IsActief = true
                }
                );
            context.SaveChanges();

            context.Antwoorden.AddRange(
                new Antwoord
                {
                    Optie = "Kolonisten van Catan",
                    PollID = 1
                },
                new Antwoord
                {
                    Optie = "Yahtzee",
                    PollID = 1
                },
                new Antwoord
                {
                    Optie = "League",
                    PollID = 2
                },
                new Antwoord
                {
                    Optie = "Civ 5",
                    PollID = 2
                },
                new Antwoord
                {
                    Optie = "Frietjes",
                    PollID = 3
                },
                new Antwoord
                {
                    Optie = "Pizza",
                    PollID = 3
                },
                new Antwoord
                {
                    Optie = "Vanille",
                    PollID = 4
                },
                new Antwoord
                {
                    Optie = "Banaan",
                    PollID = 4
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
                ,
                new Stem
                {
                    AntwoordID = 3,
                    GebruikerID = 1
                }
                ,
                new Stem
                {
                    AntwoordID = 4,
                    GebruikerID = 1
                }
                );
            context.SaveChanges();

            context.Relaties.AddRange(
                new Relatie
                {
                    GebruikerID_1 = 1,
                    GebruikerID_2 = 2,
                    status = true
                },
                new Relatie
                {
                    GebruikerID_1 = 1,
                    GebruikerID_2 = 3,
                    status = true
                },
                new Relatie
                {
                    GebruikerID_1 = 1,
                    GebruikerID_2 = 4,
                    status = true
                }
                );
            context.SaveChanges();
        }
    }
}
