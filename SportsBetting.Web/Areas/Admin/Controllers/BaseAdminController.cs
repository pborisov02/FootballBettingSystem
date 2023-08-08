namespace SportsBettingSystem.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;

	[Area("Admin")]
	[Authorize(Roles = "Administrator")]
	public class BaseAdminController : Controller
	{
		
	}
}
