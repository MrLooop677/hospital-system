using HospitalSystem.DataAccess.EntityConfigurations;
using HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.DataAccess
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDetails> AppointmentDetails { get; set; }
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=PC-46;Initial Catalog=Hospital;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppointmentEntityTypeConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);




        }
    }
}
