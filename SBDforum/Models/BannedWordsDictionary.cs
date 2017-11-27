using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBDforum.Models
{
    public class BannedWordsDictionary
    {
        [Required]
        public int BannedWordsDictionaryID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}