namespace SIgnin_Manager.Models
{
    public class MessageTable
    {
        public int Id { get; set; }
        public ApplicationUser MsgSentBy { get; set; }
        public ApplicationUser MsgReceivedBY { get; set; }
        public string Message { get; set; }
    }
}
