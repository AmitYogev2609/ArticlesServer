using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ArticlesServerBL.Models;


namespace ArticlesServerBL.Models
{
    public partial class ArtiFindDBContext :DbContext
    {
        public User LogIn(string email, string passwors)
        {
            User user= this.Users.Where(u=>u.Email==email&&u.Pswd==passwors).FirstOrDefault();
            return user;
        }
        public User checkEmailAndGetCode(string email)
        {
            User user = this.Users.Where(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else return null;
        }
        public bool EmailExist(string email)
        {
            User user = this.Users.Where(u => u.Email == email).FirstOrDefault();
            return user != null;
        }
        public bool UserNameExist(string username)
        {
            User user = this.Users.Where(u => u.UserName == username).FirstOrDefault();
            return user != null;
        }
        public List<Interest> GetInitialInterest()
        {
            List<string> names = help.createMainIntrest();
            List<Interest> initialInterest = new List<Interest>();
            foreach (string name in names)
            {
                Interest interest= Interests.Where(u=>u.InterestName==name).FirstOrDefault();
                if(interest!=null)
                    initialInterest.Add(interest);
            }
            return initialInterest;
        }
        
    }
}
