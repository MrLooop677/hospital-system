namespace HospitalSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

     

        public List<Appointment> Appointments { get; set; }
    }
}
