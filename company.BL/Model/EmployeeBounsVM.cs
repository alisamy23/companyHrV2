using company.DAL.Repository.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.BL.Model
{
    public class EmployeeBounsVM
    {
        public int id { get; set; }
        [Required]
        [RegularExpression("^(?!0)[0-9]*$", ErrorMessage = "The field is requird.")]
        public int bonusId { get; set; }
        [Required]
        [RegularExpression("^(?!0)[0-9]*$", ErrorMessage = "The field is requird.")]
        public int employeeId { get; set; }
        [Required]

        public DateTime date { get; set; }
        public virtual bonuss bonus { get; set; } = null!;
        public virtual employee employee { get; set; } = null!;

    }
}
