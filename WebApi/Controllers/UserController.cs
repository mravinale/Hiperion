using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Infrastructure.Attributes;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [UnitOfWork]
    public class UserController : ApiController
    {
        private readonly IUserServices userServices;

        public UserController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]// GET api/values
		public HttpResponseMessage Get()
        {
	        var users = userServices.GetAllUsers();
			return users == null ? Request.CreateResponse(HttpStatusCode.NotFound) : 
								   Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [HttpPost]// POST api/values
        public HttpResponseMessage Post(UserDto user)
        {
            userServices.SaveOrUpdateUser(user);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpDelete]// DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            userServices.DeleteUser(id); 
			return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}