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
            //להוסיף אינקלוד למשתמשים שעוקביפ אחריו למאמרים שלו לתחומי עינין שלו
            User user = this.Users.Where(u => u.Email == email && u.Pswd == passwors).FirstOrDefault();
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
        public List<Interest> GetInterest()
        {

            List<Interest> Interest = this.Interests.ToList<Interest>();
            
            return Interest;
        }
        public User Signup(User user)
        {
            if (user == null)
                return null;
            this.Users.Add(user);
            this.SaveChanges();
            return this.LogIn(user.Email, user.Pswd);
        }
        public List<User> GetUsers()
        {
            return Users.ToList<User>();
        }
        public List<Article> GetAllArticles()
        { return Articles.ToList<Article>(); }
        public List<Article> GetFollwedArticles(User user)
        {
            return null;
        }
        
    }
}
