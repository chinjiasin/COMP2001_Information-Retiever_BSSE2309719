using System;
using System.Collections.Generic;

namespace MAL2018_Assessment2.Models
{
    public partial class BookMark
    {
        public BookMark()
        {
            ProfileMarks = new HashSet<ProfileMark>();
        }

        public int BookMarkId { get; set; }
        public int TrailActivitiesId { get; set; }
        public string BookMarkDescription { get; set; } = null!;
        public byte[] TimePeriod { get; set; } = null!;

        public virtual TrailActivity TrailActivities { get; set; } = null!;
        public virtual ICollection<ProfileMark> ProfileMarks { get; set; }
    }
}
