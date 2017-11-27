using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SBDforum.Models
{
    public class Message
    {
        [Required]
        public int MessageID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int SenderID { get; set; }

        [Required]
        public int ReceiverID { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public bool Read { get; set; } 

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}