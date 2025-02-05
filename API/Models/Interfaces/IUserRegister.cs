using System;

namespace API.Models.Interfaces;

public interface IUserRegister
{
   public string Email { get; set; }
   public string Password { get; set; }
}
