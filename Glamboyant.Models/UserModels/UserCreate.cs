using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glamboyant.Models.UserModels
{
    public class UserCreate
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
