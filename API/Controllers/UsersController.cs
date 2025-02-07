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
      private readonly IUserService _userService;
      public UsersController(IUserRepository usersRepository, IUserService userService) 
      {
         _usersRepository = usersRepository;
         _userService = userService;
      }

      [HttpPost("register")]
      public async Task<ActionResult> Register(UserRegisterDTO userCreds)
      {
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine("test");
         Console.WriteLine(await _userService.UserExistsAsync(userCreds.Username) == true);
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         Console.WriteLine();
         if (await _userService.UserExistsAsync(userCreds.Username) == true)
         {
            return BadRequest(new {message="En användare med detta användarnamn finns redan."});
         }

         return Ok(_userService.RegisterUser(userCreds));
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

         if (result == PasswordVerificationResult.Success)
         {
            return Ok(new {message="Inloggad", token = userToLogin.UserGuid});
         }

         return Unauthorized(new {message="Felaktigt användarnamn eller lösenord!"});
      }
   }
}
