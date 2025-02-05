using System;

namespace API.Models.Interfaces;

public interface IFilmStudio
{
   public int Id { get; set; }
   public string Name { get; set; }
   public string? City { get; set; }
}
