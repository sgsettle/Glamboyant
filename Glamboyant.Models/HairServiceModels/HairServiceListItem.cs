using Glamboyant.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Models.HairServiceModels
{
    public class HairServiceListItem
    {
        public int HairServiceID { get; set; }

        [Display(Name = "Type of Service")]
        public ServiceType ServiceType { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
