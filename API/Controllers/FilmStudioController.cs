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
      private readonly IUserService _userService;
      public FilmStudioController(IFilmStudioRepository studioRepository, IUserService userService) 
      {
         _studioRepository = studioRepository;
         _userService = userService;
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
   }
}
