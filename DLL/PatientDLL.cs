using System.Collections.Generic;
using DTO;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Configuration;

namespace DLL
{

	public class PatientDLL
	{
		// Store the connection string found in the Web.config to _connectionString variable.
		private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

		public void InsertMedication(PatientDTO medication)
		{
			//Establish a connection to the SQL Server Database.
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				// Define what Stored Procedure will be used for this function.
				SqlCommand cmd = new SqlCommand("InsertPatient", conn);
				// Assign values to its parameters.
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Dosage", medication.Dosage);
				cmd.Parameters.AddWithValue("@Drug", medication.Drug);
				cmd.Parameters.AddWithValue("@PatientName", medication.Patient);

				// Open the connection
				conn.Open();

				// Execute the sql query
				cmd.ExecuteNonQuery();
			}
		}


		public void UpdateMedication(PatientDTO medication)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("UpdatePatient", conn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id", medication.Id);
				cmd.Parameters.AddWithValue("@Dosage", medication.Dosage);
				cmd.Parameters.AddWithValue("@Drug", medication.Drug);
				cmd.Parameters.AddWithValue("@PatientName", medication.Patient);

				conn.Open();
				cmd.ExecuteNonQuery();
			}

		}

		public void DeleteMedication(int id)
		{
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("DeletePatient", conn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id", id);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public List<PatientDTO> GetAllMedications()
		{
			List<PatientDTO> medications = new List<PatientDTO>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM Patient", conn);
				conn.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						medications.Add(new PatientDTO
						{
							Id = Convert.ToInt32(reader["Id"]),
							Dosage = Convert.ToDecimal(reader["Dosage"]),
							Drug = reader["Drug"].ToString(),
							Patient = reader["PatientName"].ToString(),
							ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"])
						});
					}
				}
			}
			return medications;
		}


		public PatientDTO GetPatientById(int Id)
		{
			PatientDTO patient = null;

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open(); // Open the connection
				string query = "SELECT Id, Drug, Dosage, PatientName FROM Patient WHERE Id = @Id";
				// Select all the values of a certain record, and determine which id will be matched.
				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
					// Based on the @ fill it up with the following value "Id".
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							patient = new PatientDTO
							{
								Id = reader.GetInt32(0), // Column index 0: Id.
								Drug = reader.GetString(1), // Column index 1: Drug.
								Dosage = reader.GetDecimal(2), // Column index 2: Dosage (assuming it's decimal).
								Patient = reader.GetString(3) // Column index 3: Patient.
							};
						}
					}
				}
			}
			// Return the stored patient based on reader.
			return patient;
		}
	}
}