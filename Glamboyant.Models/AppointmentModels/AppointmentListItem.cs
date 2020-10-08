using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Models.AppointmentModels
{
    public class AppointmentListItem
    {
        public int AppointmentID { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Appointment Time")]
        public TimeSpan AppointmentTime { get; set; }

        public string Address { get; set; }

        [Display(Name = "Service")]
        public int HairServiceID { get; set; }

        [Display(Name = "Client")]
        public string UserID { get; set; }
    }
}
