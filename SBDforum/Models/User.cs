using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SBDforum.Models
{

    public class User
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public string Nick { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public Boolean Role { get; set; } // [DefaultValue(false)] false-user, true-admin

        [Required]
        public string Avatar { get; set; } // DefaultValue("http://megaicons.net/static/img/icons_sizes/84/445/256/blue-user-icon.png")]

        [Required]
        public Boolean Active { get; set; } // [DefaultValue(true)] user never deleted. Admin can only disable him


        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesReceived { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}