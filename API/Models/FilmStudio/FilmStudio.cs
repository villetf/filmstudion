using System;
using API.Models.Interfaces;

namespace API.Models.Entities;

public class FilmStudio : IFilmStudio
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public string? City { get; set; }
}
