using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SIgnin_Manager.Data;
using SIgnin_Manager.DTO;
using SIgnin_Manager.Interface;
using SIgnin_Manager.Models;

namespace SIgnin_Manager.Repositories
{
    public class Messages : IMessageInterface
    {

        private ApplicationDbContext _Context;
        private UserManager<ApplicationUser> _userManger;

        public Messages(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _Context = context;
            _userManger = userManager;

        }
        public UserManagerResponse sendMessage(string senderEmail, string receiverEmail, string message)
        {
            var sender = _userManger.FindByEmailAsync(senderEmail).Result;
            var receiver = _userManger.FindByEmailAsync(receiverEmail).Result;

            var messageDetails = new MessageTable();
            messageDetails.Message = message;
            messageDetails.MsgSentBy= sender;
            messageDetails.MsgReceivedBY= receiver;

            _Context.tbl_Messages.Add(messageDetails);
            _Context.SaveChanges();

            return new UserManagerResponse
            {
                Message = "message sent",
                IsSuccess=true,

            };






           // throw new NotImplementedException();
        }

        public ICollection<MessageDTO> GetMessages(string user1Email, string user2Email)
        {


            var user1=_userManger.FindByEmailAsync(user1Email).Result;
            var user2=_userManger.FindByEmailAsync(user2Email).Result;

            var result = _Context.tbl_Messages.
                Include(a => a.MsgSentBy).
                Where(a => ((a.MsgSentBy == user1) && (a.MsgReceivedBY == user2)) || (a.MsgSentBy == user2) && (a.MsgReceivedBY == user1)).
                Select(a=> new MessageDTO()
                {
                    MessageSentBy=a.MsgSentBy.UserName,
                    MessageReceivedBy=a.MsgReceivedBY.UserName,
                    Message=a.Message,
                
                
                
                }).
                ToList();


           
            return result; 






           // throw new NotImplementedException();
        }

        public UserManagerResponse sendMessageRealTime(string senderId, string receiverEmail, string message)
        {

            var sender = _userManger.FindByIdAsync(senderId).Result;
            var receiver = _userManger.FindByEmailAsync(receiverEmail).Result;

            var messageDetails = new MessageTable();
            messageDetails.Message = message;
            messageDetails.MsgSentBy = sender;
            messageDetails.MsgReceivedBY = receiver;

            _Context.tbl_Messages.Add(messageDetails);
            _Context.SaveChanges();

            return new UserManagerResponse
            {
                Message = "message sent",
                IsSuccess = true,

            };

            //throw new NotImplementedException();
        }
    }
}
