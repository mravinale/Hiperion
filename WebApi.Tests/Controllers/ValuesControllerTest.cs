using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using Moq;
using NUnit.Framework;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Tests.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Hosting;
    using System.Web.Http.Routing;

    using Newtonsoft.Json;

    [TestFixture]
    public class ValuesControllerTest
    {
        private List<UserDto> userList;

        [SetUp]
        public void initData()
        {
             userList = new List<UserDto>
                {
                    new UserDto
                        {
                            Id= 1,
                            Address = "Address1",
                            Name = "Name1"
                        },
                    new UserDto
                        {
                            Id= 2,
                            Address = "Address2",
                            Name = "Name2"
                        }
                };
        }

        [Test]
        public void Get()
        {
            //Arrange
            var userServiceMock = new Mock<IUserServices>();
            userServiceMock.Setup(foo => foo.GetAllUsers()).Returns(userList);

            var controller = new UserController(userServiceMock.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Name1", result.ElementAt(0).Name);
            Assert.AreEqual("Name2", result.ElementAt(1).Name);
        }

        [Test]
        public void Post()
        {
            //Arrange
            var userServiceMock = new Mock<IUserServices>();
            userServiceMock.Setup(foo => foo.SaveOrUpdateUser(userList.ElementAt(0))).Returns(true);

            var jsonSerializerSettings = new JsonSerializerSettings();
            var jsonNetFormatter = new JsonNetFormatter(jsonSerializerSettings); 

            var controller = SetupControllerForTests(userServiceMock);
          
            // Act
            var result = controller.Post(userList.ElementAt(0));
            var contentResult = result.Content.ReadAsAsync(typeof(UserDto), new[] { jsonNetFormatter }).Result as UserDto;
            
            // Assert
            Assert.IsNotNull(contentResult);
        }

        public static UserController SetupControllerForTests(Mock<IUserServices> userServices)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/api/user")
                { RequestUri = new Uri("http://localhost/api/user") };
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "user" } });
            var employeesController = new UserController(userServices.Object)
            {
                ControllerContext = new HttpControllerContext(config, routeData, request),
                Request = request
            };
            employeesController.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            employeesController.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            return employeesController;
        }

        //[TestMethod]
        //public void Put()
        //{
        //    // Disponer
        //    ValuesController controller = new ValuesController();

        //    // Actuar
        //    controller.Put(5, "value");

        //    // Declarar
        //}

        //[TestMethod]
        //public void Delete()
        //{
        //    // Disponer
        //    ValuesController controller = new ValuesController();

        //    // Actuar
        //    controller.Delete(5);

        //    // Declarar
        //}
    }

    public class JsonNetFormatter : MediaTypeFormatter
    {
        private readonly JsonSerializerSettings _settings;
        private readonly Encoding encoding;

        public JsonNetFormatter(JsonSerializerSettings settings)
        {
            _settings = settings ?? new JsonSerializerSettings();

            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            encoding = new UTF8Encoding(false, true);
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        protected Task<object> OnReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders)
        {
            var ser = JsonSerializer.Create(_settings);

            return Task.Factory.StartNew(() =>
            {
                using (var sr = new StreamReader(stream, encoding))
                using (var jsonReader = new JsonTextReader(sr))
                {
                    var result = ser.Deserialize(jsonReader, type);
                    return result;
                }
            });
        }

        protected Task OnWriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
        {
            var ser = JsonSerializer.Create(_settings);

            return Task.Factory.StartNew(() =>
            {
                using (var jsonWriter = new JsonTextWriter(new StreamWriter(stream)))
                {
                    ser.Serialize(jsonWriter, value);
                    jsonWriter.Flush();
                }
            });
        }
    }
}
