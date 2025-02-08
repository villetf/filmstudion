using System;

namespace API.Models.DTOs;

public class ReturnFilmNonAdmin
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public int ReleaseYear { get; set; }
}
