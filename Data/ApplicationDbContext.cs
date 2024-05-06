using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIgnin_Manager.Models;

namespace SIgnin_Manager.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                                            : base(options)
        { }
        //public DbSet<SIgnin_Manager.Models.ApplicationUser> tbl_user{ get; set; }
        public DbSet<SIgnin_Manager.Models.UserData> tbl_UserData { get; set; }

        public DbSet<SIgnin_Manager.Models.FriendRequestTable> tbl_FriendRequest { get; set; }

        public DbSet<SIgnin_Manager.Models.MessageTable> tbl_Messages { get; set; }






    }
}
