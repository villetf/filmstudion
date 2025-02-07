using System;

namespace API.Models.Entities;

public class Rental
{
   public int Id { get; set; }
   public int StudioId { get; set; }
   public int FilmId { get; set; }
   public bool IsReturned { get; set; }
}
