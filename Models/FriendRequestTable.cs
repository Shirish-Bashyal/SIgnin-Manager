using Microsoft.EntityFrameworkCore;

namespace SIgnin_Manager.Models
{
    public class FriendRequestTable
    {
        
        public int Id { get; set; }

        public ApplicationUser SentBy { get; set; }
        public ApplicationUser ReceivedBy { get; set; }

        public Boolean IsAccepted { get; set; }
    }
}
