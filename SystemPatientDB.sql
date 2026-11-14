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