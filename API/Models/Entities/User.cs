using System;
using API.Models.Interfaces;

namespace API.Models.Entities;

public class User : IUser
{
   public int Id { get; set; }
   public required string Username { get; set; }
   public required string PasswordHash { get; set; }
   public required string Role { get; set; }
   public int? FilmStudioId { get; set; } 
   public Guid UserGuid { get; set; }
}
