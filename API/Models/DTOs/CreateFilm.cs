using System;
using API.Models.Interfaces;

namespace API.Models.DTOs;

public class CreateFilm : ICreateFilm
{
   public required string Name { get; set; }
   public int ReleaseYear { get; set; }
   public int AvailableCopies { get; set; }
}
