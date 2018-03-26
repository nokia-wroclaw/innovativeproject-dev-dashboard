namespace Dashboard.Application.GitLabApi.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }
        public string State { get; set; }
        public string AvatarUrl { get; set; }
        public string WebUrl { get; set; }
    }
}
