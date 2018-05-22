using System;
using System.Collections.Generic;
using System.Text;
using RestSharp.Deserializers;

namespace TravisApi.Models.Responses
{
    public class GetUserResponse
    {
        [DeserializeAs(Name = "@type")]
        public string Type { get; set; }

        [DeserializeAs(Name = "@href")]
        public string Href { get; set; }

        [DeserializeAs(Name = "@representation")]
        public string Representation { get; set; }

        [DeserializeAs(Name = "@permissions")]
        public Permissions Permissions { get; set; }

        public int Id { get; set; }
        public string Login { get; set; }
        public object Name { get; set; }
        public int GithubId { get; set; }
        public string AvatarUrl { get; set; }
        public bool Education { get; set; }
        public bool IsSyncing { get; set; }
        public DateTime SyncedAt { get; set; }
    }

    public class Permissions
    {
        public bool Read { get; set; }
        public bool Sync { get; set; }
    }

}
