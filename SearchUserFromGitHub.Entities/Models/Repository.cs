namespace SearchUserFromGitHub.Entities.Models
{
    /// <summary>
    /// Model for repository
    /// </summary>
    public class Repository
    {
        /// <param name="name">Repository name</param>
        public string name { get; set; }

        /// <param name="language">Repository language</param>
        public string language { get; set; }

        /// <param name="highestStargazerCount">Results with highest rate</param>
        public int highestStargazerCount { get; set; }
    }
}