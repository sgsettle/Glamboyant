using Glamboyant.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Models.HairServiceModels
{
    public class HairServiceCreate
    {
        [Required]
        [Display(Name = "Type of Service")]
        public ServiceType ServiceType { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
