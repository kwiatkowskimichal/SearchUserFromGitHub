using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using SearchUserFromGitHub.Services.Abstracts;
using SearchUserFromGitHub.Services.Services;
using System.Configuration;

namespace SearchUserFromGitHub
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IRepository, GitHubManger>(new InjectionConstructor(ConfigurationManager.AppSettings["Login"], ConfigurationManager.AppSettings["Password"]));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}