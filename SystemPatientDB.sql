/*Nombre: Jhon Michael
 Matricula: 20251861*/

CREATE DATABASE PatientSystemDB;
GO
USE PatientSystemDB;
GO

CREATE TABLE Patients (
    PatientId        INT IDENTITY(1,1) PRIMARY KEY,
    FirstName        NVARCHAR(50)  NOT NULL,
    LastName         NVARCHAR(50)  NOT NULL,
    IdCard           NVARCHAR(20)  NOT NULL UNIQUE,
    DateOfBirth      DATE          NOT NULL,
    Gender           CHAR(1)       NOT NULL CHECK (Gender IN ('M','F')),
    PhoneNumber      NVARCHAR(20)  NOT NULL,
    Address          NVARCHAR(150) NOT NULL,
    Email            NVARCHAR(100) NOT NULL,
    RegistrationDate DATETIME      NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Doctors (
    DoctorId    INT IDENTITY(1,1) PRIMARY KEY,
    FirstName   NVARCHAR(50)  NOT NULL,
    LastName    NVARCHAR(50)  NOT NULL,
    Specialty   NVARCHAR(60)  NOT NULL,
    PhoneNumber NVARCHAR(20)  NOT NULL,
    Email       NVARCHAR(100) NOT NULL,
    Schedule    NVARCHAR(100) NOT NULL
);

CREATE TABLE Appointments (
    AppointmentId   INT IDENTITY(1,1) PRIMARY KEY,
    PatientId       INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientId),
    DoctorId        INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorId),
    AppointmentDate DATETIME      NOT NULL,
    Reason          NVARCHAR(150) NOT NULL,
    Status          NVARCHAR(15)  NOT NULL DEFAULT 'Scheduled'
                    CHECK (Status IN ('Scheduled','Completed','Cancelled'))
);

CREATE TABLE MedicalHistory (
    HistoryId         INT IDENTITY(1,1) PRIMARY KEY,
    PatientId         INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientId),
    Allergies         NVARCHAR(200) NOT NULL,
    MedicalConditions NVARCHAR(200) NOT NULL,
    PreviousSurgeries NVARCHAR(200) NOT NULL,
    FamilyHistory     NVARCHAR(200) NOT NULL
);

CREATE TABLE Consultations (
    ConsultationId   INT IDENTITY(1,1) PRIMARY KEY,
    PatientId        INT NOT NULL FOREIGN KEY REFERENCES Patients(PatientId),
    DoctorId         INT NOT NULL FOREIGN KEY REFERENCES Doctors(DoctorId),
    ConsultationDate DATETIME      NOT NULL,
    Symptoms         NVARCHAR(200) NOT NULL,
    Diagnosis        NVARCHAR(200) NOT NULL,
    Observations     NVARCHAR(200) NOT NULL
);

CREATE TABLE Treatments (
    TreatmentId    INT IDENTITY(1,1) PRIMARY KEY,
    ConsultationId INT NOT NULL FOREIGN KEY REFERENCES Consultations(ConsultationId),
    TreatmentName  NVARCHAR(80)  NOT NULL,
    Description    NVARCHAR(200) NOT NULL,
    Medication     NVARCHAR(100) NOT NULL,
    StartDate      DATE NOT NULL,
    EndDate        DATE NOT NULL,
    CONSTRAINT CK_Treatment_Dates CHECK (EndDate >= StartDate)
);
GO

INSERT INTO Patients (FirstName, LastName, IdCard, DateOfBirth, Gender, PhoneNumber, Address, Email)
VALUES ('Maria', 'Perez', '001-1234567-8', '1995-04-12', 'F', '809-555-0101', 'Calle 1, Santo Domingo', 'maria@mail.com');

INSERT INTO Doctors (FirstName, LastName, Specialty, PhoneNumber, Email, Schedule)
VALUES ('Carlos', 'Gomez', 'General Medicine', '809-555-0202', 'carlos@clinic.com', 'Mon-Fri 8:00-14:00');
GO

/*Nombre: Jhon Michael
 Matricula: 20251861*/

USE PatientSystemDB;
GO


