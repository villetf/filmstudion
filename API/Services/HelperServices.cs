using System;
using System.Text.RegularExpressions;
using API.Controllers;
using API.Models.DTOs;
using API.Models.Entities;
using API.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Services;

public class HelperServices : IHelperServices
{
   private readonly IUserRepository _usersRepository;
   public HelperServices(IUserRepository usersRepository) 
   {
      _usersRepository = usersRepository;
   }

   public ReturnRegisteredUserDTO RegisterUser(UserRegisterDTO userCreds)
   {
      var passwordGenerator = new PasswordHasher<string>();
      Guid userGuid = Guid.NewGuid();

      string role;
      if (userCreds.IsAdmin == true)
      {
         role = "admin";
      }
      else
      {
         role = "studio";
      }

      User user = new User
      {
         Username = userCreds.Username,
         PasswordHash = passwordGenerator.HashPassword(string.Empty, userCreds.Password),
         Role = role,
         FilmStudioId = userCreds.FilmStudioId,
         UserGuid = userGuid
      };

      _usersRepository.CreateNewUser(user);
      var response = new ReturnRegisteredUserDTO
      {
         UserId = user.Id,
         Username = user.Username,
         Role = user.Role
      };

      return response;
   }

   public async Task<bool> UserExistsAsync(string user)
   {
      if (await _usersRepository.GetUserByUsername(user) != null)
      {
         return true;
      }

      return false;
   }

   public async Task<bool> UserIsAdmin(string? authHeader)
   {
      var user = await _usersRepository.GetUserByGuid(RemoveBearerWord(authHeader!));
      if (user != null && user.Role == "admin")
      {
         return true;
      }

      return false;
   }

   public string RemoveBearerWord(string key)
   {
      return Regex.Replace(key,  "^Bearer ", "");
   }
}
