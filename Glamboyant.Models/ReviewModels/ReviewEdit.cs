using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Models.ReviewModels
{
    public class ReviewEdit
    {
        public int ReviewID { get; set; }

        public string Text { get; set; }

        public int Rating { get; set; }

        public byte[] Image { get; set; }

        public int UserID { get; set; }
    }
}
