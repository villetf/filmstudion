using System;
using API.Models.Entities;
using API.Models.Interfaces;

namespace API.Models.Repositories;

public class FilmStudioRepository : IFilmStudioRepository
{
   public Task<FilmStudio> CreateNewStudio(FilmStudio studio)
   {
      throw new NotImplementedException();
   }

   public Task<IEnumerable<FilmStudio>> GetAllStudios()
   {
      throw new NotImplementedException();
   }

   public Task<IEnumerable<Rental>> GetRentalsByStudio(int id)
   {
      throw new NotImplementedException();
   }

   public Task<FilmStudio> GetStudioById()
   {
      throw new NotImplementedException();
   }
}
