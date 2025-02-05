using System;
using API.Models.Entities;
using API.Models.Interfaces;

namespace API.Models.Repositories;

public class UserRepository : IUserRepository
{
   public Task<bool> AuthenticateUser()
   {
      throw new NotImplementedException();
   }

   public Task<User> CreateNewUser()
   {
      throw new NotImplementedException();
   }

   public Task<User> GetUserByEmail(string email)
   {
      throw new NotImplementedException();
   }
}
