using log4net;
using System.Web.Mvc;

namespace SearchUserFromGitHub.Controllers
{
    public class ErrorController : Controller
    {
        /// <param name="logger">Instance of logger</param>
        /// <typeparam name="ILog">The element type of ILog</typeparam>
        /// <seealso cref="log4net.ILog"/>
        ILog logger = LogManager.GetLogger("File");

        /// <summary>
        /// Status code: 500 error server
        /// </summary>
        /// <returns></returns>
        public ActionResult ServerError()
        {
            logger.Error("Error server");

            return View();
        }

        /// <summary>
        /// Status code: 400 error server
        /// </summary>
        /// <returns></returns>
        public ActionResult ServerErrorUnauthorizedAccess()
        {
            logger.Error("Try to unauthorized attempt");

            return View();
        }
        
    }
}