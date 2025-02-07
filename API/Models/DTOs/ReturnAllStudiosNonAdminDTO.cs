using System;

namespace API.Models.DTOs;

public class ReturnAllStudiosNonAdminDTO
{
   public int Id { get; set; }
   public required string Name { get; set; }
}
