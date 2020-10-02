using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Models.AppointmentModels
{
    public class AppointmentEdit
    {
        public int AppointmentID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Appointment Time")]
        public TimeSpan AppointmentTime { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Service")]
        public int HairServiceID { get; set; }

        [Display(Name = "Client")]
        public int UserID { get; set; }
    }
}
