using System.Collections.Generic;

namespace SearchUserFromGitHub.Entities.Models
{
    /// <summary>
    /// Model for user
    /// </summary>
    public class User
    {
        /// <param name="userDetails">User's details</param>
        /// <typeparam name="userDetails">The element type of UserDetails</typeparam>
        /// <seealso cref="SearchUserFromGitHub.Entities.Models.UserDetails"/>
        public UserDetails userDetails { get; set; }

        /// <param name="repository">User's repository</param>
        /// <typeparam name="List<Repository>">The element type of List<Repository></typeparam>
        /// <seealso cref="SearchUserFromGitHub.Entities.Models.Repository"/>
        public List<Repository> repository { get; set; }
    }
}