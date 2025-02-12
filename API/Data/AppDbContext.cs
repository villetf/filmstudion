using System;
using API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext : DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
   {
      Database.EnsureCreated();
   }

   public DbSet<Film> Films { get; set; }
   public DbSet<FilmStudio> FilmStudios { get; set; }
   public DbSet<Rental> Rentals { get; set; }
   public DbSet<User> Users { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<Film>().HasData(
         new Film
         {
            Id = 1,
            Name = "Titanic",
            ReleaseYear = 1997,
            AvailableCopies = 10
         },
         new Film
         {
            Id = 2,
            Name = "The Matrix",
            ReleaseYear = 1999,
            AvailableCopies = 20
         },
         new Film
         {
            Id = 3,
            Name = "Inception",
            ReleaseYear = 2010,
            AvailableCopies = 25
         },
         new Film
         {
            Id = 4,
            Name = "Nyckeln till frihet",
            ReleaseYear = 1994,
            AvailableCopies = 30
         }
      );
   }
}
