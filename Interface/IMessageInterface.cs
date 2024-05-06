using Microsoft.AspNetCore.Identity;
using SIgnin_Manager.DTO;
using SIgnin_Manager.Models;

namespace SIgnin_Manager.Interface
{
    public interface IMessageInterface
    {
        public ICollection<MessageDTO> GetMessages(string user1Email, string user2Email);

        public UserManagerResponse sendMessage(string senderEmail, string receiverEmail, string message);

        public UserManagerResponse sendMessageRealTime(string senderId, string receiverEmail, string message);




    }
}
