using System;
using API.Data;
using API.Models.Entities;
using API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

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

   public async Task<IEnumerable<FilmStudio>> GetAllStudios()
   {
      return await _context.FilmStudios.ToListAsync();
   }

   public Task<IEnumerable<Rental>> GetRentalsByStudio(int id)
   {
      throw new NotImplementedException();
   }

   public async Task<FilmStudio?> GetStudioById(int? id)
   {
      if (id == null)
      {
         return null;
      }
      
      return await _context.FilmStudios.FirstOrDefaultAsync(s => s.Id == id);
   }
}
