using System;
using API.Models.DTOs;

namespace API.Services;

public interface IUserService
{
   public ReturnRegisteredUserDTO RegisterUser(UserRegisterDTO userCreds);
   public Task<bool> UserExistsAsync(string user);
}
