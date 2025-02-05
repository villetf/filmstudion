using System;

namespace API.Models.Interfaces;

public interface IUserAuthenticate
{
   public string Email { get; set; }
   public string Password { get; set; }
}
