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
}
