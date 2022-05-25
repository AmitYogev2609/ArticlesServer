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
            User user = this.Users.Where(u => u.Email == email && u.Pswd == passwors)
                .Include(u => u.FollwedInterests).ThenInclude(i => i.Interest).ThenInclude(ii => ii.ArticleInterestTypes).ThenInclude(ar => ar.Article).ThenInclude(art=>art.AuthorsArticles).ThenInclude(tf=>tf.User)
                .Include(u => u.AuthorsArticles).ThenInclude(a => a.Article)
                .Include(u => u.FolloweduserFollowings).ThenInclude(f => f.User).ThenInclude(u => u.AuthorsArticles).ThenInclude(ar => ar.Article)
                .Include(u => u.FolloweduserUsers).ThenInclude(f => f.User)
                .Include(u => u.FavoriteArticles).ThenInclude(f => f.Article)
                .FirstOrDefault()
                ;
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

            //List<Interest> Interest = this.Interests.Include(u=>u.ArticleInterestTypes).ThenInclude(arti=>arti.Article).ThenInclude(cle=>cle.AuthorsArticles).ThenInclude(ath=>ath.User)
            //    .Include(t=>t.FollwedInterests).ThenInclude(tu=>tu.User).ToList<Interest>();
            List<Interest> Interest = this.Interests.Include(u => u.ArticleInterestTypes).ThenInclude(arti => arti.Article).ThenInclude(cle => cle.AuthorsArticles)
                .ToList<Interest>();

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
        public Article addArticle(Article article)
        {
            if (article == null)
                return null;
            this.Articles.Add(article);
            this.SaveChanges();
            return article;
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
        public List<Article> GetFavoriteArticles(User user)
        {
            List<Article> Articles = new List<Article>();
            foreach (FavoriteArticle Fa in this.FavoriteArticles)
            {
                if(Fa.UserId==user.UserId)
                {
                    Articles.Add(Fa.Article);
                }
            }
            return Articles;
        }
        public bool UptadeFavoriteArticle(Article article,User user)
        {
            FavoriteArticle favoriteArticle = new FavoriteArticle()
            {
                ArticleId=article.ArticleId,
                UserId=user.UserId
            };
            
            User user1 = this.Users.Where(u => u.Email == user.Email && u.Pswd == user.Pswd).FirstOrDefault();
            user1.FavoriteArticles.Add(favoriteArticle);
            this.SaveChanges();
            return true;
        }
      
        public bool RemoveFavoriteArticle(Article article, User user)
        {
           
            User user1 = this.Users.Where(u => u.Email == user.Email && u.Pswd == user.Pswd).Include(u=>u.FavoriteArticles).ThenInclude(far=>far.Article).FirstOrDefault();
            FavoriteArticle favoriteArticle = user1.FavoriteArticles.Where(u => u.ArticleId == article.ArticleId).FirstOrDefault();
            bool suc= user1.FavoriteArticles.Remove(favoriteArticle);
            
            this.SaveChanges();
            return suc;
        }
        public User GetUserById(int userid)
        {
            return this.Users.Where(u => u.UserId == userid).FirstOrDefault();
        }
        public bool UnFollowInterest(int userid,int interestid)
        {
            User user = this.Users.Where(u => u.UserId == userid).Include(u=>u.FollwedInterests).FirstOrDefault();
            Interest interest = this.Interests.Where(intr=>intr.InterestId==interestid).FirstOrDefault();
            FollwedInterest follwedInterest= user.FollwedInterests.Where((fr) => fr.InterestId==interestid).FirstOrDefault();
            bool suc= user.FollwedInterests.Remove(follwedInterest);
            this.SaveChanges();
            bool suc2= interest.FollwedInterests.Where(fi=>fi.UserId==user.UserId).FirstOrDefault()==null;
            return suc && suc2;
        }
        public bool FollowInterest(int userid, int interestid)
        {
            User user = this.Users.Where(u => u.UserId == userid).Include(u => u.FollwedInterests).FirstOrDefault();
            Interest interest = this.Interests.Where(intr => intr.InterestId == interestid).FirstOrDefault();
            
             user.FollwedInterests.Add(new FollwedInterest() 
            { UserId=user.UserId,
            InterestId=interestid,
            });
            this.SaveChanges();
            bool suc2 = interest.FollwedInterests.Where(fi => fi.UserId == user.UserId).FirstOrDefault() != null;
            return suc2;
        }
    }
}
