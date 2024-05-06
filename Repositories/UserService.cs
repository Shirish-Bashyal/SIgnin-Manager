using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using SIgnin_Manager.DTO;
using SIgnin_Manager.Interface;
using SIgnin_Manager.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace SIgnin_Manager.Repositories
{


    public class UserService : IUserInterface
    {
        private UserManager<ApplicationUser> _userManger;
        private IConfiguration _configuration;




        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManger = userManager;
            _configuration = configuration;
        }

        public Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindUserByEmail(string email)
        {

            var user = _userManger.FindByEmailAsync(email);

            return user;




            throw new NotImplementedException();
        }

        public Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public  ICollection<ApplicationUser> GetAllUser()
        {
            var  users=  _userManger.Users.ToList();





            return users;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginDTO model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }

            

            var result = await _userManger.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "Invalid password",
                    IsSuccess = false,
                };

            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);


            //var cookie = new CookieHeaderValue("session-id", tokenAsString);
            



            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo
            };
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterDTO model)
        {
            if (model == null)
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var identityUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Name,
                AboutMe = model.AboutMe,
                Address = model.Address,
                ContactInformation = model.ContactInformation,
                EmailId = model.Email,
                Name=model.Name

            };

            var result = await _userManger.CreateAsync(identityUser, model.Password);

            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, error.Description);
            //}

            if (result.Succeeded)
            {




                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };
            }

            else
            {
                
                return new UserManagerResponse
                {
                    Message = "User not created",
                    IsSuccess = false,
                    
                   
            };
            }

        }

       
    }
}

