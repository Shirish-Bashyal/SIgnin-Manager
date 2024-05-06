using SIgnin_Manager.DTO;
using SIgnin_Manager.Models;

namespace SIgnin_Manager.Interface
{
    public interface IFriendRequestInterface
    {

        public ICollection<ApplicationUser> GetFriends(string email);

        public UserManagerResponse AddFriendRequest(string senderEmail, string receiverEmail);

       // public bool RemoveFriendRequest(string senderEmail, string receiverID);

        public bool IsFriend(string senderEmail, string receiverEmail);

        public ICollection<FriendRequestTable> GetAvailableRequest(string email);


        public bool RespodToRequest(int requestId,bool response);



    }
}
