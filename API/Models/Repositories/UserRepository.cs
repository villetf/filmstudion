using System;
using API.Data;
using API.Models.Entities;
using API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Repositories;

public class UserRepository : IUserRepository
{
   private readonly AppDbContext _context;
   public UserRepository(AppDbContext appDbContext)
   {
      _context = appDbContext;
   }
   public Task<bool> AuthenticateUser()
   {
      throw new NotImplementedException();
   }

   public void CreateNewUser(User user)
   {
      _context.Users.Add(user);
      _context.SaveChanges();
   }

   public async Task<User?> GetUserByUsername(string username)
   {
      return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
   }

   public async Task<User?> GetUserByGuid(string guid)
   {
      return await _context.Users.FirstOrDefaultAsync(u => u.UserGuid.ToString() == guid);
   }

   public async Task<User?> GetUserByStudioId(int id)
   {
      return await _context.Users.FirstOrDefaultAsync(u => u.FilmStudioId == id);
   }
}
