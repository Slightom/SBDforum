using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBDforum.Models
{
    public class Answer
    {
        [Required]
        public int AnswerID { get; set; }

        [Required]
        public int ThreadID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public string Text { get; set; }


        public virtual Thread Thread { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}