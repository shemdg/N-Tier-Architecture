using PatientManagementSystem.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace PatientManagementSystem.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Admin";
			}
		}
		// Registers a route in the Admin area.
		public override void RegisterArea(AreaRegistrationContext context)
		{   //Register the route of the admin in the Admin area.                    
			context.MapRoute(
		  "Admin_default",
		  "Admin/{controller}/{action}/{id}",
		  new { controller = "Patient", action = "Index", id = UrlParameter.Optional },
		  new { id = @"\d*" } // Ensure ID is an optional integer
	  );

		}
	}
}