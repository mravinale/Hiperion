using System;
using NUnit.Framework;
using System.Net.Http;
using System.Configuration;
using System.Net;
using System.Net.Http.Headers;

namespace Hiperion.Tests.HttpResponse
{
    public class HttpResponseTests
    {
        [Test]
        public void GetResponseIsSuccess()
        {
            //Arrange
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["serviceBaseUri"]) };

            //Act
            var response = client.GetAsync("api/user/get").Result;

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [Test]
        public void GetResponseIsJson()
        {
            //Arrange
            var client = new HttpClient{ BaseAddress = new Uri(ConfigurationManager.AppSettings["serviceBaseUri"]) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            var response = client.GetAsync("api/contacts/get").Result;

            //Assert
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
        }

        [Test]
        public void GetAuthenticationStatus()
        {
            //Arrange
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["serviceBaseUri"]) };

            //Act
            var response = client.GetAsync("api/contacts/get").Result;

            //Assert
            Assert.AreNotEqual(HttpStatusCode.Unauthorized, response.StatusCode);

        }
    }
}
