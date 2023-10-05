﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace company.BL.Model
{
    public class jobVM
    {
        [Key]
        public int id { get; set; }

        [StringLength(50)]
        public string jobName { get; set; } = null!;

        [StringLength(50)]
        [Required ]
        public string? salary { get; set; }
    }
}
