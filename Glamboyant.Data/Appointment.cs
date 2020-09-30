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

        public DateTime AppointmentDate { get; set; }

        [ForeignKey(nameof(HairService))]
        public int HairServiceID { get; set; }
        public virtual HairService HairService { get; set; }

        [ForeignKey(nameof(User))]
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
