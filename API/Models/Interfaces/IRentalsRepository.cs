using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public interface IRentalsRepository
{
   public Task<IEnumerable<Rental>> GetRentalsByStudio(int id);
}
