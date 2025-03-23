using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using BLL;
using DTO;
using PagedList;

namespace PatientManagementSystem.Areas.Admin.Controllers
{
	public class PatientController : Controller
	{
		private readonly PatientService _service;

		//Create a Constructor to create an instance of Patient Service
		public PatientController()
		{
			_service = new PatientService();
		}

		// GET: Admin/Patient/Index - with Pagination
		public ActionResult Index()
		{
			//int pageSize = 10; // Number of records per page
			//int pageNumber = (page ?? 1); // Default to page 1 if no page is specified

			//var medications = _service.GetAllMedications().ToPagedList(pageNumber, pageSize);

			return View();
		}

		// POST: Admin/Patient/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(PatientDTO medication)
		{

			if (ModelState.IsValid)
			{
				string result = _service.AddMedication(medication); // Create if new

				if (result == "Success")
				{
					// Add success message to TempData for displaying in the view
					TempData["SuccessMessage"] = medication.Id > 0 ? "Medication updated successfully!" : "Medication added successfully!";
					return RedirectToAction("Index", "Patient", new { area = "Admin" });
				}
				else
				{
					TempData["ErrorMessage"] = result;
				}

				ModelState.AddModelError("", result); // Add validation message for every input fields
			}

			return View(medication);
		}

		// GET: Admin/Patient/Create/{Id - Optional}
		[HttpGet]
		public ActionResult Create(int? id)
		{
			PatientDTO model = id.HasValue
				? _service.GetPatientById(id.Value) // Fetch existing record  
				: new PatientDTO(); // Otherwise, create a new one  

			if (id.HasValue && model == null)
				return HttpNotFound(); // Ensure record exists  

			return View(model);
		}

		// POST: Admin/Patient/Edit
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Edit(PatientDTO medication)
{
	string result = _service.EditMedication(medication); // Edit

	if (result == "Success")
	{
		// Add success message to TempData for displaying in the view
		TempData["SuccessMessage"] = medication.Id > 0 ? "Medication updated successfully!" : "Medication added successfully!";
		return RedirectToAction("Index", "Patient", new { area = "Admin" });
	}
	else
	{
		TempData["ErrorMessage"] = result;
	}

	ModelState.AddModelError("", result); // Add validation message for every input fields

	return View(medication);
}


		// GET: Admin/Patient/Edit/{id}
		[HttpGet]
		public ActionResult Edit(int? id)
		{
			PatientDTO model = _service.GetPatientById(id.Value);
			if (model == null)
				return HttpNotFound(); // Ensure record exists

			return View(model);
		}

		// POST: Admin/Patient/Delete/{Id - Optional}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int? id)
		{
			string result = _service.RemoveMedication(id.Value);

			if (result == "Success")
			{
				TempData["DeletedMessage"] = "Record successfully deleted.";
			}
			else
			{
				TempData["DeletedErrorMessage"] = result;
			}

			// Redirect to Index after the POST to avoid resubmission on refresh
			return RedirectToAction("Index", "Patient", new { area = "Admin" });
		}

		public ActionResult Error505()
		{
			return View(); // Redirect to Error 505
		}

		public ActionResult Error404()
		{
			return View(); // Redirect to Error 404
		}
	}
}