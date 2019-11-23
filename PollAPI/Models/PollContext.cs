using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPI.Models
{
    public class PollContext : DbContext
    {
        public PollContext(DbContextOptions<PollContext> options) : base(options)
        {

        }

        public DbSet<Antwoord> Antwoorden { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollGebruiker> PollGebruikers { get; set; }
        public DbSet<Stem> Stemmen { get; set; }
        public DbSet<Relatie> Relaties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");
            modelBuilder.Entity<Poll>().ToTable("Poll");
            modelBuilder.Entity<PollGebruiker>().ToTable("PollGebruiker");
            modelBuilder.Entity<Antwoord>().ToTable("Antwoord");
            modelBuilder.Entity<Stem>().ToTable("Stem");

            modelBuilder.Entity<Relatie>()
                .HasKey(e => new { e.RelatieID });

            modelBuilder.Entity<Relatie>()
                .HasOne(e => e.Zender)
                .WithMany(e => e.VerzondenVrienden)
                .HasForeignKey(e => e.GebruikerID_1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relatie>()
                .HasOne(e => e.Ontvanger)
                .WithMany(e => e.OntvangenVrienden)
                .HasForeignKey(e => e.GebruikerID_2)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
