using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Models.AppointmentModels
{
    public class AppointmentListItem
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }

        public int HairServiceID { get; set; }

        public int UserID { get; set; }
    }
}
