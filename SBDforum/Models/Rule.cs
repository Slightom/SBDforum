using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBDforum.Models
{
    public class Rule
    {
        [Required]
        public int RuleID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }
    }
}