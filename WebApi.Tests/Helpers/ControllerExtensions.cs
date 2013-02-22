namespace WebApi.Tests.Controllers
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;

    using Moq;

    using WebApi.Controllers;
    using WebApi.Services;

    public static class ControllerExtensions
    {
        public static void SetupControllerForTests(this ApiController userController)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/user")
                { RequestUri = new Uri("http://localhost/api/user") };
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "user" } });

            userController.ControllerContext = new HttpControllerContext(config, routeData, request);
            userController.Request = request;

            userController.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
            userController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }
    }
}