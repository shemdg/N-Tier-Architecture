﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementSystem.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
		// GET: Admin/Home
		public ActionResult About()
		{
			return View();
		}
	}
}