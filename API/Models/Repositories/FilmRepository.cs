using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public class FilmRepository : IFilmRepository
{
   public void AddNewFilm(Film film)
   {
      throw new NotImplementedException();
   }

   public Task<Film> EditFilm(Film film)
   {
      throw new NotImplementedException();
   }

   public Task<IEnumerable<Film>> GetAllFilms()
   {
      throw new NotImplementedException();
   }

   public Task<Film> GetFilmById(int id)
   {
      throw new NotImplementedException();
   }

   public void MakeRental(Film film, int studioId)
   {
      throw new NotImplementedException();
   }

   public void ReturnRental(Film film, int studioId)
   {
      throw new NotImplementedException();
   }
}
