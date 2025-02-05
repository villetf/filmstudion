using System;

namespace API.Models.Interfaces;

public interface ICreateFilm
{
   public string Name { get; set; }
   public int ReleaseYear { get; set; }
   public int AvailableCopies { get; set; }
}
