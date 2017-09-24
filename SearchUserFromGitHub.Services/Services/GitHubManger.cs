using SearchUserFromGitHub.Services.Abstracts;
using System;
using System.Threading.Tasks;
using Octokit;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Web;

namespace SearchUserFromGitHub.Services.Services
{
    /// <summary>
    /// Class for managing user
    /// </summary>
    public class GitHubManger : IRepository
    {
        /// <param name="APP_NAME">Name of application</param>
        const string APP_NAME = "SearchUserFromGitHub";

        /// <param name="COUNT">How many repositories we need</param>
        const sbyte COUNT = 5;

        /// <param name="gitHubClient">Octokit object</param>
        /// <seealso cref="Octokit.GitHubClient"/>
        GitHubClient _gitHubClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="login">Login user for authenticate</param>
        /// <param name="pass">Password user for authenticate</param>
        public GitHubManger(string login, string pass) {
            _gitHubClient = GetConnectionToRepository(login, pass);
        }

        /// <seealso cref="SearchUserFromGitHub.Services.Abstracts.IRepository"/>
        public async Task<Entities.Models.User> GetUserAsync(string name)
        {            
            Entities.Models.User userFromGit = await GetUserFromGitAsync(name);

            return userFromGit;
        }

        /// <summary>
        /// Establish connection to git 
        /// </summary>
        /// <param name="login">Login user for authenticate</param>
        /// <param name="pass">Password user for authenticate</param>
        /// <returns>GitHubClient</returns>
        GitHubClient GetConnectionToRepository(string login, string pass)
        {
            if (login.Equals("") || pass.Equals(""))
                throw new HttpException(400, "Please fill credentials in web.config");

            Credentials credentials = new Credentials(login, pass);
            Connection connection = new Connection(new ProductHeaderValue(APP_NAME)){ Credentials = credentials };

            return new GitHubClient(connection);
        }

        /// <summary>
        /// Method gets user if exists, otherwise returns empty object
        /// </summary>
        /// <param name="name">User's name</param>
        /// <returns>Entities.Models.User</returns>
        async Task<Entities.Models.User> GetUserFromGitAsync(string name)
        {
            Entities.Models.User user = new Entities.Models.User();

            if (await CheckIfUserExistsAsync(name))
            {
                User hubUser = await _gitHubClient.User.Get(name);
                IEnumerable<Repository> repositores = await GetRepositoresAsync(name);

                Entities.Models.Account account = new Entities.Models.Account()
                {                    
                    user = hubUser,
                    repositories = repositores
                };

                user = GetGitUser(account);
            }

            return user;
        }

        /// <summary>
        /// Method gets user details and repositories
        /// </summary>
        /// <param name="account">Entities.Models.Account</param>
        /// <returns>Entities.Models.User</returns>
        Entities.Models.User GetGitUser(Entities.Models.Account account)
        {
            return new Entities.Models.User()
            {
                userDetails = new Entities.Models.UserDetails()
                {
                    name = account.user.Name ?? "-",
                    location = account.user.Location ?? "-",
                    avatarUrl = account.user.AvatarUrl ?? "-"
                },                
                repository = account.repositories.Select(z => new Entities.Models.Repository()
                                                                                            {
                                                                                                name = z.Name ?? "-",
                                                                                                language = z.Language ?? "-",
                                                                                                highestStargazerCount = z.StargazersCount
                                                                                            }
                                                                                           ).ToList()
            };
        }

        /// <summary>
        /// Get repositories top 5
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <returns>Repositories</returns>
        async Task<IEnumerable<Repository>> GetRepositoresAsync(string name)
        {
            IEnumerable<Repository> allRepositoriesForUser = await _gitHubClient.Repository.GetAllForUser(name);

            return allRepositoriesForUser.OrderByDescending(z => z.StargazersCount).Take(COUNT);
        }

        /// <summary>
        /// Method check if user exists
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <returns>bool</returns>
        async Task<bool> CheckIfUserExistsAsync(string name)
        {
            var searchResult = await _gitHubClient.Search.SearchUsers(new SearchUsersRequest(name));

            return searchResult.Items.Any(z => z.Login == name);
        }
    }
}