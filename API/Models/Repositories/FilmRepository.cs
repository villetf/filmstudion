using System;
using API.Data;
using API.Models.Entities;

namespace API.Models.Interfaces;

public class FilmRepository : IFilmRepository
{
   private readonly AppDbContext _context;
   public FilmRepository(AppDbContext appDbContext)
   {
      _context = appDbContext;
   }

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

   public async Task<Film?> GetFilmById(int id)
   {
      return await _context.Films.FindAsync(id);
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
