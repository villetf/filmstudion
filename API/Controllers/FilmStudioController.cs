using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
   public class FilmStudioController : ControllerBase
   {
      private readonly IFilmStudioRepository _studioRepository;
      private readonly IUserRepository _userRepository;
      private readonly IHelperServices _helperServices;
      private readonly IRentalsRepository _rentalsRepository;
      private readonly IFilmRepository _filmRepository;
      public FilmStudioController(IFilmStudioRepository studioRepository, IHelperServices helperServices, IUserRepository userRepository, IRentalsRepository rentalsRepository, IFilmRepository filmRepository) 
      {
         _studioRepository = studioRepository;
         _helperServices = helperServices;
         _userRepository = userRepository;
         _rentalsRepository = rentalsRepository;
         _filmRepository = filmRepository;
      }

      // Endpoint för att registera filmstudio (skapar också en användare för studion)
      [HttpPost("register")]
      public async Task<ActionResult<IFilmStudio>> RegisterStudio(RegisterFilmStudioDTO registrationInfo)
      {
         if (await _helperServices.UserExistsAsync(registrationInfo.Name) == true)
         {
            return BadRequest(new {message="En användare med detta användarnamn finns redan."});
         }

         FilmStudio newStudio = new FilmStudio
         {
            Name = registrationInfo.Name,
            City = registrationInfo.City
         };

         var createdStudio = await _studioRepository.CreateNewStudio(newStudio);

         var newUser = new UserRegisterDTO
         {
            Username = registrationInfo.Name,
            Password = registrationInfo.Password,
            IsAdmin = false,
            FilmStudioId = createdStudio.Id
         };
         
         _helperServices.RegisterUser(newUser);

         return Ok(newStudio);
      }

      // Endpoint för att hämta alla filmstudior
      [HttpGet("/api/filmstudios")]
      public async Task<ActionResult<IEnumerable<FilmStudio>>> GetStudios()
      {
         // var loggedInUser = await _userRepository.GetUserByGuid(Regex.Replace(Request.Headers.Authorization, "^Bearer ", ""));
         bool isAdmin = await _helperServices.UserIsAdmin(Request.Headers.Authorization!);
         var studios = await _studioRepository.GetAllStudios();

         if (isAdmin == true)
         {
            List<ReturnStudioAdminDTO> returnAdminList = [];
            foreach (var studio in studios)
            {
               var currentStudio = new ReturnStudioAdminDTO
               {
                  Id = studio.Id,
                  Name = studio.Name,
                  City = studio.City!,
                  RentedFilmCopies = await _rentalsRepository.GetRentalsByStudio(studio.Id)
               };
               returnAdminList.Add(currentStudio);
            }

            return Ok(returnAdminList);
         }
         
         List<ReturnStudioNonAdminDTO> returnNonAdminList = [];
         foreach (var studio in studios)
         {
            var currentStudio = new ReturnStudioNonAdminDTO
            {
               Id = studio.Id,
               Name = studio.Name
            };
            returnNonAdminList.Add(currentStudio);
         }
         
         return Ok(returnNonAdminList);
      }

      // Endpoint för att hämta filmstudio från ID
      [HttpGet("{id}")]
      public async Task<ActionResult<FilmStudio>> GetSingleStudio(int id)
      {
         var studio = await _studioRepository.GetStudioById(id);

         if (studio == null)
         {
            return NotFound(new {message=$"Hittade ingen studio med id {id}."});
         }

         bool isAdmin = await _helperServices.UserIsAdmin(Request.Headers.Authorization!);
         var studioUser = await _userRepository.GetUserByStudioId(id);

         if (studioUser != null && (isAdmin == true || studioUser.UserGuid.ToString() == _helperServices.RemoveBearerWord(Request.Headers.Authorization!)))
         {
            var studioToReturnAdmin = new ReturnStudioAdminDTO
            {
               Id = studio.Id,
               Name = studio.Name,
               City = studio.City!,
               RentedFilmCopies = await _rentalsRepository.GetRentalsByStudio(studio.Id)
            };

            return Ok(studioToReturnAdmin);
         }

         var studioToReturnNonAdmin = new ReturnStudioNonAdminDTO
         {
            Id = studio.Id,
            Name = studio.Name
         };

         return Ok(studioToReturnNonAdmin);
      }
  
      [HttpGet("/api/mystudio/rentals")]
      public async Task<ActionResult<IEnumerable<FilmCopy>>> GetOwnRentals()
      {
         var user = await _userRepository.GetUserByGuid(_helperServices.RemoveBearerWord(Request.Headers.Authorization!));
         
         if (user == null || user.Role != "filmstudio")
         {
            return Unauthorized(new {message="Du har inte behörighet att göra detta."});
         }

         var studio = await _studioRepository.GetStudioById(user.FilmStudioId);
         var rentedFilms = await _rentalsRepository.GetRentalsByStudio(studio!.Id);

         List<FilmCopy> returnList = [];

         foreach (var rental in rentedFilms)
         {
            var currentRental = new FilmCopy
            {
               RentalId = rental.Id,
               Film = await _filmRepository.GetFilmById(rental.FilmId)!
            };

            returnList.Add(currentRental);
         }

         return Ok(returnList);
      }
   }
}
