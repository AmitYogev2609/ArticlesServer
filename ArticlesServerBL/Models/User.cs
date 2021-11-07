using System;
using System.Collections.Generic;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class User
    {
        public User()
        {
            ArticleReports = new HashSet<ArticleReport>();
            AuthorsArticles = new HashSet<AuthorsArticle>();
            Comments = new HashSet<Comment>();
            FolloweduserFollowings = new HashSet<Followeduser>();
            FolloweduserUsers = new HashSet<Followeduser>();
            FollwedInterests = new HashSet<FollwedInterest>();
            MessageReceivers = new HashSet<Message>();
            MessageSenders = new HashSet<Message>();
            UserReportReportedUsers = new HashSet<UserReport>();
            UserReportUserIdReportedNavigations = new HashSet<UserReport>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Email { get; set; }
        public string Pswd { get; set; }
        public bool IsManger { get; set; }

        public virtual ICollection<ArticleReport> ArticleReports { get; set; }
        public virtual ICollection<AuthorsArticle> AuthorsArticles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Followeduser> FolloweduserFollowings { get; set; }
        public virtual ICollection<Followeduser> FolloweduserUsers { get; set; }
        public virtual ICollection<FollwedInterest> FollwedInterests { get; set; }
        public virtual ICollection<Message> MessageReceivers { get; set; }
        public virtual ICollection<Message> MessageSenders { get; set; }
        public virtual ICollection<UserReport> UserReportReportedUsers { get; set; }
        public virtual ICollection<UserReport> UserReportUserIdReportedNavigations { get; set; }
    }
}
