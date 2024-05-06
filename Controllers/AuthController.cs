using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIgnin_Manager.Repositories;
using SIgnin_Manager.DTO;
using SIgnin_Manager.Interface;
using Microsoft.AspNetCore.Authorization;
using SIgnin_Manager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using SIgnin_Manager.Helper;

namespace SIgnin_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserInterface _userService;
     
        private IConfiguration _configuration;
        private readonly JwtValidateAndDeserialize _jwt;

        public AuthController(IUserInterface userService,  IConfiguration configuration,JwtValidateAndDeserialize jwt)
        {
            _userService = userService;
            
            _configuration = configuration;
            _jwt = jwt;
        }


       




        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO model)
        {



          
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

               

                return Ok(result);
            }

            return BadRequest("Some properties are not valid");
        }



        [HttpGet]
        [Authorize]
        public IActionResult ShowUsers()
        {



            // Retrieve the JWT token from the request's Authorization header
            string token = HttpContext.Request.Headers["Authorization"].ToString();

            // The token comes with the "Bearer " prefix, so we need to remove it
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }
            var validatedToken=_jwt.ValidateAndDeseerialize(token);//calls the function to validate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(validatedToken) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            var userId = claims.FirstOrDefault(c => c.Type == "Email")?.Value; // is this needed or not how to find out



            //validate the token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var validationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuerSigningKey = true,
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes((_configuration["AuthSettings:Key"]))),
            //    ValidateIssuer = false, // Set to true if you have a specific issuer
            //    ValidateAudience = false // Set to true if you have a specific audience
            //};

            //SecurityToken validatedToken;
            //var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            if (userId != null)
            {
                var result = _userService.GetAllUser();

                return Ok(result);
            }
            return BadRequest();

        }



        //[HttpGet("GetUsers")]
        //public IActionResult GetUser()
        //{

        //    var user = new UserData();
            







        //    return Ok();
        //}
        









    }


}
