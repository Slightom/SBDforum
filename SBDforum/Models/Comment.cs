using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBDforum.Models
{
    public class Comment
    {
        [Required]
        public int CommentID { get; set; }

        [Required]
        public int AnswerID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual User User { get; set; }
    }
}