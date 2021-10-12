using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public string Text { get; set; }
        public int ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
        public virtual User Sender { get; set; }
    }
}
