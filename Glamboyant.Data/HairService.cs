using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Data
{
    public class HairService
    {
        [Key]
        public int HairServiceId { get; set; }

        [Required]
        [Display(Name = "Service")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Guid OwnerID { get; set; }
    }
}
