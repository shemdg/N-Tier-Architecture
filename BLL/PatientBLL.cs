using DLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	public class PatientService
	{
		private readonly PatientDLL _repository;

		public PatientService()
		{
			//Creates a constructor to make an instance of Patient Repository Class (DAL).
			_repository = new PatientDLL();
		}

		public string AddMedication(PatientDTO medication)
		{
			// Create validation first before calling the Insert Medication Function (DAL).
			string validationMessage = ValidateMedication(medication);
			if (!string.IsNullOrEmpty(validationMessage))
				return validationMessage;

			_repository.InsertMedication(medication);
			return "Success";
		}

		public string RemoveMedication(int id)
		{
			if (id <= 0)
				return "Invalid medication ID.";

			_repository.DeleteMedication(id);
			return "Success";
		}

		public string EditMedication(PatientDTO medication)
		{
			string validationMessage = ValidateMedication(medication);
			if (!string.IsNullOrEmpty(validationMessage))
				return validationMessage;

			_repository.UpdateMedication(medication);
			return "Success";
		}

		public List<PatientDTO> GetAllMedications()
		{
			return _repository.GetAllMedications();
		}

		private string ValidateMedication(PatientDTO medication)
		{
			// All possible validation within input fields.
			if (medication == null)
				return "Medication cannot be null.";

			if (medication.Dosage <= 0)
				return "Dosage must be greater than zero.";

			if (string.IsNullOrWhiteSpace(medication.Drug) || medication.Drug.Length > 50)
				return "Drug name is required and should not exceed 50 characters.";

			if (string.IsNullOrWhiteSpace(medication.Patient) || medication.Patient.Length > 50)
				return "Patient name is required and should not exceed 50 characters.";

			return string.Empty;
		}

		public PatientDTO GetPatientById(int id)
		{
			// Create an instance of getting the ID of the patient coming from then _repository (Patient Repository) - DAL
			return _repository.GetPatientById(id);
		}
	}
}