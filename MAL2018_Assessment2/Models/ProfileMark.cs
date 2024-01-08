using System;
using System.Collections.Generic;

namespace MAL2018_Assessment2.Models
{
    public partial class ProfileMark
    {
        public int ProfileMarkId { get; set; }
        public int ProfileId { get; set; }
        public int BookMarkId { get; set; }

        public virtual BookMark BookMark { get; set; } = null!;
        public virtual Profile Profile { get; set; } = null!;
    }
}
