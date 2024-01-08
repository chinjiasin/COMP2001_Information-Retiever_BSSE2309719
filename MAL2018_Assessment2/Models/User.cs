using System;
using System.Collections.Generic;

namespace MAL2018_Assessment2.Models
{
    public partial class User
    {
        public User()
        {
            Profiles = new HashSet<Profile>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? Position { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
