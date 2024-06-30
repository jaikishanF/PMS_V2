﻿using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the ApplicationUser to EmergencyDetails relationship
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.EmergencyDetails)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey<ApplicationUser>(a => a.EmergencyDetailsId)
                .OnDelete(DeleteBehavior.Cascade);  // Ensures that deleting an ApplicationUser deletes the associated EmergencyDetails

            // Configure the ApplicationUser to InsuranceDetails relationship
            builder.Entity<ApplicationUser>()
                .HasOne(a => a.InsuranceDetails)
                .WithOne(i => i.ApplicationUser)
                .HasForeignKey<ApplicationUser>(a => a.InsuranceDetailsId)
                .OnDelete(DeleteBehavior.Cascade);  // Ensures that deleting an ApplicationUser deletes the associated InsuranceDetails
        }

        public DbSet<EmergencyDetails> EmergencyDetails { get; set; }
        public DbSet<InsuranceDetails> InsuranceDetails { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingAvailability> BookingAvailability { get; set; }
    }
}
