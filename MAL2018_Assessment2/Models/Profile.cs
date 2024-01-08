using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MAL2018_Assessment2.Models
{
    public partial class Profile
    {
        public Profile()
        {
            ProfileMarks = new HashSet<ProfileMark>();
        }

        [JsonPropertyName("profileId")]
        public int ProfileId { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("profileName")]
        public string ProfileName { get; set; }

        [JsonPropertyName("userContact")]
        public string UserContact { get; set; }

        [JsonPropertyName("bio")]
        public string Bio { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<ProfileMark> ProfileMarks { get; set; }
    }
}
