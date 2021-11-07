using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class ArtiFindDBContext : DbContext
    {
        public ArtiFindDBContext()
        {
        }

        public ArtiFindDBContext(DbContextOptions<ArtiFindDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleInterestType> ArticleInterestTypes { get; set; }
        public virtual DbSet<ArticleReport> ArticleReports { get; set; }
        public virtual DbSet<AuthorsArticle> AuthorsArticles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FavoriteArticle> FavoriteArticles { get; set; }
        public virtual DbSet<Followeduser> Followedusers { get; set; }
        public virtual DbSet<FollwedInterest> FollwedInterests { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserReport> UserReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=ArtiFindDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.ArticleName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text");
            });

            modelBuilder.Entity<ArticleInterestType>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.InterestId })
                    .HasName("ArticleInterestType_PK");

                entity.ToTable("ArticleInterestType");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.InterestId).HasColumnName("InterestID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleInterestTypes)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("articleInteresttype_articleid_foreign");

                entity.HasOne(d => d.Interest)
                    .WithMany(p => p.ArticleInterestTypes)
                    .HasForeignKey(d => d.InterestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("articleInteresttype_Interestid_foreign");
            });

            modelBuilder.Entity<ArticleReport>(entity =>
            {
                entity.ToTable("ArticleReport");

                entity.Property(e => e.ArticleReportId).HasColumnName("ArticleReportID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.ReportedArticle)
                    .WithMany(p => p.ArticleReports)
                    .HasForeignKey(d => d.ReportedArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArticleRe__Repor__35BCFE0A");

                entity.HasOne(d => d.UserIdReportedNavigation)
                    .WithMany(p => p.ArticleReports)
                    .HasForeignKey(d => d.UserIdReported)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArticleRe__UserI__34C8D9D1");
            });

            modelBuilder.Entity<AuthorsArticle>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ArticleId })
                    .HasName("AuthorsArticle_PK");

                entity.ToTable("AuthorsArticle");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.AuthorsArticles)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authorsarticle_articleid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthorsArticles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authorsarticle_usersid_foreign");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.ComentId)
                    .HasName("PK__Comment__A7BAF2A8E3ABB299");

                entity.ToTable("Comment");

                entity.Property(e => e.ComentId).HasColumnName("ComentID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_articleid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_userid_foreign");
            });

            modelBuilder.Entity<FavoriteArticle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FavoriteArticle");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Article)
                    .WithMany()
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favoritearticle_articleid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favoritearticle_userid_foreign");
            });

            modelBuilder.Entity<Followeduser>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.FollowingId })
                    .HasName("Followedusers_PK");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FollowingId).HasColumnName("FollowingID");

                entity.HasOne(d => d.Following)
                    .WithMany(p => p.FolloweduserFollowings)
                    .HasForeignKey(d => d.FollowingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("followedusers_followingid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FolloweduserUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("followedusers_userid_foreign");
            });

            modelBuilder.Entity<FollwedInterest>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.InterestId })
                    .HasName("FollwedInterests_PK");

                entity.Property(e => e.InterestId).HasColumnName("InterestID");

                entity.HasOne(d => d.Interest)
                    .WithMany(p => p.FollwedInterests)
                    .HasForeignKey(d => d.InterestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("follwedinterests_interestid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FollwedInterests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("follwedinterests_userid_foreign");
            });

            modelBuilder.Entity<Interest>(entity =>
            {
                entity.Property(e => e.InterestId).HasColumnName("InterestID");

                entity.Property(e => e.InterestName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("message_receiverid_foreign");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("message_senderid_foreign");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Pswd)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserReport>(entity =>
            {
                entity.ToTable("UserReport");

                entity.Property(e => e.UserReportId).HasColumnName("UserReportID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.ReportedUser)
                    .WithMany(p => p.UserReportReportedUsers)
                    .HasForeignKey(d => d.ReportedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRepor__Repor__398D8EEE");

                entity.HasOne(d => d.UserIdReportedNavigation)
                    .WithMany(p => p.UserReportUserIdReportedNavigations)
                    .HasForeignKey(d => d.UserIdReported)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserRepor__UserI__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
