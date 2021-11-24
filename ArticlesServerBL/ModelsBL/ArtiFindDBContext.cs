using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace ArticlesServerBL.Models
{
    public partial class ArtiFindDBContext :DbContext
    {
        public User LogIn(string email, string passwors)
        {
            User user= this.Users.Where(u=>u.Email==email&&u.Pswd==passwors).FirstOrDefault();
            return user;
        }
    }
}
