using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SIgnin_Manager.Interface;

namespace SIgnin_Manager.Hubs
{
    public class SignalrHub:Hub
    {

        private IMessageInterface _message;
        private IUserInterface _user;

        public SignalrHub(IMessageInterface message, IUserInterface user)
        {
            _message = message;
            _user = user;

        }


        [Authorize]
        public void SendMessage( string receiverEmail, string message)
        {
            var senderId = Context.User.Identity.Name;
            // this is the id so find out the email from this
            //save data to database by calling the function from repositories
            //if success send message to the receiver email through signalr
            //if not return error message

            //afterhub is setup create a client application to consume the api and create signalr connection 


            var result=_message.sendMessageRealTime(senderId, receiverEmail, message);

            if (result.IsSuccess == true)
            {
                var recepeint=_user.FindUserByEmail(receiverEmail);
                var receipientId = recepeint.Result.Id;
                Clients.User(receipientId).SendAsync("ReceiveMessage", message);




            }

            






            // . receiveMessage(message);














            // Clients.User(recipientId).receiveMessage(message);
            //Clients.All.SendAsync("ReceiveMessage",message);
        }









    }
}
