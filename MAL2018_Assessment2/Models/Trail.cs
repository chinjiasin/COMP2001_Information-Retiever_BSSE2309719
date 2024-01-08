using System;
using System.Collections.Generic;

namespace MAL2018_Assessment2.Models
{
    public partial class Trail
    {
        public Trail()
        {
            TrailActivities = new HashSet<TrailActivity>();
        }

        public int TrailId { get; set; }
        public double? Distance { get; set; }
        public string? TrailLength { get; set; }
        public string? EstimatedTime { get; set; }
        public string? TrailLocation { get; set; }

        public virtual ICollection<TrailActivity> TrailActivities { get; set; }
    }
}
