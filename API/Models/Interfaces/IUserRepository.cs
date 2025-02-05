using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public interface IUserRepository
{
   public Task<bool> AuthenticateUser();
   public Task<User> CreateNewUser();
   public Task<User> GetUserByEmail(string email);
}
