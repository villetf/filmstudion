using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public interface IFilmCopy
{
   public int RentalId { get; set; }
   public Film Film { get; set; }
}
