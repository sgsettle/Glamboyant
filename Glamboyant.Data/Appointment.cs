using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Data
{
    public class Appointment
    {
        [Key]
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

        [ForeignKey(nameof(HairService))]
        [Display(Name = "Service")]
        public int HairServiceID { get; set; }
        public virtual HairService HairService { get; set; }

        [ForeignKey(nameof(User))]
        [Display(Name = "Client")]
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
