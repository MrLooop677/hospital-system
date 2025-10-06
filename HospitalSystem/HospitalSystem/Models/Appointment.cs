namespace HospitalSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public DateTime Date { get; set; } 

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Status { get; set; } = "Booked"; // Booked / Cancelled / Done

        public Doctor Doctor { get; set; }

        public Patient Patient { get; set; }
    }
}
