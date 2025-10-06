namespace HospitalSystem.Models
{
    public class AppointmentDetails
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string DoctorSpecialty { get; set; }
         
        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public string PatientPhone { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
