USE PatientDB;

CREATE TABLE Patient (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    Dosage DECIMAL(7,4) NOT NULL, 
    Drug NVARCHAR(50) NOT NULL, 
    PatientName NVARCHAR(50) NOT NULL, 
    ModifiedDate DATETIME NOT NULL DEFAULT GETDATE() 
);

-- -------------------------------------------------------------

CREATE PROCEDURE InsertPatient
	@Dosage DECIMAL(7,4),
	@Drug NVARCHAR(MAX),
	@PatientName NVARCHAR(MAX)
AS
BEGIN 
	INSERT INTO Patient(Dosage, Drug, PatientName, ModifiedDate)
	VALUES (@Dosage, @Drug, @PatientName, GETDATE());
END;
GO

CREATE PROCEDURE UpdatePatient
	@id INT,
	@Dosage DECIMAL(7,4),
	@Drug NVARCHAR(MAX),
	@PatientName NVARCHAR(MAX)
AS
BEGIN
	UPDATE Patient	
	SET Dosage = @Dosage,
		Drug = @Drug,
		PatientName = @PatientName,
		ModifiedDate = GETDATE()
	WHERE Id = @id
END;
GO

CREATE PROCEDURE DeletePatient
	@id INT
AS 
BEGIN
	DELETE FROM Patient WHERE Id = @Id;
END;
GO
