using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public interface IFilmStudioRepository
{
   public Task<IEnumerable<FilmStudio>> GetAllStudios();
   public Task<FilmStudio?> GetStudioById(int? id);
   public Task<IEnumerable<Rental>> GetRentalsByStudio(int id);
   public Task<FilmStudio> CreateNewStudio(FilmStudio studio);
}
