using HospitalSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalSystem.DataAccess.EntityConfigurations
{
    public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            //builder.HasKey(app => new { app.DoctorId,app.PatientId });
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .ValueGeneratedOnAdd(); 

            builder.HasIndex(a => new { a.DoctorId, a.PatientId, a.Date, a.StartTime })
                   .IsUnique();

        }
    }
}
