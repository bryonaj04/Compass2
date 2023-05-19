using System;
using Compass.Models;
using Microsoft.EntityFrameworkCore;
namespace Compass.Data
{
    public class SchedulingContext : DbContext
    {
        public SchedulingContext(DbContextOptions<SchedulingContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Room> Rooms { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Meeting>().ToTable("Meeting");
            modelBuilder.Entity<Room>().ToTable("Room");
        }
    }
}

