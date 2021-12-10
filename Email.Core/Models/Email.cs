namespace EmailTracker.Core.Models
{
    public class Email : BaseEntity
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string EmailSubject { get; set; }
        public string Body { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public bool IsArchived { get; set; }

    }
}
