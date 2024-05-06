using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SIgnin_Manager.Helper;
using SIgnin_Manager.Interface;
using System.IdentityModel.Tokens.Jwt;

namespace SIgnin_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private IConfiguration _configuration;
        private IMessageInterface _message;

        private readonly JwtValidateAndDeserialize _jwt;
        public MessageController(IMessageInterface message, IConfiguration configuration, JwtValidateAndDeserialize jwt)
        {
            _message = message;

            _configuration = configuration;
            _jwt = jwt;
        }


        [HttpGet]
       // [Authorize]
        public IActionResult GetMessages()
        {
            //string token = HttpContext.Request.Headers["Authorization"].ToString();

            //// The token comes with the "Bearer " prefix, so we need to remove it
            //if (token.StartsWith("Bearer "))
            //{
            //    token = token.Substring("Bearer ".Length);
            //}
            //var validatedToken = _jwt.ValidateAndDeseerialize(token);//calls the function to validate the token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var jsonToken = tokenHandler.ReadToken(validatedToken) as JwtSecurityToken;
            //var claims = jsonToken.Claims;
            //var email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

            var email = "shirishbashyal@gmail.com";
            var friendEmail = "shraddhabashyal@gmail.com";

            var result = _message.GetMessages(email, friendEmail);
            return Ok(result);

        }

        //[HttpPost]
        //[Authorize]
        //public IActionResult SendMessage(string friendEmail, string message)
        //{
        //    string token = HttpContext.Request.Headers["Authorization"].ToString();

        //    // The token comes with the "Bearer " prefix, so we need to remove it
        //    if (token.StartsWith("Bearer "))
        //    {
        //        token = token.Substring("Bearer ".Length);
        //    }
        //    var validatedToken = _jwt.ValidateAndDeseerialize(token);//calls the function to validate the token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var jsonToken = tokenHandler.ReadToken(validatedToken) as JwtSecurityToken;
        //    var claims = jsonToken.Claims;
        //    var email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

        //    var result = _message.sendMessage(email, friendEmail, message);


        //    return Ok(result);






        //}
    }
}
