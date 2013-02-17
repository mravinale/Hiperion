using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    using WebApi.Models;
    using WebApi.Services;

    public class UserController : ApiController
    {
        private readonly UserServices userServices;

        public UserController(UserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]// GET api/values
        public IEnumerable<UserDto> Get()
        {
            return this.userServices.GetAllUsers();
        }

        [HttpPost]// POST api/values
        public HttpResponseMessage Post(UserDto user)
        {
            userServices.SaveOrUpdateUser(user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]// DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            userServices.DeleteUser(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}