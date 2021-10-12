using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string Pswd { get; set; }
        public bool IsManger { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
    }
}
