using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchUserFromGitHub.Services.Services;
using System.Configuration;
using System.Web;
using SearchUserFromGitHub.Entities.Models;
using System.Threading.Tasks;

namespace SearchUserFromGitHub.Tests.Services
{
    [TestClass]
    public class GitHubManagerTests
    {
        /// <summary>
        /// Test sholud give exception, if login is not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void ThrowExceptionIfLoginIsEmpty()
        {
            GitHubManger gitHubManger = new GitHubManger("", ConfigurationManager.AppSettings["Password"]);
        }

        /// <summary>
        /// Test sholud give exception, if password is not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void ThrowExceptionIfPasswordIsEmpty()
        {
            GitHubManger gitHubManger = new GitHubManger(ConfigurationManager.AppSettings["Login"], "");
        }

        /// <summary>
        /// Test sholud give exception, if credentials is not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void ThrowExceptionIfCredentialsAreEmpty()
        {
            GitHubManger gitHubManger = new GitHubManger("", "");
        }

        /// <summary>
        /// Test sholud give exception, if login is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionIfLoginIsNull()
        {
            GitHubManger gitHubManger = new GitHubManger(null, ConfigurationManager.AppSettings["Password"]);
        }

        /// <summary>
        /// Test sholud give exception, if password is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionIfPasswordIsNull()
        {
            GitHubManger gitHubManger = new GitHubManger(ConfigurationManager.AppSettings["Login"], null);
        }

        /// <summary>
        /// Test sholud give exception, if credentials are null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionIfCredentialsAreNull()
        {
            GitHubManger gitHubManger = new GitHubManger(null, null);
        }

        /// <summary>
        /// Test sholud not give exception, if credentials are provided
        /// </summary>
        [TestMethod]
        public void DoesNotThrowExceptionIfCredientialsAreProvided()
        {
            try
            {
                GitHubManger gitHubManger = new GitHubManger(ConfigurationManager.AppSettings["Login"], ConfigurationManager.AppSettings["Password"]);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        /// <summary>
        /// Test pass if object Rob Conery is not null
        /// </summary>
        [TestMethod]
        public async Task DoesRobConeryExists()
        {
            User results = await RobConeryRepository();

            Assert.IsNotNull(results, "Rob Conery does not exists");
        }

        /// <summary>
        /// Test pass if object Rob Conery contains 5 repositories
        /// </summary>
        [TestMethod]
        public async Task DoesRobConeryHas5Repository()
        {
            User results = await RobConeryRepository();

            Assert.AreEqual(results.repository.Count, 5);
        }

        /// <summary>
        /// Test pass if object Rob Conery contains name not empty
        /// </summary>
        [TestMethod]
        public async Task DoesRobConeryHasName()
        {
            User results = await RobConeryRepository();

            Assert.AreNotEqual(results.userDetails.name, "");
        }

        /// <summary>
        /// Test pass if object Rob Conery contains name with exactly the same string 'Rob Conery'
        /// </summary>
        [TestMethod]
        public async Task DoesRobConeryIsRobConery()
        {
            User results = await RobConeryRepository();

            Assert.AreEqual(results.userDetails.name, "Rob Conery");
        }

        /// <summary>
        /// Test pass if return object is null, becouse this user not exists for sure
        /// </summary>
        [TestMethod]
        public async Task DoesUserNotExists()
        {
            GitHubManger controller = new GitHubManger(ConfigurationManager.AppSettings["Login"], ConfigurationManager.AppSettings["Password"]);
            User results = await controller.GetUserAsync("userNotExistsForSure456");

            Assert.IsNull(results.repository);
        }

        /// <summary>
        /// Helper method, return Rob Conery object
        /// </summary>
        static async Task<User> RobConeryRepository()
        {
            GitHubManger controller = new GitHubManger(ConfigurationManager.AppSettings["Login"], ConfigurationManager.AppSettings["Password"]);
            User results = await controller.GetUserAsync("robconery");

            return results;
        }
    }
}