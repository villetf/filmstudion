using System;
using API.Models.Interfaces;

namespace API.Models.Entities;

public class Film : IFilm
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public int ReleaseYear { get; set; }
   public int AvailableCopies { get; set; }
}
