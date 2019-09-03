using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTrackerApi.Domains
{
    public class LiveTracker : IDomain, IWithId
    {
        public Guid Id { get; set; }
        [ForeignKey("BusId")]
        public virtual Bus Bus { get; set; }
        public Guid BusId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
        public Guid StudentId { get; set; }
        public string ConnectionId { get; set; }
        public string DeviceFCMId { get; set; }
    }
}
