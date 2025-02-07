using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public interface IUserRepository
{
   public Task<bool> AuthenticateUser();
   public void CreateNewUser(User user);
   public Task<User?> GetUserByUsername(string username);
   public Task<User?> GetUserByGuid(string guid);
}
