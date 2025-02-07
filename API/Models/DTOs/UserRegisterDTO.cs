using System;
using API.Models.Interfaces;

namespace API.Models.DTOs;

public class UserRegisterDTO : IUserRegister
{
   public required string Username { get; set; }
   public required string Password { get; set; }
   public required bool IsAdmin { get; set; }
   public int? FilmStudioId { get; set; }
}