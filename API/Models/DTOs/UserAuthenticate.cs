using System;
using API.Models.Interfaces;

namespace API.Models.DTOs;

public class UserAuthenticate : IUserAuthenticate
{
   public required string Email { get; set; }
   public required string Password { get; set; }
}
