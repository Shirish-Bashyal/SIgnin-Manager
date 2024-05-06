using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SIgnin_Manager.Helper;
using SIgnin_Manager.Interface;
using SIgnin_Manager.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace SIgnin_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendRequest : ControllerBase
    {
        private IUserInterface _userService;

        private IConfiguration _configuration;
        private IFriendRequestInterface _friendRequest;
       
        private readonly JwtValidateAndDeserialize _jwt;
        public FriendRequest(IFriendRequestInterface friendRequest, IConfiguration configuration, JwtValidateAndDeserialize jwt)
        {
            _friendRequest = friendRequest;

            _configuration = configuration;
            _jwt = jwt;
        }


        [HttpGet("GetFriends")]
        [Authorize]
        public IActionResult ShowFriends()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();

            // The token comes with the "Bearer " prefix, so we need to remove it
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }
            var validatedToken = _jwt.ValidateAndDeseerialize(token);//calls the function to validate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(validatedToken) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            var email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

            var friendList=_friendRequest.GetFriends(email);
            //now find the friends of the user


            return Ok(friendList);
        }


        [HttpGet("{checkEmail}/IsFriend")]
        [Authorize]
        public IActionResult IsFriend(string checkEmail)   
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();

            // The token comes with the "Bearer " prefix, so we need to remove it
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }
            var validatedToken = _jwt.ValidateAndDeseerialize(token);//calls the function to validate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(validatedToken) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            var email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

           var result=_friendRequest.IsFriend(email, checkEmail);
         


            return Ok(result);
        }


        [HttpGet("{receiverEmail}/Add/FriendRequest")]
        [Authorize]
        public IActionResult AddFriendRequest(string receiverEmail)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();

            // The token comes with the "Bearer " prefix, so we need to remove it
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }
            var validatedToken = _jwt.ValidateAndDeseerialize(token);//calls the function to validate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(validatedToken) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            var email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

           var result=_friendRequest.IsFriend(email, receiverEmail);
            if(result==true)
            {
                return BadRequest("You are already friends");
            }


            var result1 = _friendRequest.AddFriendRequest(email, receiverEmail);



            return Ok(result1);
        }


        [HttpGet("Get/Request")]
        [Authorize]
        public IActionResult ShowFriendRequest()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();

            // The token comes with the "Bearer " prefix, so we need to remove it
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }
            var validatedToken = _jwt.ValidateAndDeseerialize(token);//calls the function to validate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(validatedToken) as JwtSecurityToken;
            var claims = jsonToken.Claims;
            var email = claims.FirstOrDefault(c => c.Type == "Email")?.Value;

            var friendList = _friendRequest.GetAvailableRequest(email);
            //now find the friends of the user


            return Ok(friendList);
        }

        [HttpPut("{requestId}/respondToRequest")]
        [Authorize]
        public IActionResult RespondToRequest(int requestId, bool response)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString();

            // The token comes with the "Bearer " prefix, so we need to remove it
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring("Bearer ".Length);
            }
            var validatedToken = _jwt.ValidateAndDeseerialize(token);

            var result = _friendRequest.RespodToRequest(requestId, response);
            return Ok(result);







        }













    }
}
