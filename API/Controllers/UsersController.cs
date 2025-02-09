using System.Threading.Tasks;
using API.Models.DTOs;
using API.Models.Entities;
using API.Models.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class UsersController : ControllerBase
   {
      private readonly IUserRepository _usersRepository;
      private readonly IHelperServices _helperServices;
      private readonly IFilmStudioRepository _filmStudioRepository;
      public UsersController(IUserRepository usersRepository, IHelperServices helperServices, IFilmStudioRepository filmStudioRepository) 
      {
         _usersRepository = usersRepository;
         _helperServices = helperServices;
         _filmStudioRepository = filmStudioRepository;
      }

      [HttpPost("register")]
      public async Task<ActionResult> Register(UserRegisterDTO userCreds)
      {
         if (await _helperServices.UserExistsAsync(userCreds.Username) == true)
         {
            return BadRequest(new {message="En användare med detta användarnamn finns redan."});
         }

         return Ok(_helperServices.RegisterUser(userCreds));
      }

      [HttpPost("authenticate")]
      public async Task<ActionResult> Authenticate(UserAuthenticateDTO creds)
      {
         var userToLogin = await _usersRepository.GetUserByUsername(creds.Username);

         if (userToLogin == null)
         {
            return Unauthorized(new {message="Felaktigt användarnamn eller lösenord!"});
         }

         var passwordGenerator = new PasswordHasher<string>();
         var result = passwordGenerator.VerifyHashedPassword(string.Empty, userToLogin.PasswordHash, creds.Password);

         if (result == PasswordVerificationResult.Failed)
         {
            return Unauthorized(new {message="Felaktigt användarnamn eller lösenord!"});
         }

         if (userToLogin.Role == "filmstudio")
         {
            return Ok(new {
               id = userToLogin.Id,
               username = userToLogin.Username,
               role = userToLogin.Role,
               filmStudioId = userToLogin.FilmStudioId,
               filmStudio = await _filmStudioRepository.GetStudioById(userToLogin.FilmStudioId),
               guid = userToLogin.UserGuid
            });
         } else
         {
            return Ok(new {
               id = userToLogin.Id,
               username = userToLogin.Username,
               role = userToLogin.Role,
               guid = userToLogin.UserGuid
            });
         }
      }
   }
}
