using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public interface IRentalsRepository
{
   public Task<IEnumerable<Rental>> GetRentalsByStudio(int id);
   public void MakeRental(Film film, int studioId);
   public void ReturnRental(Film film, int studioId);
   public bool StudioRentsThisMovie(int studioId, int filmId);
   public void DecreaseStock(Film film);
}
