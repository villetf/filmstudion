using System;
using API.Data;
using API.Models.Entities;
using API.Models.Interfaces;

namespace API.Models.Repositories;

public class FilmStudioRepository : IFilmStudioRepository
{
   private readonly AppDbContext _context;
   public FilmStudioRepository(AppDbContext appDbContext)
   {
      _context = appDbContext;
   }


   public async Task<FilmStudio> CreateNewStudio(FilmStudio studio)
   {
      await _context.FilmStudios.AddAsync(studio);
      await _context.SaveChangesAsync();
      return studio;
   }

   public Task<IEnumerable<FilmStudio>> GetAllStudios()
   {
      throw new NotImplementedException();
   }

   public Task<IEnumerable<Rental>> GetRentalsByStudio(int id)
   {
      throw new NotImplementedException();
   }

   public Task<FilmStudio> GetStudioById()
   {
      throw new NotImplementedException();
   }
}
