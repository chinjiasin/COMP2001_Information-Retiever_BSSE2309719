using System;
using System.Collections.Generic;

namespace MAL2018_Assessment2.Models
{
    public partial class TrailActivity
    {
        public TrailActivity()
        {
            BookMarks = new HashSet<BookMark>();
        }

        public int TrailActivitiesId { get; set; }
        public int TrailId { get; set; }
        public string? ActivitiesType { get; set; }
        public string? ActivitiesDescription { get; set; }
        public DateTime? LastUpdated { get; set; }

        public virtual Trail Trail { get; set; } = null!;
        public virtual ICollection<BookMark> BookMarks { get; set; }
    }
}
