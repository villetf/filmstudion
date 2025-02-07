using System;

namespace API.Models.Interfaces;

public interface IUserRegister
{
   public string Username { get; set; }
   public string Password { get; set; }
   public bool IsAdmin { get; set; }
   public int? FilmStudioId { get; set; }
}
