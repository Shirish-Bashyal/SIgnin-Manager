using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SIgnin_Manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAuth : ControllerBase
    {
        //craete a function that calls for the google api for sign in and redirect to google signin page and after user have selected his google account redirects to a new url
        [HttpGet("signin")]
        public IActionResult GoogleSignIn()
        {
            string redirectUrl = "https://localhost:5001/api/GoogleAuth/GoogleResponse";
            string clientId = "your client id";
            string scope = "https://www.googleapis.com/auth/userinfo.email";
            string responseType = "code";
            string url = $"https://accounts.google.com/o/oauth2/v2/auth?client_id={clientId}&redirect_uri={redirectUrl}&response_type={responseType}&scope={scope}";
            return Redirect(url);
        }
        //this is the url that google redirects to after user have selected his google account
        [HttpGet("GoogleResponse")]
        public IActionResult GoogleResponse(string code)
        {
            //code is the code that google sends back to the url that user is redirected to
            //this code is used to get the access token and refresh token from google
            return Ok(code);
        }
       
    }
}
