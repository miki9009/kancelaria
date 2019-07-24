using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kancelaria.API.Data;
using Kancelaria.API.Dtos;
using Kancelaria.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kancelaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthRepository _repository;
        readonly IConfiguration _config;
        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

//Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            userRegisterDto.Username = userRegisterDto.Username.ToLower();

            if(await _repository.UserExists(userRegisterDto.Username))
            return BadRequest("Username already exists");

            var userToCreate = new User
            {
                UserName = userRegisterDto.Username,
                Email = userRegisterDto.Email
            };

            var createdUser = await _repository.Register(userToCreate, userRegisterDto.Password);

            return StatusCode(201);
        }

//LOGIN
        
        [HttpPost("login")]
        public async Task<IActionResult>Login(UserLoginDto userLoginDto)
        {
            var userFromRepo = await _repository.Login(userLoginDto.Username.ToLower(), userLoginDto.Password);
            if(userFromRepo == null)
                return Unauthorized();

            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //https://jwt.io/ <- Sprawdzenie poprawnosci tokena
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var val = await _repository.GetUsers();

            return Ok(val);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(int ID)
        {
            var val = await _repository.GetUser(ID);

            return Ok(val);
        }

    }
}