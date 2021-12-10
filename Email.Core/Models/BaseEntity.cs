using System;

namespace EmailTracker.Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOnDate { get; set; }
    }
}
