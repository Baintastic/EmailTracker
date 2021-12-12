namespace EmailTracker.Core.Models
{
    public class Email : BaseEntity
    {
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string EmailSubject { get; set; }
        public string Body { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public bool IsArchived { get; set; }
    }
}
