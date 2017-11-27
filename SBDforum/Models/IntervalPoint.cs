using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBDforum.Models
{
    public class IntervalPoint
    {
        [Required]
        public int IntervalPointID { get; set; }

        [Required]
        public int LowerRange { get; set; }

        [Required]
        public int UpperRange { get; set; }

        [Required]
        public string Name { get; set; }
    }
}