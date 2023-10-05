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
    public class EmployeesVacationVM
    {
        public int id { get; set; }
        [Required]
        [RegularExpression("^(?!0)[0-9]*$", ErrorMessage = "The field is requird.")]
        public int vacationId { get; set; }
        [Required]
        [RegularExpression("^(?!0)[0-9]*$", ErrorMessage = "The field is requird.")]
        public int employeeId { get; set; }
        [Required]

        [Column(TypeName = "datetime")]
        public DateTime fromDate { get; set; }
        [Required]

        [Column(TypeName = "datetime")]
        public DateTime toDate { get; set; }
        public virtual employee employee { get; set; } = null!;

        public virtual vacation vacation { get; set; } = null!;

    }
}
