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
}
