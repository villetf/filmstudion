using System;
using API.Models.Entities;

namespace API.Models.DTOs;

public class ReturnAllStudiosAdminDTO
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public required string City { get; set; }
   public required IEnumerable<Rental> RentedFilmCopies { get; set; }
}
