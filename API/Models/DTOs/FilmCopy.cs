using System;
using API.Models.Entities;
using API.Models.Interfaces;

namespace API.Models.DTOs;

public class FilmCopy : IFilmCopy
{
   public int RentalId { get; set; }
   public required Film? Film { get; set; }
}
