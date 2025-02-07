using System;

namespace API.Models.DTOs;

public class ReturnRegisteredUserDTO
{
   public int UserId { get; set; }
   public required string Username { get; set; }
   public required string Role { get; set; }
}
