using Microsoft.AspNetCore.Identity;

namespace SIgnin_Manager.Models
{
    public class ApplicationUser:IdentityUser
    {
  
        public string Name { get; set; }
        public string AboutMe { get; set; }

        public string ContactInformation { get; set; }
        public string EmailId { get; set; }

        public string Address { get; set; }






    }
}
