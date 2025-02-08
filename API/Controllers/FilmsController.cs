using API.Models.DTOs;
using API.Models.Entities;
using API.Models.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
      private readonly IFilmRepository _filmRepository;
      private readonly IUserRepository _userRepository;
      private readonly IHelperServices _helperServices;
      public FilmsController(IFilmRepository filmRepository, IUserRepository userRepository, IHelperServices helperServices) 
      {
         _filmRepository = filmRepository;
         _userRepository = userRepository;
         _helperServices = helperServices;
      }

      [HttpGet]
      public async Task<ActionResult<IEnumerable<object>>> GetAllFilms()
      {
         var user = await _userRepository.GetUserByGuid(_helperServices.RemoveBearerWord(Request.Headers.Authorization!));
         IEnumerable<Film> films = await _filmRepository.GetAllFilms();

         if (user == null)
         {
            List<ReturnFilmNonAdmin> returnList = [];
            foreach (var film in films)
            {
               var returnObject = new ReturnFilmNonAdmin
               {
                  Id = film.Id,
                  Name = film.Name,
                  ReleaseYear = film.ReleaseYear
               };
               returnList.Add(returnObject);
            }
            return returnList;
         }

         return films.ToList();
      }
    }
}
