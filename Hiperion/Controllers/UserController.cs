namespace Hiperion.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Infrastructure.Attributes;
    using Models;
    using Services;

    [UnitOfWork]
    public class UserController : ApiController
    {
        private readonly IUserServices userServices;

        public UserController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

		[HttpGet]// GET api/user
		public HttpResponseMessage Get()
        {
	        var users = userServices.GetAllUsers();
			return users == null ? Request.CreateResponse(HttpStatusCode.NotFound) : 
								   Request.CreateResponse(HttpStatusCode.OK, users);
        }

		[HttpPost]// POST api/user
        public HttpResponseMessage Post(UserDto user)
        {
            userServices.SaveOrUpdateUser(user);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpDelete]// DELETE api/user/5
        public HttpResponseMessage Delete(int id)
        {
            userServices.DeleteUser(id); 
			return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}