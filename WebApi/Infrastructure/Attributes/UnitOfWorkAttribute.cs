

using System;
using System.Data.Entity;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Castle.Windsor;
using WebApi.Infrastructure.EF;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace WebApi.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class UnitOfWorkAttribute : ActionFilterAttribute
	{ 
		public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
		{
			if (actionExecutedContext.Exception != null) return;

			var container = actionExecutedContext.ActionContext.ControllerContext.Configuration.DependencyResolver;
			var context = container.GetService(typeof (IDbContext)) as IDbContext;
			context.SaveChanges();
		}
	}
}