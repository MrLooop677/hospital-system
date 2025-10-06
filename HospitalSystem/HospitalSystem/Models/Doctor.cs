namespace HospitalSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string FullName { get; set; }= string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string MainImg { get; set; } = string.Empty;

        public int SpecialtyId { get; set; }

        public string Email { get; set; } = string.Empty;

        public List<Appointment> Appointments { get; set; }
    }
}
