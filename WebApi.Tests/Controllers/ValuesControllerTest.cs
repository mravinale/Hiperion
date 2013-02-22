using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Tests.Controllers
{
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

            var controller = new UserController(userServiceMock.Object);
            controller.SetupControllerForTests();
          
            // Act
            var result = controller.Post(userList.ElementAt(0));
            var contentResult = result.Content.ReadAsAsync(typeof(UserDto), new[] { jsonNetFormatter }).Result as UserDto;
            
            // Assert
            Assert.IsNotNull(contentResult);
        }
    }
}
