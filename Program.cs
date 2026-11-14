using System;
using Microsoft.Data.SqlClient;


/*Nombre: Jhon Michael
 Matricula: 20251861*/


class Program
{
    static string connStr = "Server=localhost;Database=PatientSystemDB;Trusted_Connection=True;TrustServerCertificate=True;";

    static int Main(string[] args)
    {
        try
        {
            string opcion = "";

            do
            {
                Console.Clear();
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
                    {
                        string Opcion = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("====================================================");
                            Console.WriteLine();
                            Console.WriteLine("           PATIENT MANAGEMENT               ");
                            Console.WriteLine();
                            Console.WriteLine("====================================================");
                            Console.WriteLine("1.Add Patient");
                            Console.WriteLine("2.List Patients");
                            Console.WriteLine("3.Modify Patient");
                            Console.WriteLine("4.Delete Patient");
                            Console.WriteLine("5.Back");
                            Console.Write("Select An Option: ");
                            Opcion = (Console.ReadLine() ?? "").Trim();

                            switch (Opcion)
                            {
                                case "1":
                                    Patient p = new Patient(0, ReadText("First Name"), ReadText("Last Name"),
                                        ReadText("ID Card"), ReadDate("Date Of Birth"), ReadGender(),
                                        ReadText("Phone Number"), ReadText("Address"), ReadText("Email"));

                                    if (p.DateOfBirth > DateTime.Now)
                                    {
                                        Console.WriteLine("The date of birth cannot be in the future.");
                                        Console.WriteLine("Press ENTER to continue...");
                                        Console.ReadLine();
                                        break;
                                    }

                                    SqlCommand cmd = new SqlCommand("INSERT INTO Patients (FirstName, LastName, IdCard, DateOfBirth, Gender, PhoneNumber, Address, Email) VALUES (@fn, @ln, @id, @dob, @g, @ph, @ad, @em)", new SqlConnection(connStr));
                                    cmd.Parameters.AddWithValue("@fn", p.FirstName);
                                    cmd.Parameters.AddWithValue("@ln", p.LastName);
                                    cmd.Parameters.AddWithValue("@id", p.IdCard);
                                    cmd.Parameters.AddWithValue("@dob", p.DateOfBirth);
                                    cmd.Parameters.AddWithValue("@g", p.Gender);
                                    cmd.Parameters.AddWithValue("@ph", p.PhoneNumber);
                                    cmd.Parameters.AddWithValue("@ad", p.Address);
                                    cmd.Parameters.AddWithValue("@em", p.Email);
                                    SaveData(cmd);
                                    break;

                                case "2":
                                    ShowData("SELECT * FROM Patients");
                                    break;

                                case "3":
                                    int idEdit = ReadInt("Patient Id to modify");
                                    string newPhone = ReadText("New Phone Number");
                                    string newAddress = ReadText("New Address");
                                    string newEmail = ReadText("New Email");

                                    SqlCommand cmdEdit = new SqlCommand("UPDATE Patients SET PhoneNumber = @ph, Address = @ad, Email = @em WHERE PatientId = @id", new SqlConnection(connStr));
                                    cmdEdit.Parameters.AddWithValue("@ph", newPhone);
                                    cmdEdit.Parameters.AddWithValue("@ad", newAddress);
                                    cmdEdit.Parameters.AddWithValue("@em", newEmail);
                                    cmdEdit.Parameters.AddWithValue("@id", idEdit);
                                    SaveData(cmdEdit);
                                    break;

                                case "4":
                                    int idDelete = ReadInt("Patient Id to delete");
                                    if (ConfirmDelete())
                                    {
                                        SqlCommand cmdDelete = new SqlCommand("DELETE FROM Patients WHERE PatientId = @id", new SqlConnection(connStr));
                                        cmdDelete.Parameters.AddWithValue("@id", idDelete);
                                        SaveData(cmdDelete);
                                    }
                                    break;

                                case "5":
                                    break;

                                default:
                                    Console.WriteLine("Invalid Option. Select An Option From 1 to 5");
                                    Console.WriteLine("Press ENTER to continue...");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (Opcion != "5");
                        break;
                    }

                    case "2":
                    {
                        string Opcion = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("====================================================");
                            Console.WriteLine();
                            Console.WriteLine("                Doctor Management              ");
                            Console.WriteLine();
                            Console.WriteLine("====================================================");
                            Console.WriteLine("1.Add Doctor");
                            Console.WriteLine("2.List Doctors");
                            Console.WriteLine("3.Modify Doctor");
                            Console.WriteLine("4.Delete Doctor");
                            Console.WriteLine("5.Back");
                            Console.Write("Select An Option: ");
                            Opcion = (Console.ReadLine() ?? "").Trim();

                            switch (Opcion)
                            {
                                case "1":
                                    Doctor d = new Doctor(0, ReadText("First Name"), ReadText("Last Name"),
                                        ReadText("Specialty"), ReadText("Phone Number"), ReadText("Email"),
                                        ReadText("Schedule"));

                                    SqlCommand cmd = new SqlCommand("INSERT INTO Doctors (FirstName, LastName, Specialty, PhoneNumber, Email, Schedule) VALUES (@fn, @ln, @sp, @ph, @em, @sc)", new SqlConnection(connStr));
                                    cmd.Parameters.AddWithValue("@fn", d.FirstName);
                                    cmd.Parameters.AddWithValue("@ln", d.LastName);
                                    cmd.Parameters.AddWithValue("@sp", d.Specialty);
                                    cmd.Parameters.AddWithValue("@ph", d.PhoneNumber);
                                    cmd.Parameters.AddWithValue("@em", d.Email);
                                    cmd.Parameters.AddWithValue("@sc", d.Schedule);
                                    SaveData(cmd);
                                    break;

                                case "2":
                                    ShowData("SELECT * FROM Doctors");
                                    break;

                                case "3":
                                    int idEdit = ReadInt("Doctor Id to modify");
                                    string newPhone = ReadText("New Phone Number");
                                    string newEmail = ReadText("New Email");
                                    string newSchedule = ReadText("New Schedule");

                                    SqlCommand cmdEdit = new SqlCommand("UPDATE Doctors SET PhoneNumber = @ph, Email = @em, Schedule = @sc WHERE DoctorId = @id", new SqlConnection(connStr));
                                    cmdEdit.Parameters.AddWithValue("@ph", newPhone);
                                    cmdEdit.Parameters.AddWithValue("@em", newEmail);
                                    cmdEdit.Parameters.AddWithValue("@sc", newSchedule);
                                    cmdEdit.Parameters.AddWithValue("@id", idEdit);
                                    SaveData(cmdEdit);
                                    break;

                                case "4":
                                    int idDelete = ReadInt("Doctor Id to delete");
                                    if (ConfirmDelete())
                                    {
                                        SqlCommand cmdDelete = new SqlCommand("DELETE FROM Doctors WHERE DoctorId = @id", new SqlConnection(connStr));
                                        cmdDelete.Parameters.AddWithValue("@id", idDelete);
                                        SaveData(cmdDelete);
                                    }
                                    break;

                                case "5":
                                    break;

                                default:
                                    Console.WriteLine("Invalid Option. Select An Option From 1 to 5");
                                    Console.WriteLine("Press ENTER to continue...");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (Opcion != "5");
                        break;
                    }

                    case "3":
                    {
                        string Opcion = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("====================================================");
                            Console.WriteLine();
                            Console.WriteLine("              Appointment Management              ");
                            Console.WriteLine();
                            Console.WriteLine("====================================================");
                            Console.WriteLine("1.Add Appointment");
                            Console.WriteLine("2.List Appointments");
                            Console.WriteLine("3.Modify Appointment");
                            Console.WriteLine("4.Delete Appointment");
                            Console.WriteLine("5.Back");
                            Console.Write("Select An Option: ");
                            Opcion = (Console.ReadLine() ?? "").Trim();

                            switch (Opcion)
                            {
                                case "1":
                                    Appointment a = new Appointment(0, ReadInt("Patient Id"), ReadInt("Doctor Id"),
                                        ReadDate("Appointment Date"), ReadText("Reason"), "Scheduled");

                                    if (a.AppointmentDate < DateTime.Now)
                                    {
                                        Console.WriteLine("The appointment date must be in the future.");
                                        Console.WriteLine("Press ENTER to continue...");
                                        Console.ReadLine();
                                        break;
                                    }

                                    SqlCommand cmd = new SqlCommand("INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Reason, Status) VALUES (@p, @d, @dt, @r, @s)", new SqlConnection(connStr));
                                    cmd.Parameters.AddWithValue("@p", a.PatientId);
                                    cmd.Parameters.AddWithValue("@d", a.DoctorId);
                                    cmd.Parameters.AddWithValue("@dt", a.AppointmentDate);
                                    cmd.Parameters.AddWithValue("@r", a.Reason);
                                    cmd.Parameters.AddWithValue("@s", a.Status);
                                    SaveData(cmd);
                                    break;

                                case "2":
                                    ShowData("SELECT * FROM Appointments");
                                    break;

                                case "3":
                                    int idEdit = ReadInt("Appointment Id to modify");
                                    string newReason = ReadText("New Reason");
                                    string newStatus = ReadStatus();

                                    SqlCommand cmdEdit = new SqlCommand("UPDATE Appointments SET Reason = @r, Status = @s WHERE AppointmentId = @id", new SqlConnection(connStr));
                                    cmdEdit.Parameters.AddWithValue("@r", newReason);
                                    cmdEdit.Parameters.AddWithValue("@s", newStatus);
                                    cmdEdit.Parameters.AddWithValue("@id", idEdit);
                                    SaveData(cmdEdit);
                                    break;

                                case "4":
                                    int idDelete = ReadInt("Appointment Id to delete");
                                    if (ConfirmDelete())
                                    {
                                        SqlCommand cmdDelete = new SqlCommand("DELETE FROM Appointments WHERE AppointmentId = @id", new SqlConnection(connStr));
                                        cmdDelete.Parameters.AddWithValue("@id", idDelete);
                                        SaveData(cmdDelete);
                                    }
                                    break;

                                case "5":
                                    break;

                                default:
                                    Console.WriteLine("Invalid Option. Select An Option From 1 to 5");
                                    Console.WriteLine("Press ENTER to continue...");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (Opcion != "5");
                        break;
                    }

                    case "4":
                    {
                        string Opcion = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("====================================================");
                            Console.WriteLine();
                            Console.WriteLine("                Medical History              ");
                            Console.WriteLine();
                            Console.WriteLine("====================================================");
                            Console.WriteLine("1.Add Medical History");
                            Console.WriteLine("2.List Medical Histories");
                            Console.WriteLine("3.Modify Medical History");
                            Console.WriteLine("4.Delete Medical History");
                            Console.WriteLine("5.Back");
                            Console.Write("Select An Option: ");
                            Opcion = (Console.ReadLine() ?? "").Trim();

                            switch (Opcion)
                            {
                                case "1":
                                    MedicalHistory h = new MedicalHistory(0, ReadInt("Patient Id"), ReadText("Allergies"),
                                        ReadText("Medical Conditions"), ReadText("Previous Surgeries"),
                                        ReadText("Family History"));

                                    SqlCommand cmd = new SqlCommand("INSERT INTO MedicalHistory (PatientId, Allergies, MedicalConditions, PreviousSurgeries, FamilyHistory) VALUES (@p, @a, @m, @s, @f)", new SqlConnection(connStr));
                                    cmd.Parameters.AddWithValue("@p", h.PatientId);
                                    cmd.Parameters.AddWithValue("@a", h.Allergies);
                                    cmd.Parameters.AddWithValue("@m", h.MedicalConditions);
                                    cmd.Parameters.AddWithValue("@s", h.PreviousSurgeries);
                                    cmd.Parameters.AddWithValue("@f", h.FamilyHistory);
                                    SaveData(cmd);
                                    break;

                                case "2":
                                    ShowData("SELECT * FROM MedicalHistory");
                                    break;

                                case "3":
                                    int idEdit = ReadInt("History Id to modify");
                                    string newAllergies = ReadText("New Allergies");
                                    string newConditions = ReadText("New Medical Conditions");

                                    SqlCommand cmdEdit = new SqlCommand("UPDATE MedicalHistory SET Allergies = @a, MedicalConditions = @m WHERE HistoryId = @id", new SqlConnection(connStr));
                                    cmdEdit.Parameters.AddWithValue("@a", newAllergies);
                                    cmdEdit.Parameters.AddWithValue("@m", newConditions);
                                    cmdEdit.Parameters.AddWithValue("@id", idEdit);
                                    SaveData(cmdEdit);
                                    break;

                                case "4":
                                    int idDelete = ReadInt("History Id to delete");
                                    if (ConfirmDelete())
                                    {
                                        SqlCommand cmdDelete = new SqlCommand("DELETE FROM MedicalHistory WHERE HistoryId = @id", new SqlConnection(connStr));
                                        cmdDelete.Parameters.AddWithValue("@id", idDelete);
                                        SaveData(cmdDelete);
                                    }
                                    break;

                                case "5":
                                    break;

                                default:
                                    Console.WriteLine("Invalid Option. Select An Option From 1 to 5");
                                    Console.WriteLine("Press ENTER to continue...");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (Opcion != "5");
                        break;
                    }

                    case "5":
                    {
                        string Opcion = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("====================================================");
                            Console.WriteLine();
                            Console.WriteLine("                Medical Consultations              ");
                            Console.WriteLine();
                            Console.WriteLine("====================================================");
                            Console.WriteLine("1.Add Consultation");
                            Console.WriteLine("2.List Consultations");
                            Console.WriteLine("3.Modify Consultation");
                            Console.WriteLine("4.Delete Consultation");
                            Console.WriteLine("5.Back");
                            Console.Write("Select An Option: ");
                            Opcion = (Console.ReadLine() ?? "").Trim();

                            switch (Opcion)
                            {
                                case "1":
                                    Consultation c = new Consultation(0, ReadInt("Patient Id"), ReadInt("Doctor Id"),
                                        ReadDate("Consultation Date"), ReadText("Symptoms"), ReadText("Diagnosis"),
                                        ReadText("Observations"));

                                    SqlCommand cmd = new SqlCommand("INSERT INTO Consultations (PatientId, DoctorId, ConsultationDate, Symptoms, Diagnosis, Observations) VALUES (@p, @d, @dt, @s, @di, @o)", new SqlConnection(connStr));
                                    cmd.Parameters.AddWithValue("@p", c.PatientId);
                                    cmd.Parameters.AddWithValue("@d", c.DoctorId);
                                    cmd.Parameters.AddWithValue("@dt", c.ConsultationDate);
                                    cmd.Parameters.AddWithValue("@s", c.Symptoms);
                                    cmd.Parameters.AddWithValue("@di", c.Diagnosis);
                                    cmd.Parameters.AddWithValue("@o", c.Observations);
                                    SaveData(cmd);
                                    break;

                                case "2":
                                    ShowData("SELECT * FROM Consultations");
                                    break;

                                case "3":
                                    int idEdit = ReadInt("Consultation Id to modify");
                                    string newDiagnosis = ReadText("New Diagnosis");
                                    string newObservations = ReadText("New Observations");

                                    SqlCommand cmdEdit = new SqlCommand("UPDATE Consultations SET Diagnosis = @di, Observations = @o WHERE ConsultationId = @id", new SqlConnection(connStr));
                                    cmdEdit.Parameters.AddWithValue("@di", newDiagnosis);
                                    cmdEdit.Parameters.AddWithValue("@o", newObservations);
                                    cmdEdit.Parameters.AddWithValue("@id", idEdit);
                                    SaveData(cmdEdit);
                                    break;

                                case "4":
                                    int idDelete = ReadInt("Consultation Id to delete");
                                    if (ConfirmDelete())
                                    {
                                        SqlCommand cmdDelete = new SqlCommand("DELETE FROM Consultations WHERE ConsultationId = @id", new SqlConnection(connStr));
                                        cmdDelete.Parameters.AddWithValue("@id", idDelete);
                                        SaveData(cmdDelete);
                                    }
                                    break;

                                case "5":
                                    break;

                                default:
                                    Console.WriteLine("Invalid Option. Select An Option From 1 to 5");
                                    Console.WriteLine("Press ENTER to continue...");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (Opcion != "5");
                        break;
                    }

                    case "6":
                    {
                        string Opcion = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("====================================================");
                            Console.WriteLine();
                            Console.WriteLine("               Treatment Management              ");
                            Console.WriteLine();
                            Console.WriteLine("====================================================");
                            Console.WriteLine("1.Add Treatment");
                            Console.WriteLine("2.List Treatments");
                            Console.WriteLine("3.Modify Treatment");
                            Console.WriteLine("4.Delete Treatment");
                            Console.WriteLine("5.Back");
                            Console.Write("Select An Option: ");
                            Opcion = (Console.ReadLine() ?? "").Trim();

                            switch (Opcion)
                            {
                                case "1":
                                    Treatment t = new Treatment(0, ReadInt("Consultation Id"), ReadText("Treatment Name"),
                                        ReadText("Description"), ReadText("Medication"), ReadDate("Start Date"),
                                        ReadDate("End Date"));

                                    if (t.EndDate < t.StartDate)
                                    {
                                        Console.WriteLine("The end date cannot be before the start date.");
                                        Console.WriteLine("Press ENTER to continue...");
                                        Console.ReadLine();
                                        break;
                                    }

                                    SqlCommand cmd = new SqlCommand("INSERT INTO Treatments (ConsultationId, TreatmentName, Description, Medication, StartDate, EndDate) VALUES (@c, @n, @d, @m, @sd, @ed)", new SqlConnection(connStr));
                                    cmd.Parameters.AddWithValue("@c", t.ConsultationId);
                                    cmd.Parameters.AddWithValue("@n", t.TreatmentName);
                                    cmd.Parameters.AddWithValue("@d", t.Description);
                                    cmd.Parameters.AddWithValue("@m", t.Medication);
                                    cmd.Parameters.AddWithValue("@sd", t.StartDate.Date);
                                    cmd.Parameters.AddWithValue("@ed", t.EndDate.Date);
                                    SaveData(cmd);
                                    break;

                                case "2":
                                    ShowData("SELECT * FROM Treatments");
                                    break;

                                case "3":
                                    int idEdit = ReadInt("Treatment Id to modify");
                                    string newMedication = ReadText("New Medication");
                                    DateTime newEndDate = ReadDate("New End Date");

                                    SqlCommand cmdEdit = new SqlCommand("UPDATE Treatments SET Medication = @m, EndDate = @ed WHERE TreatmentId = @id AND StartDate <= @ed", new SqlConnection(connStr));
                                    cmdEdit.Parameters.AddWithValue("@m", newMedication);
                                    cmdEdit.Parameters.AddWithValue("@ed", newEndDate.Date);
                                    cmdEdit.Parameters.AddWithValue("@id", idEdit);
                                    SaveData(cmdEdit);
                                    break;

                                case "4":
                                    int idDelete = ReadInt("Treatment Id to delete");
                                    if (ConfirmDelete())
                                    {
                                        SqlCommand cmdDelete = new SqlCommand("DELETE FROM Treatments WHERE TreatmentId = @id", new SqlConnection(connStr));
                                        cmdDelete.Parameters.AddWithValue("@id", idDelete);
                                        SaveData(cmdDelete);
                                    }
                                    break;

                                case "5":
                                    break;

                                default:
                                    Console.WriteLine("Invalid Option. Select An Option From 1 to 5");
                                    Console.WriteLine("Press ENTER to continue...");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (Opcion != "5");
                        break;
                    }

                    case "7":
                    {
                        string Opcion = "";
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("====================================================");
                            Console.WriteLine();
                            Console.WriteLine("                    Reports              ");
                            Console.WriteLine();
                            Console.WriteLine("====================================================");
                            Console.WriteLine("1.Appointments Per Doctor");
                            Console.WriteLine("2.Consultations Per Patient");
                            Console.WriteLine("3.Back");
                            Console.Write("Select An Option: ");
                            Opcion = (Console.ReadLine() ?? "").Trim();

                            switch (Opcion)
                            {
                                case "1":
                                    ShowData("SELECT d.FirstName + ' ' + d.LastName AS Doctor, COUNT(a.AppointmentId) AS TotalAppointments FROM Doctors d LEFT JOIN Appointments a ON a.DoctorId = d.DoctorId GROUP BY d.DoctorId, d.FirstName, d.LastName");
                                    break;

                                case "2":
                                    ShowData("SELECT p.FirstName + ' ' + p.LastName AS Patient, COUNT(c.ConsultationId) AS TotalConsultations FROM Patients p LEFT JOIN Consultations c ON c.PatientId = p.PatientId GROUP BY p.PatientId, p.FirstName, p.LastName");
                                    break;

                                case "3":
                                    break;

                                default:
                                    Console.WriteLine("Invalid Option. Select An Option From 1 to 3");
                                    Console.WriteLine("Press ENTER to continue...");
                                    Console.ReadLine();
                                    break;
                            }
                        } while (Opcion != "3");
                        break;
                    }

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

    static string ReadText(string message)
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

    static int ReadInt(string message)
    {
        int number;
        Console.Write(message + ": ");
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.Write("Invalid number. Try again: ");
        }
        return number;
    }

    static DateTime ReadDate(string message)
    {
        DateTime date;
        Console.Write(message + " (yyyy-MM-dd HH:mm): ");
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.Write("Invalid date. Try again: ");
        }
        return date;
    }

    static string ReadGender()
    {
        string gender = "";
        while (gender != "M" && gender != "F")
        {
            gender = ReadText("Gender (M/F)").ToUpper();
            if (gender != "M" && gender != "F")
            {
                Console.WriteLine("Invalid gender. Use M or F.");
            }
        }
        return gender;
    }

    static string ReadStatus()
    {
        string status = "";
        bool valid = false;
        while (!valid)
        {
            status = ReadText("Status (Scheduled/Completed/Cancelled)");
            status = char.ToUpper(status[0]) + status.Substring(1).ToLower();
            valid = status == "Scheduled" || status == "Completed" || status == "Cancelled";
            if (!valid)
            {
                Console.WriteLine("Invalid status.");
            }
        }
        return status;
    }

    static bool ConfirmDelete()
    {
        string answer = ReadText("Are you sure you want to delete this record? (Y/N)").ToUpper();
        return answer == "Y";
    }

    static int SaveData(SqlCommand cmd)
    {
        try
        {
            cmd.Connection.Open();
            int rows = cmd.ExecuteNonQuery();

            if (rows > 0)
            {
                Console.WriteLine("Operation completed successfully.");
            }
            else
            {
                Console.WriteLine("No record found with that Id (or the data is not valid).");
            }
        }
        catch (SqlException ex)
        {
            if (ex.Number == 2627)
            {
                Console.WriteLine("Error: that ID card is already registered.");
            }
            else if (ex.Number == 547)
            {
                Console.WriteLine("Error: the operation could not be completed. Check the Ids, or the record is related to other records.");
            }
            else
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
        }
        finally
        {
            cmd.Connection.Close();
        }

        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
        return 0;
    }

    static int ShowData(string sql)
    {
        SqlConnection con = new SqlConnection(connStr);
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

        Console.WriteLine("Press ENTER to continue...");
        Console.ReadLine();
        return 0;
    }
}


/*Clases of PATIENTS SYSTEM*/


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
    public string Reason { get; set; } = "";
    public string Status { get; set; } = "";

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
    public string Allergies { get; set; } = "";
    public string MedicalConditions { get; set; } = "";
    public string PreviousSurgeries { get; set; } = "";
    public string FamilyHistory { get; set; } = "";

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
    public string Symptoms { get; set; } = "";
    public string Diagnosis { get; set; } = "";
    public string Observations { get; set; } = "";

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
    public string TreatmentName { get; set; } = "";
    public string Description { get; set; } = "";
    public string Medication { get; set; } = "";
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