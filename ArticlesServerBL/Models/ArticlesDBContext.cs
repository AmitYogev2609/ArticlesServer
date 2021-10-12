using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ArticlesServerBL.Models
{
    public partial class ArticlesDBContext : DbContext
    {
        public ArticlesDBContext()
        {
        }

        public ArticlesDBContext(DbContextOptions<ArticlesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleIntrestType> ArticleIntrestTypes { get; set; }
        public virtual DbSet<AuthorsArticle> AuthorsArticles { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FavoriteArticle> FavoriteArticles { get; set; }
        public virtual DbSet<Followeduser> Followedusers { get; set; }
        public virtual DbSet<FollwedInterest> FollwedInterests { get; set; }
        public virtual DbSet<Interest> Interests { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserReport> UserReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=ArticlesDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

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

            modelBuilder.Entity<ArticleIntrestType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ArticleIntrestType");

                entity.Property(e => e.ArticleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ArticleID");

                entity.Property(e => e.IntrestId).HasColumnName("IntrestID");

                entity.HasOne(d => d.Article)
                    .WithMany()
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("articleintresttype_articleid_foreign");

                entity.HasOne(d => d.Intrest)
                    .WithMany()
                    .HasForeignKey(d => d.IntrestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("articleintresttype_intrestid_foreign");
            });

            modelBuilder.Entity<AuthorsArticle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AuthorsArticle");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.UsersId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UsersID");

                entity.HasOne(d => d.Article)
                    .WithMany()
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authorsarticle_articleid_foreign");

                entity.HasOne(d => d.Users)
                    .WithMany()
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authorsarticle_usersid_foreign");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.ComentId)
                    .HasName("PK__Comment__A7BAF2A8565B11C4");

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

                entity.Property(e => e.ArticleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ArticleID");

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
                entity.HasNoKey();

                entity.Property(e => e.FollowingId).HasColumnName("FollowingID");

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Following)
                    .WithMany()
                    .HasForeignKey(d => d.FollowingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("followedusers_followingid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("followedusers_userid_foreign");
            });

            modelBuilder.Entity<FollwedInterest>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.InterestId).HasColumnName("InterestID");

                entity.HasOne(d => d.Interest)
                    .WithMany()
                    .HasForeignKey(d => d.InterestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("follwedinterests_interestid_foreign");

                entity.HasOne(d => d.User)
                    .WithMany()
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

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(255);
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
                entity.HasNoKey();

                entity.ToTable("UserReport");

                entity.Property(e => e.ReportId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ReportID");

                entity.HasOne(d => d.Report)
                    .WithMany()
                    .HasForeignKey(d => d.ReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userreport_reportid_foreign");

                entity.HasOne(d => d.UserIdReportedNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.UserIdReported)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userreport_useridreported_foreign");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
