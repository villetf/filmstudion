using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public interface IUser
{
   public int Id { get; set; }
   public string Username { get; set; }
   public string PasswordHash { get; set; }
   public string Role { get; set; }
   public int? FilmStudioId { get; set; } 
   public FilmStudio? FilmStudio { get; set; } 
   public Guid UserGuid { get; set; }
}
