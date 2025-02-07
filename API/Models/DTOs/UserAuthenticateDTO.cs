using System;
using API.Models.Interfaces;

namespace API.Models.DTOs;

public class UserAuthenticateDTO : IUserAuthenticate
{
   public required string Username { get; set; }
   public required string Password { get; set; }
}
