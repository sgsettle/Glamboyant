using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Models.ReviewModels
{
    public class ReviewCreate
    {
        public string Text { get; set; }

        public int Rating { get; set; }

        public int UserID { get; set; }
    }
}
