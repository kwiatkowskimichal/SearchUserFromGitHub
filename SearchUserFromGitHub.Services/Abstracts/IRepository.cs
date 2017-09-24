using System.Threading.Tasks;
using SearchUserFromGitHub.Entities.Models;

namespace SearchUserFromGitHub.Services.Abstracts
{
    /// <summary>
    /// Interface for user
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Method takes user by name
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <returns>User</returns>
        Task<User> GetUserAsync(string name);
    }
}