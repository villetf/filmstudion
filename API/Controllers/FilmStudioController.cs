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
      private readonly IUserService _userService;
      private readonly IRentalsRepository _rentalsRepository;
      public FilmStudioController(IFilmStudioRepository studioRepository, IUserService userService, IUserRepository userRepository, IRentalsRepository rentalsRepository) 
      {
         _studioRepository = studioRepository;
         _userService = userService;
         _userRepository = userRepository;
         _rentalsRepository = rentalsRepository;
      }

      [HttpPost("register")]
      public async Task<ActionResult<IFilmStudio>> RegisterStudio(RegisterFilmStudioDTO registrationInfo)
      {
         if (await _userService.UserExistsAsync(registrationInfo.Name) == true)
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
         
         _userService.RegisterUser(newUser);

         return Ok(newStudio);
      }

      [HttpGet("/api/filmstudios")]
      public async Task<ActionResult<IEnumerable<FilmStudio>>> GetStudios()
      {
         Console.WriteLine(Regex.Replace(Request.Headers.Authorization, "^Bearer ", ""));
         var loggedInUser = await _userRepository.GetUserByGuid(Regex.Replace(Request.Headers.Authorization, "^Bearer ", ""));
         var studios = await _studioRepository.GetAllStudios();

         if (loggedInUser != null && loggedInUser.Role == "admin")
         {
            List<ReturnAllStudiosAdminDTO> returnAdminList = [];
            foreach (var studio in studios)
            {
               var currentStudio = new ReturnAllStudiosAdminDTO
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
         
         List<ReturnAllStudiosNonAdminDTO> returnNonAdminList = [];
         foreach (var studio in studios)
         {
            var currentStudio = new ReturnAllStudiosNonAdminDTO
            {
               Id = studio.Id,
               Name = studio.Name
            };
            returnNonAdminList.Add(currentStudio);
         }
         
         return Ok(returnNonAdminList);
      }
   }
}
