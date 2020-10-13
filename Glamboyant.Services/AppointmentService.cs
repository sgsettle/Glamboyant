using Glamboyant.Data;
using Glamboyant.Models.AppointmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Services
{
    public class AppointmentService
    {
        private readonly string _userID;

        public AppointmentService(string userID)
        {
            _userID = userID;
        }

        public bool CreateAppointmentService(AppointmentCreate model)
        {
            var entity =
                new Appointment()
                {
                    AppointmentDate = model.AppointmentDate,
                    AppointmentTime = model.AppointmentTime,
                    Address = model.Address,
                    HairServiceID = model.HairServiceID,
                    UserID = _userID
                };

            using (var apt = new ApplicationDbContext())
            {
                apt.Appointments.Add(entity);
                return apt.SaveChanges() == 1;
            }
        }

        public IEnumerable<AppointmentListItem> GetAppointments()
        {
            using(var apt = new ApplicationDbContext())
            {
                var query =
                    apt
                        .Appointments
                        .Where(e => e.UserID == _userID)
                        .Select(
                            e =>
                                new AppointmentListItem
                                {
                                    UserID = e.UserID,
                                    AppointmentID = e.AppointmentID,
                                    AppointmentDate = e.AppointmentDate,
                                    AppointmentTime = e.AppointmentTime,
                                    Address = e.Address,
                                    HairServiceID = e.HairServiceID,
                                    Name = e.HairService.Name
                                }
                        );

                return query.ToArray();
            }
        }

        public AppointmentDetail GetAppointmentByID(int id)
        {
            using(var apt = new ApplicationDbContext())
            {
                var entity =
                    apt
                        .Appointments
                        .Single(e => e.AppointmentID == id && e.UserID == _userID);
                return
                    new AppointmentDetail
                    {
                        AppointmentID = entity.AppointmentID,
                        AppointmentDate = entity.AppointmentDate,
                        AppointmentTime = entity.AppointmentTime,
                        Address = entity.Address,
                        HairServiceID = entity.HairServiceID,
                        Name = entity.HairService.Name,
                        UserID = entity.UserID
                    };
            }
        }

        public bool UpdateAppointment(AppointmentEdit model)
        {
            using(var apt = new ApplicationDbContext())
            {
                var entity =
                    apt
                        .Appointments
                        .Single(e => e.AppointmentID == model.AppointmentID && e.UserID == _userID);

                entity.AppointmentDate = model.AppointmentDate;
                entity.AppointmentTime = model.AppointmentTime;
                entity.Address = model.Address;
                entity.HairServiceID = model.HairServiceID;

                return apt.SaveChanges() == 1;
            }
        }

        public bool DeleteAppointmentService(int appointmentID)
        {
            using (var apt = new ApplicationDbContext())
            {
                var entity =
                    apt
                        .Appointments
                        .Single(e => e.AppointmentID == appointmentID && e.UserID == _userID);

                apt.Appointments.Remove(entity);

                return apt.SaveChanges() == 1;
            }
        }
    }
}
