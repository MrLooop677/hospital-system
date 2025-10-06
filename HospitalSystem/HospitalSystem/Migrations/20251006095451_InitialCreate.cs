using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "AppointmentDetails",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AppointmentId = table.Column<int>(type: "int", nullable: false),
            //        DoctorId = table.Column<int>(type: "int", nullable: false),
            //        DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DoctorSpecialty = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PatientId = table.Column<int>(type: "int", nullable: false),
            //        PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PatientPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Date = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
            //        EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
            //        Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AppointmentDetails", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Patients",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Patients", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Specialties",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Specialties", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Doctors",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        MainImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SpecialtyId = table.Column<int>(type: "int", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Doctors", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Doctors_Specialties_SpecialtyId",
            //            column: x => x.SpecialtyId,
            //            principalTable: "Specialties",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId_PatientId_Date_StartTime",
                table: "Appointments",
                columns: new[] { "DoctorId", "PatientId", "Date", "StartTime" },
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Appointments_PatientId",
            //    table: "Appointments",
            //    column: "PatientId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Doctors_SpecialtyId",
            //    table: "Doctors",
            //    column: "SpecialtyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentDetails");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
