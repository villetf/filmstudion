using API.Models.DTOs;
using API.Models.Entities;
using API.Models.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
      private readonly IRentalsRepository _rentalsRepository;
      public FilmsController(IFilmRepository filmRepository, IUserRepository userRepository, IHelperServices helperServices, IRentalsRepository rentalsRepository) 
      {
         _filmRepository = filmRepository;
         _userRepository = userRepository;
         _helperServices = helperServices;
         _rentalsRepository = rentalsRepository;
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
      
      [HttpPost]
      public async Task<ActionResult<Film>> CreateFilm(Film film)
      {
         if (await _helperServices.UserIsAdmin(Request.Headers.Authorization!) == false)
         {
            return Unauthorized(new {message="Du har inte behörighet att göra detta."});
         }

         return Ok(await _filmRepository.AddNewFilm(film));
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Film>> GetMovieById(int id)
      {
         var user = await _userRepository.GetUserByGuid(_helperServices.RemoveBearerWord(Request.Headers.Authorization!));
         var film = await _filmRepository.GetFilmById(id);

         if (film == null)
         {
            return NotFound(new {message=$"Filmen med ID {id} finns inte."});
         }

         if (user == null)
         {
            
            ReturnFilmNonAdmin filmToReturn = new ReturnFilmNonAdmin
            {
               Id = film.Id,
               Name = film.Name,
               ReleaseYear = film.ReleaseYear
            };
            return Ok(filmToReturn);
         }

         return Ok(film);
      }

      [HttpPut("{id}")]
      public async Task<ActionResult<Film>> EditFilm(int id, Film film)
      {
         if (await _helperServices.UserIsAdmin(Request.Headers.Authorization!) == false)
         {
            return Unauthorized(new {message="Du har inte behörighet att göra detta."});
         }

         if (await _filmRepository.FilmExists(id) == false)
         {
            return NotFound(new {message="Denna filmen finns inte."});
         }

         film.Id = id;

         return Ok(await _filmRepository.EditFilm(film));
      }

      [HttpPost("rent")]
      public async Task<ActionResult> MakeRental([FromQuery]int id, [FromQuery]int studioId)
      {
         var user = await _userRepository.GetUserByGuid(_helperServices.RemoveBearerWord(Request.Headers.Authorization!));
         bool userIsAdmin = await _helperServices.UserIsAdmin(_helperServices.RemoveBearerWord(Request.Headers.Authorization!));
         if (user == null || (userIsAdmin == false && user.FilmStudioId != studioId))
         {
            return Unauthorized(new {message="Du har inte behörighet att göra detta."});
         }

         var film = await _filmRepository.GetFilmById(id);

         if (film == null)
         {
            return Conflict(new {message="Filmen du angett finns inte."});
         }

         if (film.AvailableCopies == 0)
         {
            return Conflict(new {message="Filmen du angett har inga exemplar tillgänliga för uthyrning."});
         }

         if (_rentalsRepository.StudioRentsThisMovie(studioId, id) == true)
         {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = "Studion hyr redan den här filmen." });
         }

         _rentalsRepository.MakeRental(film, studioId);
         _rentalsRepository.DecreaseStock(film);
         return Ok(new {message="Uthyrning lyckades!"});
      }

      [HttpPost("return")]
      public async Task<ActionResult> ReturnRental([FromQuery]int id, [FromQuery]int studioId)
      {
         var user = await _userRepository.GetUserByGuid(_helperServices.RemoveBearerWord(Request.Headers.Authorization!));
         bool userIsAdmin = await _helperServices.UserIsAdmin(_helperServices.RemoveBearerWord(Request.Headers.Authorization!));
         if (user == null || (userIsAdmin == false && user.FilmStudioId != studioId))
         {
            return Unauthorized(new {message="Du har inte behörighet att göra detta."});
         }

         var film = await _filmRepository.GetFilmById(id);

         if (film == null)
         {
            return Conflict(new {message="Filmen du angett finns inte."});
         }  

         if (_rentalsRepository.StudioRentsThisMovie(studioId, id) == false)
         {
            return StatusCode(StatusCodes.Status403Forbidden, new { message = "Studion hyr inte den här filmen." });
         }

         var rental = await _rentalsRepository.GetRentedFilm(film.Id, studioId);

         _rentalsRepository.ReturnRental(rental, studioId);
         _rentalsRepository.IncreaseStock(film);
         return Ok(new {message="Återlämning lyckades!"});
      }
   }
}