IF NOT EXISTS (SELECT 1 FROM Patients WHERE IdCard = '001-2345678-9')
    INSERT INTO Patients (FirstName, LastName, IdCard, DateOfBirth, Gender, PhoneNumber, Address, Email)
    VALUES ('Juan', 'Rodriguez', '001-2345678-9', '1988-09-23', 'M', '809-555-0102', 'Av. Venezuela 45, Santo Domingo Este', 'juan.rodriguez@mail.com');

IF NOT EXISTS (SELECT 1 FROM Patients WHERE IdCard = '001-3456789-0')
    INSERT INTO Patients (FirstName, LastName, IdCard, DateOfBirth, Gender, PhoneNumber, Address, Email)
    VALUES ('Ana', 'Martinez', '001-3456789-0', '2001-01-30', 'F', '809-555-0103', 'Calle Duarte 12, Los Mina', 'ana.martinez@mail.com');

IF NOT EXISTS (SELECT 1 FROM Patients WHERE IdCard = '001-4567890-1')
    INSERT INTO Patients (FirstName, LastName, IdCard, DateOfBirth, Gender, PhoneNumber, Address, Email)
    VALUES ('Luis', 'Fernandez', '001-4567890-1', '1975-06-15', 'M', '809-555-0104', 'Calle Mella 78, San Isidro', 'luis.fernandez@mail.com');

IF NOT EXISTS (SELECT 1 FROM Patients WHERE IdCard = '001-5678901-2')
    INSERT INTO Patients (FirstName, LastName, IdCard, DateOfBirth, Gender, PhoneNumber, Address, Email)
    VALUES ('Carmen', 'Santos', '001-5678901-2', '1999-12-03', 'F', '809-555-0105', 'Av. Charles de Gaulle 210', 'carmen.santos@mail.com');

IF NOT EXISTS (SELECT 1 FROM Doctors WHERE Email = 'carlos@clinic.com')
    INSERT INTO Doctors (FirstName, LastName, Specialty, PhoneNumber, Email, Schedule)
    VALUES ('Carlos', 'Gomez', 'General Medicine', '809-555-0202', 'carlos@clinic.com', 'Mon-Fri 8:00-14:00');

IF NOT EXISTS (SELECT 1 FROM Doctors WHERE Email = 'laura.diaz@clinic.com')
    INSERT INTO Doctors (FirstName, LastName, Specialty, PhoneNumber, Email, Schedule)
    VALUES ('Laura', 'Diaz', 'Pediatrics', '809-555-0203', 'laura.diaz@clinic.com', 'Mon-Thu 9:00-15:00');

IF NOT EXISTS (SELECT 1 FROM Doctors WHERE Email = 'pedro.almonte@clinic.com')
    INSERT INTO Doctors (FirstName, LastName, Specialty, PhoneNumber, Email, Schedule)
    VALUES ('Pedro', 'Almonte', 'Cardiology', '809-555-0204', 'pedro.almonte@clinic.com', 'Tue-Fri 10:00-16:00');

IF NOT EXISTS (SELECT 1 FROM Doctors WHERE Email = 'sofia.reyes@clinic.com')
    INSERT INTO Doctors (FirstName, LastName, Specialty, PhoneNumber, Email, Schedule)
    VALUES ('Sofia', 'Reyes', 'Dermatology', '809-555-0205', 'sofia.reyes@clinic.com', 'Mon-Wed 8:00-12:00');

DECLARE @p1 INT = (SELECT PatientId FROM Patients WHERE IdCard = '001-1234567-8');
DECLARE @p2 INT = (SELECT PatientId FROM Patients WHERE IdCard = '001-2345678-9');
DECLARE @p3 INT = (SELECT PatientId FROM Patients WHERE IdCard = '001-3456789-0');
DECLARE @p4 INT = (SELECT PatientId FROM Patients WHERE IdCard = '001-4567890-1');
DECLARE @p5 INT = (SELECT PatientId FROM Patients WHERE IdCard = '001-5678901-2');

DECLARE @d1 INT = (SELECT DoctorId FROM Doctors WHERE Email = 'carlos@clinic.com');
DECLARE @d2 INT = (SELECT DoctorId FROM Doctors WHERE Email = 'laura.diaz@clinic.com');
DECLARE @d3 INT = (SELECT DoctorId FROM Doctors WHERE Email = 'pedro.almonte@clinic.com');
DECLARE @d4 INT = (SELECT DoctorId FROM Doctors WHERE Email = 'sofia.reyes@clinic.com');

