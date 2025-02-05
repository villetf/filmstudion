using System;

namespace API.Models.Interfaces;

public interface IFilm
{
   public int Id { get; set; }
   public string Name { get; set; }
   public int ReleaseYear { get; set; }
   public int AvailableCopies { get; set; }
}
