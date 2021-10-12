using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class Followeduser
    {
        public int UserId { get; set; }
        public int FollowingId { get; set; }

        public virtual User Following { get; set; }
        public virtual User User { get; set; }
    }
}
