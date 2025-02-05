using System;

namespace API.Models.Entities;

public class User
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public required string Password { get; set; }
   public required string Role { get; set; }
   public int? FilmStudioId { get; set; } 
}