IF NOT EXISTS (SELECT 1 FROM Appointments)
    INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Reason, Status) VALUES
    (@p1, @d1, DATEADD(DAY, 3, GETDATE()),   'General checkup',        'Scheduled'),
    (@p2, @d3, DATEADD(DAY, 5, GETDATE()),   'Chest pain evaluation',   'Scheduled'),
    (@p3, @d4, DATEADD(DAY, 7, GETDATE()),   'Skin rash',               'Scheduled'),
    (@p4, @d3, DATEADD(DAY, -12, GETDATE()), 'Blood pressure control',  'Completed'),
    (@p5, @d2, DATEADD(DAY, -6, GETDATE()),  'Child vaccination',       'Completed'),
    (@p2, @d1, DATEADD(DAY, -2, GETDATE()),  'Flu symptoms',            'Cancelled');

IF NOT EXISTS (SELECT 1 FROM MedicalHistory)
    INSERT INTO MedicalHistory (PatientId, Allergies, MedicalConditions, PreviousSurgeries, FamilyHistory) VALUES
    (@p1, 'Penicillin', 'None',          'None',          'Diabetes (mother)'),
    (@p2, 'None',       'Hypertension',  'Appendectomy',  'Heart disease (father)'),
    (@p3, 'Dust',       'Asthma',        'None',          'None'),
    (@p4, 'None',       'Hypertension',  'Knee surgery',  'Hypertension (father)');

IF NOT EXISTS (SELECT 1 FROM Consultations)
BEGIN
    DECLARE @c1 INT, @c2 INT, @c3 INT, @c4 INT;

    INSERT INTO Consultations (PatientId, DoctorId, ConsultationDate, Symptoms, Diagnosis, Observations)
    VALUES (@p4, @d3, DATEADD(DAY, -12, GETDATE()), 'Headache and dizziness', 'High blood pressure', 'Reduce salt intake and return in 2 weeks');
    SET @c1 = SCOPE_IDENTITY();

    INSERT INTO Consultations (PatientId, DoctorId, ConsultationDate, Symptoms, Diagnosis, Observations)
    VALUES (@p5, @d2, DATEADD(DAY, -6, GETDATE()), 'Routine visit', 'Healthy', 'Vaccination schedule updated');
    SET @c2 = SCOPE_IDENTITY();

    INSERT INTO Consultations (PatientId, DoctorId, ConsultationDate, Symptoms, Diagnosis, Observations)
    VALUES (@p3, @d4, DATEADD(DAY, -20, GETDATE()), 'Itchy red skin', 'Contact dermatitis', 'Avoid the allergen and use cream');
    SET @c3 = SCOPE_IDENTITY();

    INSERT INTO Consultations (PatientId, DoctorId, ConsultationDate, Symptoms, Diagnosis, Observations)
    VALUES (@p1, @d1, DATEADD(DAY, -30, GETDATE()), 'Fever and cough', 'Seasonal flu', 'Rest and drink fluids');
    SET @c4 = SCOPE_IDENTITY();

    INSERT INTO Treatments (ConsultationId, TreatmentName, Description, Medication, StartDate, EndDate) VALUES
    (@c1, 'Blood pressure control', 'Daily medication and low salt diet', 'Losartan 50mg',
        CAST(DATEADD(DAY, -12, GETDATE()) AS DATE), CAST(DATEADD(DAY, 18, GETDATE()) AS DATE)),
    (@c3, 'Skin treatment', 'Apply cream twice a day', 'Hydrocortisone cream',
        CAST(DATEADD(DAY, -20, GETDATE()) AS DATE), CAST(DATEADD(DAY, -10, GETDATE()) AS DATE)),
    (@c4, 'Flu treatment', 'Rest and medication for fever', 'Acetaminophen 500mg',
        CAST(DATEADD(DAY, -30, GETDATE()) AS DATE), CAST(DATEADD(DAY, -25, GETDATE()) AS DATE));
END
GO