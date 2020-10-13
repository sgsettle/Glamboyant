using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Data
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        public int Rating { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        [ForeignKey(nameof(User))]
        [Display(Name = "Client")]
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
