using System;
using Microsoft.Data.SqlClient;


/*Nombre: Jhon Michael
 Matricula: 20251861*/


class Program
{
    static int Main(string[] args)
    {
        try
        {
            DataBase db = new DataBase();
            string opcion = "";

            do
            {
                Console.WriteLine("====================================================");
                Console.WriteLine();
                Console.WriteLine("           PATIENT MANAGEMENT SYSTEM              ");
                Console.WriteLine();
                Console.WriteLine("====================================================");

                Console.WriteLine("1.Patient Management");
                Console.WriteLine("2.Doctor Management");
                Console.WriteLine("3.Appointment Management");
                Console.WriteLine("4.Medical History");
                Console.WriteLine("5.Medical Consultations");
                Console.WriteLine("6.Treatment Management");
                Console.WriteLine("7.Reports");
                Console.WriteLine("8.Exit");
                Console.WriteLine("====================================================");
                Console.WriteLine();

                Console.Write("Select An Option: ");
                opcion = (Console.ReadLine() ?? "").Trim();

                switch (opcion)
                {
                    case "1":
                        db.PatientMenu();
                        break;

                    case "2":
                        db.DoctorMenu();
                        break;

                    case "3":
                        db.AppointmentMenu();
                        break;

                    case "4":
                        db.HistoryMenu();
                        break;

                    case "5":
                        db.ConsultationMenu();
                        break;

                    case "6":
                        db.TreatmentMenu();
                        break;

                    case "7":
                        db.ReportsMenu();
                        break;

                    case "8":
                        Console.WriteLine("====================================================");
                        Console.WriteLine();
                        Console.WriteLine("       The System Has Finished Successfully              ");
                        Console.WriteLine();
                        Console.WriteLine("====================================================");
                        break;

                    default:
                        Console.WriteLine("Invalid Option. Please Try Again.");
                        Console.WriteLine("Select An Option From 1 to 8");
                        Console.WriteLine("Press ENTER to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            } while (opcion != "8");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            Console.WriteLine("Press ENTER to return to the menu...");
            Console.ReadLine();
            Console.Clear();
        }

        return 0;
    }
}


public class Patient
{
    public int PatientId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string IdCard { get; set; } = "";
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Address { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime RegistrationDate { get; set; }

    public Patient(int patientId, string firstName, string lastName,
       string idCard, DateTime dateOfBirth, string gender,
       string phoneNumber, string address, string email)
    {
        PatientId = patientId;
        FirstName = firstName;
        LastName = lastName;
        IdCard = idCard;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        PhoneNumber = phoneNumber;
        Address = address;
        Email = email;
        RegistrationDate = DateTime.Now;
    }
}

public class Doctor
{
    public int DoctorId { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Specialty { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Email { get; set; } = "";
    public string Schedule { get; set; } = "";

    public Doctor(int doctorId, string firstName, string lastName,
       string specialty, string phoneNumber, string email, string schedule)
    {
        DoctorId = doctorId;
        FirstName = firstName;
        LastName = lastName;
        Specialty = specialty;
        PhoneNumber = phoneNumber;
        Email = email;
        Schedule = schedule;
    }
}

public class Appointment
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Reason { get; set; }
    public string Status { get; set; }

    public Appointment(int appointmentId, int patientId, int doctorId,
        DateTime appointmentDate, string reason, string status)
    {
        AppointmentId = appointmentId;
        PatientId = patientId;
        DoctorId = doctorId;
        AppointmentDate = appointmentDate;
        Reason = reason;
        Status = status;
    }
}

public class MedicalHistory
{
    public int HistoryId { get; set; }
    public int PatientId { get; set; }
    public string Allergies { get; set; }
    public string MedicalConditions { get; set; }
    public string PreviousSurgeries { get; set; }
    public string FamilyHistory { get; set; }

    public MedicalHistory(int historyId, int patientId, string allergies,
        string medicalConditions, string previousSurgeries,
        string familyHistory)
    {
        HistoryId = historyId;
        PatientId = patientId;
        Allergies = allergies;
        MedicalConditions = medicalConditions;
        PreviousSurgeries = previousSurgeries;
        FamilyHistory = familyHistory;
    }
}

public class Consultation
{
    public int ConsultationId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime ConsultationDate { get; set; }
    public string Symptoms { get; set; }
    public string Diagnosis { get; set; }
    public string Observations { get; set; }

    public Consultation(int consultationId, int patientId, int doctorId,
        DateTime consultationDate, string symptoms, string diagnosis,
        string observations)
    {
        ConsultationId = consultationId;
        PatientId = patientId;
        DoctorId = doctorId;
        ConsultationDate = consultationDate;
        Symptoms = symptoms;
        Diagnosis = diagnosis;
        Observations = observations;
    }
}

public class Treatment
{
    public int TreatmentId { get; set; }
    public int ConsultationId { get; set; }
    public string TreatmentName { get; set; }
    public string Description { get; set; }
    public string Medication { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Treatment(int treatmentId, int consultationId,
        string treatmentName, string description, string medication,
        DateTime startDate, DateTime endDate)
    {
        TreatmentId = treatmentId;
        ConsultationId = consultationId;
        TreatmentName = treatmentName;
        Description = description;
        Medication = medication;
        StartDate = startDate;
        EndDate = endDate;
    }
}


public class DataBase
{
    string connection = "Server=localhost;Database=PatientSystemDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public string SubMenu(string title, string option1, string option2)
    {
        Console.Clear();
        Console.WriteLine("====================================================");
        Console.WriteLine();
        Console.WriteLine("           " + title);
        Console.WriteLine();
        Console.WriteLine("====================================================");
        Console.WriteLine("1." + option1);
        Console.WriteLine("2." + option2);
        Console.WriteLine("3.Back");
        Console.WriteLine("====================================================");
        Console.Write("Select An Option: ");
        return (Console.ReadLine() ?? "").Trim();
    }

    public void InvalidOption()
    {
        Console.WriteLine("Invalid Option. Select An Option From 1 to 3");
        Pause();
    }

    public void Pause()
    {
        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
    }

    public string ReadText(string message)
    {
        string text = "";
        while (text == "")
        {
            Console.Write(message + ": ");
            text = (Console.ReadLine() ?? "").Trim();
            if (text == "")
            {
                Console.WriteLine("This field cannot be empty.");
            }
        }
        return text;
    }

    public int ReadInt(string message)
    {
        int number;
        Console.Write(message + ": ");
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.Write("Invalid number. Try again: ");
        }
        return number;
    }

    public DateTime ReadDate(string message)
    {
        DateTime date;
        Console.Write(message + " (yyyy-MM-dd HH:mm): ");
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.Write("Invalid date. Try again: ");
        }
        return date;
    }

    public void Save(SqlConnection con, SqlCommand cmd)
    {
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            Console.WriteLine("Saved successfully.");
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
                Console.WriteLine("Error: that ID card is already registered.");
            }
            else if (ex.Number == 547)
            {
                Console.WriteLine("Error: invalid data. Check that the IDs exist.");
            }
            else
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
        }
        finally
        {
            con.Close();
        }
        Pause();
    }

    public void List(string sql)
    {
        SqlConnection con = new SqlConnection(connection);
        SqlCommand cmd = new SqlCommand(sql, con);

        try
        {
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("No records found.");
            }

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + " | ");
                }
                Console.WriteLine();
            }
            reader.Close();
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Database error: " + ex.Message);
        }
        finally
        {
            con.Close();
        }
        Pause();
    }

    public void PatientMenu()
    {
        string op = "";
        do
        {
            op = SubMenu("PATIENT MANAGEMENT", "Add Patient", "List Patients");
            if (op == "1") AddPatient();
            else if (op == "2") List("SELECT * FROM Patients");
            else if (op != "3") InvalidOption();
        } while (op != "3");
    }

    public void AddPatient()
    {
        Patient p = new Patient(0, ReadText("First Name"), ReadText("Last Name"), ReadText("ID Card"),
            ReadDate("Date Of Birth"), ReadText("Gender (M/F)").ToUpper(), ReadText("Phone Number"),
            ReadText("Address"), ReadText("Email"));

        if (p.DateOfBirth > DateTime.Now)
        {
            Console.WriteLine("The date of birth cannot be in the future.");
            Pause();
            return;
        }

        SqlConnection con = new SqlConnection(connection);
        SqlCommand cmd = new SqlCommand("INSERT INTO Patients (FirstName, LastName, IdCard, DateOfBirth, Gender, PhoneNumber, Address, Email) VALUES (@fn, @ln, @id, @dob, @g, @ph, @ad, @em)", con);
        cmd.Parameters.AddWithValue("@fn", p.FirstName);
        cmd.Parameters.AddWithValue("@ln", p.LastName);
        cmd.Parameters.AddWithValue("@id", p.IdCard);
        cmd.Parameters.AddWithValue("@dob", p.DateOfBirth);
        cmd.Parameters.AddWithValue("@g", p.Gender);
        cmd.Parameters.AddWithValue("@ph", p.PhoneNumber);
        cmd.Parameters.AddWithValue("@ad", p.Address);
        cmd.Parameters.AddWithValue("@em", p.Email);
        Save(con, cmd);
    }

    public void DoctorMenu()
    {
        string op = "";
        do
        {
            op = SubMenu("DOCTOR MANAGEMENT", "Add Doctor", "List Doctors");
            if (op == "1") AddDoctor();
            else if (op == "2") List("SELECT * FROM Doctors");
            else if (op != "3") InvalidOption();
        } while (op != "3");
    }

    public void AddDoctor()
    {
        Doctor d = new Doctor(0, ReadText("First Name"), ReadText("Last Name"), ReadText("Specialty"),
            ReadText("Phone Number"), ReadText("Email"), ReadText("Schedule"));

        SqlConnection con = new SqlConnection(connection);
        SqlCommand cmd = new SqlCommand("INSERT INTO Doctors (FirstName, LastName, Specialty, PhoneNumber, Email, Schedule) VALUES (@fn, @ln, @sp, @ph, @em, @sc)", con);
        cmd.Parameters.AddWithValue("@fn", d.FirstName);
        cmd.Parameters.AddWithValue("@ln", d.LastName);
        cmd.Parameters.AddWithValue("@sp", d.Specialty);
        cmd.Parameters.AddWithValue("@ph", d.PhoneNumber);
        cmd.Parameters.AddWithValue("@em", d.Email);
        cmd.Parameters.AddWithValue("@sc", d.Schedule);
        Save(con, cmd);
    }

    public void AppointmentMenu()
    {
        string op = "";
        do
        {
            op = SubMenu("APPOINTMENT MANAGEMENT", "Add Appointment", "List Appointments");
            if (op == "1") AddAppointment();
            else if (op == "2") List("SELECT * FROM Appointments");
            else if (op != "3") InvalidOption();
        } while (op != "3");
    }

    public void AddAppointment()
    {
        Appointment a = new Appointment(0, ReadInt("Patient Id"), ReadInt("Doctor Id"),
            ReadDate("Appointment Date"), ReadText("Reason"), "Scheduled");

        if (a.AppointmentDate < DateTime.Now)
        {
            Console.WriteLine("The appointment date must be in the future.");
            Pause();
            return;
        }

        SqlConnection con = new SqlConnection(connection);
        SqlCommand cmd = new SqlCommand("INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Reason, Status) VALUES (@p, @d, @dt, @r, @s)", con);
        cmd.Parameters.AddWithValue("@p", a.PatientId);
        cmd.Parameters.AddWithValue("@d", a.DoctorId);
        cmd.Parameters.AddWithValue("@dt", a.AppointmentDate);
        cmd.Parameters.AddWithValue("@r", a.Reason);
        cmd.Parameters.AddWithValue("@s", a.Status);
        Save(con, cmd);
    }

    public void HistoryMenu()
    {
        string op = "";
        do
        {
            op = SubMenu("MEDICAL HISTORY", "Add Medical History", "List Medical Histories");
            if (op == "1") AddHistory();
            else if (op == "2") List("SELECT * FROM MedicalHistory");
            else if (op != "3") InvalidOption();
        } while (op != "3");
    }

    public void AddHistory()
    {
        MedicalHistory h = new MedicalHistory(0, ReadInt("Patient Id"), ReadText("Allergies"),
            ReadText("Medical Conditions"), ReadText("Previous Surgeries"), ReadText("Family History"));

        SqlConnection con = new SqlConnection(connection);
        SqlCommand cmd = new SqlCommand("INSERT INTO MedicalHistory (PatientId, Allergies, MedicalConditions, PreviousSurgeries, FamilyHistory) VALUES (@p, @a, @m, @s, @f)", con);
        cmd.Parameters.AddWithValue("@p", h.PatientId);
        cmd.Parameters.AddWithValue("@a", h.Allergies);
        cmd.Parameters.AddWithValue("@m", h.MedicalConditions);
        cmd.Parameters.AddWithValue("@s", h.PreviousSurgeries);
        cmd.Parameters.AddWithValue("@f", h.FamilyHistory);
        Save(con, cmd);
    }

    public void ConsultationMenu()
    {
        string op = "";
        do
        {
            op = SubMenu("MEDICAL CONSULTATIONS", "Add Consultation", "List Consultations");
            if (op == "1") AddConsultation();
            else if (op == "2") List("SELECT * FROM Consultations");
            else if (op != "3") InvalidOption();
        } while (op != "3");
    }

    public void AddConsultation()
    {
        Consultation c = new Consultation(0, ReadInt("Patient Id"), ReadInt("Doctor Id"),
            ReadDate("Consultation Date"), ReadText("Symptoms"), ReadText("Diagnosis"), ReadText("Observations"));

        SqlConnection con = new SqlConnection(connection);
        SqlCommand cmd = new SqlCommand("INSERT INTO Consultations (PatientId, DoctorId, ConsultationDate, Symptoms, Diagnosis, Observations) VALUES (@p, @d, @dt, @s, @di, @o)", con);
        cmd.Parameters.AddWithValue("@p", c.PatientId);
        cmd.Parameters.AddWithValue("@d", c.DoctorId);
        cmd.Parameters.AddWithValue("@dt", c.ConsultationDate);
        cmd.Parameters.AddWithValue("@s", c.Symptoms);
        cmd.Parameters.AddWithValue("@di", c.Diagnosis);
        cmd.Parameters.AddWithValue("@o", c.Observations);
        Save(con, cmd);
    }

    public void TreatmentMenu()
    {
        string op = "";
        do
        {
            op = SubMenu("TREATMENT MANAGEMENT", "Add Treatment", "List Treatments");
            if (op == "1") AddTreatment();
            else if (op == "2") List("SELECT * FROM Treatments");
            else if (op != "3") InvalidOption();
        } while (op != "3");
    }

    public void AddTreatment()
    {
        Treatment t = new Treatment(0, ReadInt("Consultation Id"), ReadText("Treatment Name"),
            ReadText("Description"), ReadText("Medication"), ReadDate("Start Date"), ReadDate("End Date"));

        if (t.EndDate < t.StartDate)
        {
            Console.WriteLine("The end date cannot be before the start date.");
            Pause();
            return;
        }

        SqlConnection con = new SqlConnection(connection);
        SqlCommand cmd = new SqlCommand("INSERT INTO Treatments (ConsultationId, TreatmentName, Description, Medication, StartDate, EndDate) VALUES (@c, @n, @d, @m, @sd, @ed)", con);
        cmd.Parameters.AddWithValue("@c", t.ConsultationId);
        cmd.Parameters.AddWithValue("@n", t.TreatmentName);
        cmd.Parameters.AddWithValue("@d", t.Description);
        cmd.Parameters.AddWithValue("@m", t.Medication);
        cmd.Parameters.AddWithValue("@sd", t.StartDate.Date);
        cmd.Parameters.AddWithValue("@ed", t.EndDate.Date);
        Save(con, cmd);
    }

    public void ReportsMenu()
    {
        string op = "";
        do
        {
            op = SubMenu("REPORTS", "Appointments Per Doctor", "Consultations Per Patient");
            if (op == "1")
            {
                List("SELECT d.FirstName + ' ' + d.LastName AS Doctor, COUNT(a.AppointmentId) AS TotalAppointments FROM Doctors d LEFT JOIN Appointments a ON a.DoctorId = d.DoctorId GROUP BY d.DoctorId, d.FirstName, d.LastName");
            }
            else if (op == "2")
            {
                List("SELECT p.FirstName + ' ' + p.LastName AS Patient, COUNT(c.ConsultationId) AS TotalConsultations FROM Patients p LEFT JOIN Consultations c ON c.PatientId = p.PatientId GROUP BY p.PatientId, p.FirstName, p.LastName");
            }
            else if (op != "3") InvalidOption();
        } while (op != "3");
    }
}