using System;
using API.Models.Entities;

namespace API.Models.Interfaces;

public interface IFilmRepository
{
   public Task<Film> AddNewFilm(Film film);
   public Task<IEnumerable<Film>> GetAllFilms();
   public Task<Film?> GetFilmById(int id);
   public void MakeRental(Film film, int studioId);
   public void ReturnRental(Film film, int studioId);
   public Task<Film> EditFilm(Film film);
}
