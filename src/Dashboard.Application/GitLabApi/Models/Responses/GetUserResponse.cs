using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Application.GitLabApi.Models.Responses
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string AvatarUrl { get; set; }
        public string WebUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public object Bio { get; set; }
        public object Location { get; set; }
        public string Skype { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string WebsiteUrl { get; set; }
        public string Organization { get; set; }
        public DateTime LastSignInAt { get; set; }
        public DateTime ConfirmedAt { get; set; }
        public int ThemeId { get; set; }
        public string LastActivityOn { get; set; }
        public int ColorSchemeId { get; set; }
        public int ProjectsLimit { get; set; }
        public DateTime CurrentSignInAt { get; set; }
        public List<Identity> Identities { get; set; }
        public bool CanCreateGroup { get; set; }
        public bool CanCreateProject { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool External { get; set; }
    }

    public class Identity
    {
        public string Provider { get; set; }
        public string ExternUid { get; set; }
    }
}
