using System;
using API.Models.Interfaces;

namespace API.Models.DTOs;

public class RegisterFilmStudio : IRegisterFilmStudio
{
   public required string Name { get; set; }
   public string? City { get; set; }
}
