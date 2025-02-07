using System;
using API.Models.Interfaces;

namespace API.Models.DTOs;

public class RegisterFilmStudioDTO : IRegisterFilmStudio
{
   public required string Name { get; set; }
   public string? City { get; set; }
   public required string Password { get; set; }
}
