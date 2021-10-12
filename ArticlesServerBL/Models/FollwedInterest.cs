using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class FollwedInterest
    {
        public int UserId { get; set; }
        public int InterestId { get; set; }

        public virtual Interest Interest { get; set; }
        public virtual User User { get; set; }
    }
}
