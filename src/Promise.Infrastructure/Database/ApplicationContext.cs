﻿using Microsoft.EntityFrameworkCore;
using Promise.Domain.Models;

namespace Promise.Infrastructure.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Report> Reports { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
