using log4net;
using SearchUserFromGitHub.Entities.Models;
using SearchUserFromGitHub.Services.Abstracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SearchUserFromGitHub.Controllers
{
    /// <summary>
    /// Core of default controller
    /// </summary>
    public class HomeController : Controller
    {
        /// <param name="_git">Instance of git</param>
        /// <typeparam name="IRepository">The element type of IRepository</typeparam>
        /// <seealso cref="SearchUserFromGitHub.Services.Abstracts.IRepository"/>
        readonly IRepository _git = null;

        /// <param name="logger">Instance of logger</param>
        /// <typeparam name="ILog">The element type of ILog</typeparam>
        /// <seealso cref="log4net.ILog"/>
        ILog logger = LogManager.GetLogger("File");

        /// <summary>
        /// Default controller
        /// </summary>
        /// <param name="_git">Injected instance</param>
        public HomeController(IRepository _git)
        {
            this._git = _git;
        }

        /// <summary>
        /// Default view
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            logger.Info("Starting page load");

            return View();
        }

        /// <summary>
        /// Retrives instance of user if exists
        /// </summary>
        /// <param name="name">User's name</param>
        /// <returns>User in Json format</returns>
        public async Task<JsonResult> GetUserFromGitAsync(string name)
        {
            logger.Info(string.Format("Requested about: {0}", name));

            User user = await _git.GetUserAsync(name);

            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}