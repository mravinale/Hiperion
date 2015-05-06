namespace Hiperion.Tests.Helpers
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;

    using Newtonsoft.Json;

    using Controllers;
    using System.Configuration;

    public static class ObjectExtensions
    {
        public static void SetupController<T>(this ApiController userController) where T : class
        {
            var entityString = typeof(T).Name.ToLower().Replace("Dto","");
            var requestUriBase = ConfigurationManager.AppSettings["serviceBaseUri"] + "api/";

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, requestUriBase) { RequestUri = new Uri(requestUriBase + entityString) };
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", entityString } });

            userController.ControllerContext = new HttpControllerContext(config, routeData, request);
            userController.Request = request;

            userController.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
            userController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        public static T GetContent<T>(this HttpResponseMessage responseMessage) where T : class 
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            var jsonNetFormatter = new JsonNetFormatter(jsonSerializerSettings);

            return responseMessage.Content.ReadAsAsync(typeof(T), new[] { jsonNetFormatter }).Result as T;
        }
    }
}