﻿using Microsoft.EntityFrameworkCore;
using Taboo.Entities;

namespace Taboo.DAL
{
    public class TabuDbContext : DbContext
    {
        public TabuDbContext(DbContextOptions opt) : base(opt)
        {
        }
        public DbSet<Language> Languages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(model=>
            {
                model.HasKey(model=> model.Code);
                model.Property(x => x.Code)
                  .IsRequired()
                  .HasMaxLength(2);
                model.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);
                model.Property(x=> x.Icon)
                .IsRequired()
                .HasMaxLength(128);


            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
