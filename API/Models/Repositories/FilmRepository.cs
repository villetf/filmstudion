using System;
using System.Threading.Tasks;
using API.Data;
using API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Interfaces;

public class FilmRepository : IFilmRepository
{
   private readonly AppDbContext _context;
   public FilmRepository(AppDbContext appDbContext)
   {
      _context = appDbContext;
   }

   public async Task<Film> AddNewFilm(Film film)
   {
      _context.Films.Add(film);
      await _context.SaveChangesAsync();
      return film;
   }

   public async Task<Film> EditFilm(Film film)
   {
      _context.Films.Update(film);
      await _context.SaveChangesAsync();
      return film;
   }

   public async Task<IEnumerable<Film>> GetAllFilms()
   {
      return await _context.Films.ToListAsync();
   }

   public async Task<Film?> GetFilmById(int id)
   {
      return await _context.Films.FindAsync(id);
   }
}
