using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public interface IRentalsRepository
{
   public Task<IEnumerable<Rental>> GetRentalsByStudio(int id);
   public void MakeRental(Film film, int studioId);
   public void ReturnRental(Rental rental, int studioId);
   public bool StudioRentsThisMovie(int studioId, int filmId);
   public void DecreaseStock(Film film);
   public void IncreaseStock(Film film);
   public Task<Rental> GetRentedFilm(int filmId, int studioId); 
}
