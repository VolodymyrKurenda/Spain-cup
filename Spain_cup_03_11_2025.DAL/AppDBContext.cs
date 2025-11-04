using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spain_cup_03_11_2025.DAL.Entities;



namespace Spain_cup_03_11_2025
{
    public class AppDbContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Club_Match> Club_Matches { get; set; }

        public DbSet<Goal> Goals { get; set; }

        private string ConectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=C#Practice;Integrated Security=True;Connect Timeout=30;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Club_Match>()
            .HasOne(cm => cm.Club1)
            .WithMany(c => c.Club1_Matches)
            .HasForeignKey(cm => cm.Club1_id)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Club_Match>()
                .HasOne(cm => cm.Club2)
                .WithMany(c => c.Club2_Matches)
                .HasForeignKey(cm => cm.Club2_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Club_Match>()
                .HasOne(cm => cm.Match)
                .WithMany(m => m.Club_Match)
                .HasForeignKey(cm => cm.MatchID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
