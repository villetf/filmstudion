using System;
using API.Data;
using API.Models.Entities;
using API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Repositories;

public class RentalsRepository : IRentalsRepository
{
   private readonly AppDbContext _context;
   public RentalsRepository(AppDbContext appDbContext)
   {
      _context = appDbContext;
   }

   public async Task<IEnumerable<Rental>> GetRentalsByStudio(int id)
   {
      return await _context.Rentals.Where(r => r.StudioId == id).ToListAsync();
   }

   public void MakeRental(Film film, int studioId)
   {
      var newRental = new Rental
      {
         StudioId = studioId,
         FilmId = film.Id,
         IsReturned = false
      };

      _context.Rentals.Add(newRental);
      _context.SaveChanges();
   }

   public void ReturnRental(Film film, int studioId)
   {
      throw new NotImplementedException();
   }

   public bool StudioRentsThisMovie(int studioId, int filmId)
   {
      return _context.Rentals.Any(r => (r.StudioId == studioId) && (r.FilmId == filmId) && r.IsReturned == false);
   }

   public void DecreaseStock(Film film)
   {
      film.AvailableCopies--;
      _context.Films.Update(film);
      _context.SaveChanges();
   }
}
