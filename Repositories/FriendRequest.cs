using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SIgnin_Manager.Data;
using SIgnin_Manager.DTO;
using SIgnin_Manager.Interface;
using SIgnin_Manager.Models;

namespace SIgnin_Manager.Repositories
{
    public class FriendRequest : IFriendRequestInterface
    {
        private ApplicationDbContext _Context;
        private UserManager<ApplicationUser> _userManger;

        public FriendRequest(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _Context = context;
            _userManger = userManager;

        }
        public UserManagerResponse AddFriendRequest(string senderEmail, string receiverEmail) //okay
        {
            var sender = _userManger.FindByEmailAsync(senderEmail).Result;

            var receiver = _userManger.FindByEmailAsync(receiverEmail).Result;

            var friendRequest = new FriendRequestTable();
            friendRequest.SentBy= sender;
            friendRequest.ReceivedBy= receiver;
            friendRequest.IsAccepted = false;

            _Context.tbl_FriendRequest.Add(friendRequest);

            var result = _Context.SaveChanges();

            var returning = new UserManagerResponse
            {
                Message="Friend Request Sent"

            };
            return returning;


           
        }

        public ICollection<FriendRequestTable> GetAvailableRequest(string email)   // okay
        {

            var receiver = _userManger.FindByEmailAsync(email).Result;

            var friends = _Context.tbl_FriendRequest.Where(a => (a.ReceivedBy==receiver) && (a.IsAccepted == false)).ToList();
            return friends;
           // throw new NotImplementedException();
        }

        public  ICollection<ApplicationUser> GetFriends(string email)     //exception beacause of lazy loading //solved //okay
        {
            var sender = _userManger.FindByEmailAsync(email).Result;

           // var friends = _Context.tbl_FriendRequest.Where(a => (((a.SentBy == sender) && (a.IsAccepted == true)) || ((a.ReceivedBy == sender) && (a.IsAccepted == true)))).ToList();
           var friends=_Context.tbl_FriendRequest.
                Include(a=>a.SentBy).                          //Eager loading to get the foreign key
                Where(a => (((a.SentBy == sender) && (a.IsAccepted == true)) || ((a.ReceivedBy == sender) && (a.IsAccepted == true)))).ToList();
            var myfriends= new List<ApplicationUser>();
            
            
            foreach (var friend in friends)
            {
                var result = _userManger.FindByEmailAsync(  friend.SentBy.EmailId).Result;
                myfriends.Add(result);

            }
            return myfriends;
           // throw new NotImplementedException();
        }

        public bool IsFriend(string senderEmail, string receiverEmail)   //okay
        {
           var sender = _userManger.FindByEmailAsync(senderEmail).Result;
            
            var receiver = _userManger.FindByEmailAsync(receiverEmail).Result;

            var isFriend = _Context.tbl_FriendRequest.Where(a => (a.SentBy == sender && a.ReceivedBy==receiver && a.IsAccepted==true )|| (a.SentBy == receiver && a.ReceivedBy == sender && a.IsAccepted == true)).FirstOrDefault();

            if(isFriend==null)
                return false;
            else
                return true;






          // throw new NotImplementedException();
        }

        public bool RespodToRequest(int requestId, bool response)  //okay
        {
            var request=_Context.tbl_FriendRequest.Where(a=>a.Id==requestId).FirstOrDefault();
            if (request == null)
                return false;
            if (response)
            {
                request.IsAccepted=true;

            }

            _Context.Update(request);
            _Context.SaveChanges();

            return response;



            //throw new NotImplementedException();
        }
    }
}
