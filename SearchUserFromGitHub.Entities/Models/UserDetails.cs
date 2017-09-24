namespace SearchUserFromGitHub.Entities.Models
{
    /// <summary>
    /// Model for user details
    /// </summary>
    public class UserDetails
    {
        /// <param name="name">User name</param>
        public string name { get; set; }

        /// <param name="location">User location</param>
        public string location { get; set; }

        /// <param name="avatarUrl">User's picture</param>
        public string avatarUrl { get; set; }
    }
}