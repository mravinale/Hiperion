namespace Hiperion.Tests.HttpResponse
{
    using System;
    using NUnit.Framework;
    using System.Net.Http;
    using System.Configuration;
    using System.Net;
    using System.Net.Http.Headers;

    public class HttpResponseTests
    {
        [Test, Ignore]
        public void GetResponseIsSuccess()
        {
            //Arrange
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["serviceBaseUri"]) };

            //Act
            var response = client.GetAsync("api/user/get").Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


		 [Test, Ignore]
        public void GetResponseIsJson()
        {
            //Arrange
            var client = new HttpClient{ BaseAddress = new Uri(ConfigurationManager.AppSettings["serviceBaseUri"]) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            var response = client.GetAsync("api/user/get").Result;

            //Assert
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
        }

		[Test, Ignore]
        public void GetAuthenticationStatus()
        {
            //Arrange
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["serviceBaseUri"]) };

            //Act
			var response = client.GetAsync("api/user/get").Result;

            //Assert
            Assert.AreNotEqual(HttpStatusCode.Unauthorized, response.StatusCode);

        }
    }
}
