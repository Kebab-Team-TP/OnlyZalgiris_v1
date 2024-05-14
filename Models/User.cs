namespace Zalgiris.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string ProfilePictureUrl { get; set; }

        public User(string username, string password, string email, bool isAdmin, string profilePictureUrl)
        {
            Username = username;
            Password = password;
            Email = email;
            IsAdmin = isAdmin;
            ProfilePictureUrl = profilePictureUrl;
        }

        public User()
        {
            Username = null;
            Password = null;
            Email = null;
            IsAdmin = false;
            ProfilePictureUrl = null;
        }
    }
}
