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
                .Include(u => u.FolloweduserFollowings).ThenInclude(f => f.User)
                .Include(u => u.FolloweduserUsers).ThenInclude(f => f.Following).ThenInclude(u => u.AuthorsArticles).ThenInclude(ar => ar.Article)
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

            //foreach (var item in this.Articles.Include(au=>au.AuthorsArticles).ThenInclude(a=>a.User))
            //{
            //    string str = "";
            //    foreach (var item1 in item.AuthorsArticles)
            //    {
            //         if(str=="")
            //        {
            //            str = $"by:@{item1.User.UserName}";
            //        }
            //         else
            //        {
            //            str += $", @{item1.User.UserName}";
            //        }
            //    }
            //    item.AuthorsList = str;
            //}
            //this.SaveChanges();
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
            Article article1= this.Articles.Where(art=>art.ArticleId==article.ArticleId).Include(ar=>ar.AuthorsArticles).ThenInclude(ari=>ari.User).FirstOrDefault();
            string str= "";
            foreach (var item in article.AuthorsArticles)
            {
                if(str=="")
                {
                    str = $"by:@{item.User.UserName}";
                }
                else
                {
                    str+=$", @{item.User.UserName}";
                }
            }
            article.AuthorsList = str;
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
        public User GetUserDetails(int id)
        {
            User user = this.Users.Where(u => u.UserId == id)
                .Include(u => u.AuthorsArticles).ThenInclude(a => a.Article)
                .Include(u => u.FollwedInterests).ThenInclude(fi=>fi.Interest)
                .Include(u => u.FolloweduserFollowings).ThenInclude(f => f.User)
                .Include(u => u.FolloweduserUsers).ThenInclude(f => f.Following)
                .FirstOrDefault();
            return user;
        }
        public bool follewUser(int userid,int follewid)
        {
            User user= this.Users.Where(u=>u.UserId==userid).FirstOrDefault();
            Followeduser followeduser = new Followeduser()
            {
                UserId = userid,
                FollowingId = follewid,
            };
            user.FolloweduserUsers.Add(followeduser);
            
            this.SaveChanges();
           return /*user1.Where(u=>u.FollowingId==userid).FirstOrDefault()!=null&&*/user.FolloweduserUsers.Where(u=>u.FollowingId==follewid).FirstOrDefault()!=null;
            
        }
        public bool UnfollewUser(int userid, int follewid)
        {
            User user = this.Users.Where(u => u.UserId == userid).Include(u=>u.FolloweduserFollowings).Include(u=>u.FolloweduserUsers).FirstOrDefault();
            Followeduser followeduser = user.FolloweduserUsers.Where(u => u.FollowingId == follewid).FirstOrDefault();
           user.FolloweduserUsers.Remove(followeduser);
            this.SaveChanges();
            return user.FolloweduserUsers.Where(u => u.FollowingId == follewid).FirstOrDefault() == null;



        }
        public bool uptadeUserDetails(User user)
        {
            User userFromdb= this.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();
            if (userFromdb!=null)
            { 
                userFromdb.Email = user.Email;
                userFromdb.FirstName = user.FirstName;
                userFromdb.LastName = user.LastName;
                userFromdb.UserName = user.UserName;
                this.SaveChanges();
                return true;
            }
            return false;

        }
        public Comment AddComment(Comment comment)
        {
            this.Comments.Add(comment);
            this.SaveChanges();
            int userid=comment.UserId;
            int articleid = comment.ArticleId;
            User user=this.Users.Where(u => u.UserId == userid).FirstOrDefault();
            Article article=this.Articles.Where(ar=>ar.ArticleId==articleid).FirstOrDefault();
            Comment comment1=user.Comments.Where(cm=>cm.ComentId==comment.ComentId).FirstOrDefault();
            Comment comment2=article.Comments.Where(cm=>cm.ComentId==comment.ComentId).FirstOrDefault();
            if(comment1 != null && comment2 != null)
            return comment ;
            return null;
        }
        public List<Comment> GetArticleComments(int Articleid)
        {
            Article article = this.Articles.Where(art => art.ArticleId == Articleid).Include(com=>com.Comments).ThenInclude(cm=>cm.User).FirstOrDefault();
            List<Comment> comments = article.Comments.ToList<Comment>();
            return comments;
        }
        public string GetArticleAuthors(int articleid)
        {
            string str = "";
            Article arti = this.Articles.Where(art => art.ArticleId == articleid).Include(ar => ar.AuthorsArticles).ThenInclude(aa => aa.User).FirstOrDefault();
            if(arti != null)
            {
                foreach (var item in arti.AuthorsArticles)
                {
                    if(str=="")
                    {
                        str += "by:@"+item.User.UserName;
                    }
                    else
                    {
                        str += ", @" + item.User.UserName;
                    }
                }
            }
            return str;
        }
        public Interest AddInterest(string name)
        {
            if(this.Interests.Where(intr=>intr.InterestName==name).FirstOrDefault()==null)
            { 
            Interest interest = new Interest()
            {
                InterestName = name,
                IsMajor=false
            };
            this.Interests.Add(interest);
            this.SaveChanges();
            return interest;
            }
            return null;
        }
        public List<ArticleInterestType> GetArticleIntersetType(int articleId)
        {
            Article article= this.Articles.Where(ar=>ar.ArticleId==articleId).Include(art=>art.ArticleInterestTypes).ThenInclude(ait=>ait.Interest).FirstOrDefault();
            if(article!=null)
            {
                return article.ArticleInterestTypes.ToList<ArticleInterestType>();
            }
            return null;
        }
        public List<AuthorsArticle> GetAuthorsArticle(int articleId)
        {
            Article article = this.Articles.Where(ar => ar.ArticleId == articleId).Include(art => art.AuthorsArticles).ThenInclude(ait => ait.User).FirstOrDefault();
            if (article != null)
            {
                return article.AuthorsArticles.ToList<AuthorsArticle>();
            }
            return null;
        }
        public bool MakeUserAdmin(int userId)
        {
            User user = this.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (user == null)
                return false;
            user.IsManger = true;
            this.SaveChanges();
            return user.IsManger;
        }
    }
}
