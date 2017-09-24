using System.Collections.Generic;

namespace SearchUserFromGitHub.Entities.Models
{
    /// <summary>
    /// Model for account
    /// </summary>
    public class Account
    {
        /// <param name="user">User's git account</param>
        /// <typeparam name="Octokit.User">The element type of Octokit.User</typeparam>
        /// <seealso cref="Octokit.User"/>
        public Octokit.User user { get; set; }

        /// <param name="repositories">User's git repository</param>
        /// <typeparam name="IEnumerable<Octokit.Repository>">The element type of IEnumerable<Octokit.Repository></typeparam>
        /// <seealso cref="Octokit.Repository"/>
        public IEnumerable<Octokit.Repository> repositories { get; set; }
    }
}