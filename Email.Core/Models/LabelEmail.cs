using EmailTracker.Core.Models;

namespace EmailTracker.Core
{
    public class LabelEmail : BaseEntity
    {
        public int LabelId { get; set; }
        public int EmailId { get; set; }
    }
}
