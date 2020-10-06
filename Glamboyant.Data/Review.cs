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
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
