﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Models.ReviewModels
{
    public class ReviewCreate
    {
        public int Rating { get; set; }

        public string Text { get; set; }

        public byte[] Image { get; set; }

        public int UserID { get; set; }
    }
}
