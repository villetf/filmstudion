using System;

namespace API.Models.Interfaces;

public interface IUserAuthenticate
{
   public string Username { get; set; }
   public string Password { get; set; }
}
