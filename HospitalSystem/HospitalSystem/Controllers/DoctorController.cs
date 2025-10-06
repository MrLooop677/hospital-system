using HospitalSystem.DataAccess;
using HospitalSystem.Models;
using HospitalSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HospitalSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ILogger<DoctorController> _logger;
        private ApplicationDbContext _db = new();

        public DoctorController(ILogger<DoctorController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var specializations = _db.Specialties;
            ViewBag.Specializations = specializations.AsNoTracking().AsAsyncEnumerable();
            return View();
        }
        [HttpPost]
        public IActionResult CompleteAppointment( string PatientName, DateTime AppointmentDate, TimeSpan AppointmentTime,int DocId)
        {
            Console.WriteLine($"DoctorId: {DocId}, PatientName: {PatientName}, Date: {AppointmentDate}, Time: {AppointmentTime}");

            if (_db == null)
            {
                throw new Exception("Database context is not initialized.");
            }

          
            var patient = _db.Patients.FirstOrDefault(p => p.FullName == PatientName);
            if (patient == null)
            {
                patient = new Patient { FullName = PatientName };
                _db.Patients.Add(patient);
                _db.SaveChanges();
            }
            var doctor = _db.Doctors.FirstOrDefault(d => d.Id == DocId);
            if (doctor == null)
            {
                ModelState.AddModelError("", "Invalid Doctor selected.");
                return View();
            }

            var endTime = AppointmentTime.Add(TimeSpan.FromMinutes(30));

            bool isBooked = _db.Appointments.Any(a =>
               doctor.Id> 0&&
                a.Date == AppointmentDate &&
                (
                    (AppointmentTime >= a.StartTime && AppointmentTime < a.EndTime) ||
                    (endTime > a.StartTime && endTime <= a.EndTime)
                )
            );

            if (isBooked)
            {
                ModelState.AddModelError("", "This time slot is already booked for the selected doctor.");
                return View();
            }
            Console.WriteLine($"doctorId {doctor.Id}");
            var appointment = new Appointment
            {
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                Date = AppointmentDate,
                StartTime = AppointmentTime,
                EndTime = endTime,
                Status = "Booked"
            };

            _db.Appointments.Add(appointment);
            _db.SaveChanges();

            TempData["Success"] = "Appointment booked successfully!";
            return RedirectToAction("BookAppointment");
        }


        [HttpGet]
        public IActionResult CompleteAppointment(string DoctorName,string PatientName,int DocId)
        {
            ViewBag.DoctorName = DoctorName;
            ViewBag.DocId = DocId;
            var validDates = GetValidAppointmentDates();
            ViewBag.ValidDates = validDates;
            ViewBag.StartTime = 9;
            ViewBag.EndTime = 17;


            return View();
        }
        public IActionResult AppointmentDetails(string DoctorName,string PatientName,int DocId)
        {
            var appointments = _db.Appointments
               .Include(a => a.Doctor)
               .Include(a => a.Patient)
               .AsNoTracking()
               .ToList();

            return View(appointments);
        }
        private List<DateTime> GetValidAppointmentDates()
        {
            var validDates = new List<DateTime>();
            var startDate = DateTime.Today;

            // Get next 30 days for appointment availability
            for (int i = 0; i < 30; i++)
            {
                var date = startDate.AddDays(i);
                // Sunday=0, Monday=1, Tuesday=2, Wednesday=3, Thursday=4, Friday=5, Saturday=6
                if (date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday)
                {
                    validDates.Add(date);
                }
            }

            return validDates;
        }
        public IActionResult bookappointment(string searchName,int specialystFilter,int page=1)
        {
            var doctors = _db.Doctors.Join(
                _db.Specialties,
                d => d.SpecialtyId,
                s => s.Id,
                (d,s)=>new DoctorWithSpecialtyViewModel
                { 
                    DocId=d.Id,
                    FullName = d.FullName,
                    PhoneNumber = d.PhoneNumber,
                    Email = d.Email,
                    SpecialtyName = s.Name,
                    specId = s.Id,
                    MainImg=d.MainImg
                }    
                );
            ViewBag.SearchName= searchName;
            if (!string.IsNullOrEmpty(searchName))
            {
                doctors = doctors.Where(d => d.FullName.Contains(searchName));
            }
            ViewBag.Specialties = _db.Specialties.AsEnumerable();
            if (specialystFilter != 0)
                doctors = doctors.Where(d=>d.specId==specialystFilter);
            ViewBag.specialystFilter= specialystFilter;

            ViewBag.totalPage = Math.Ceiling(doctors.Count() / 8.0);
            ViewBag.currentPage = page;
            doctors = doctors.Skip((page - 1) * 8).Take(8);
            return View(doctors.AsNoTracking().AsEnumerable());
        }
      

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
