using System;
using API.Models.DTOs;

namespace API.Services;

public interface IHelperServices
{
   public ReturnRegisteredUserDTO RegisterUser(UserRegisterDTO userCreds);
   public Task<bool> UserExistsAsync(string user);
   public Task<bool> UserIsAdmin(string authHeader);
   public string RemoveBearerWord(string? key);
}
