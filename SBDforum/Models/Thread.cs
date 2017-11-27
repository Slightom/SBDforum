using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBDforum.Models
{
    public class Thread
    {
        [Required]
        public int    ThreadID { get; set; }

        [Required]
        public int    CategoryID { get; set; }

        [Required]
        public int    UserID { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public DateTime Date { get; set; }



        public virtual Category Category { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}