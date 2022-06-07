use master
--drop database ArtiFindDB

create database ArtiFindDB
go

use ArtiFindDB
go


CREATE TABLE Users(
    UserID INT identity(1,1) NOT NULL PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    UserName NVARCHAR(255) NOT NULL,
    BirthDay DATETIME NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Pswd NVARCHAR(255) NOT NULL,
    IsManger bit default(0) NOT NULL
);

CREATE TABLE Article(
    ArticleID INT identity(1,1) NOT NULL PRIMARY KEY,
    HtmlText TEXT NOT NULL,
    ArticleName NVARCHAR(255) NOT NULL,
    PublishDate DATETIME default(GETDATE()) NOT NULL,
    
);

CREATE TABLE AuthorsArticle(
    UserID INT  NOT NULL,
    ArticleID INT NOT NULL,
    CONSTRAINT AuthorsArticle_PK Primary KEY(UserID, ArticleID)
);

CREATE TABLE Interests(
    InterestID INT identity(1,1) NOT NULL PRIMARY KEY,
    InterestName NVARCHAR(255) NOT NULL
);

CREATE TABLE FollwedInterests(
    UserId INT NOT NULL,
    InterestID INT NOT NULL,
    CONSTRAINT FollwedInterests_PK Primary KEY(UserID, InterestID)
);
CREATE TABLE ArticleInterestType(
    ArticleID INT NOT NULL,
    InterestID INT NOT NULL,
    CONSTRAINT ArticleInterestType_PK Primary KEY(ArticleID, InterestID)
);
CREATE TABLE Followedusers(
    UserID INT NOT NULL,
    FollowingID INT NOT NULL,
    CONSTRAINT Followedusers_PK Primary KEY(UserID, FollowingID)

);
CREATE TABLE Comment(
    ComentID INT identity(1,1) NOT NULL PRIMARY KEY,
    Text NVARCHAR(255) NOT NULL,
    UserID INT NOT NULL,
    ArticleID INT NOT NULL
);

CREATE TABLE ArticleReport(
    ArticleReportID INT identity(1,1) NOT NULL PRIMARY KEY,
    Text NVARCHAR(255) NOT NULL,
    UserIdReported INT NOT NULL Foreign key references Users(UserID),
    ReportedArticleId INT NOT NULL Foreign key references Article(ArticleID),
);

CREATE TABLE UserReport(
    UserReportID INT identity(1,1) NOT NULL PRIMARY KEY,
    Text NVARCHAR(255) NOT NULL,
    UserIdReported INT NOT NULL Foreign key references Users(UserID),
    ReportedUserId INT NOT NULL Foreign key references Users(UserID),
);

CREATE TABLE FavoriteArticle(
    ArticleID INT  NOT NULL,
    UserID INT NOT NULL
);

CREATE TABLE Message(
    MessageID INT identity(1,1) NOT NULL PRIMARY KEY,
    SenderID INT NOT NULL,
    Text NVARCHAR(255) NOT NULL,
    ReceiverID INT NOT NULL
);

ALTER TABLE
    AuthorsArticle ADD CONSTRAINT authorsarticle_usersid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
ALTER TABLE
    FollwedInterests ADD CONSTRAINT follwedinterests_userid_foreign FOREIGN KEY(UserId) REFERENCES Users(UserID);
ALTER TABLE
    Followedusers ADD CONSTRAINT followedusers_userid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
ALTER TABLE
    Followedusers ADD CONSTRAINT followedusers_followingid_foreign FOREIGN KEY(FollowingID) REFERENCES Users(UserID);
ALTER TABLE
    AuthorsArticle ADD CONSTRAINT authorsarticle_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
ALTER TABLE
    ArticleInterestType ADD CONSTRAINT articleInteresttype_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
ALTER TABLE
    FollwedInterests ADD CONSTRAINT follwedinterests_interestid_foreign FOREIGN KEY(InterestID) REFERENCES Interests(InterestID);
ALTER TABLE
    ArticleInterestType ADD CONSTRAINT articleInteresttype_Interestid_foreign FOREIGN KEY(InterestID) REFERENCES Interests(InterestID);
ALTER TABLE
    Comment ADD CONSTRAINT comment_userid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
ALTER TABLE
    Comment ADD CONSTRAINT comment_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
ALTER TABLE
    FavoriteArticle ADD CONSTRAINT favoritearticle_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
ALTER TABLE
    FavoriteArticle ADD CONSTRAINT favoritearticle_userid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
ALTER TABLE
    Message ADD CONSTRAINT message_senderid_foreign FOREIGN KEY(SenderID) REFERENCES Users(UserID);
ALTER TABLE
    Message ADD CONSTRAINT message_receiverid_foreign FOREIGN KEY(ReceiverID) REFERENCES Users(UserID);
ALTER TABLE
    Interests ADD IsMajor bit default(0) NOT NULL
ALTER TABLE
    Article ADD Description NVARCHAR(255) NOT NULL
ALTER TABLE
    FavoriteArticle ADD CONSTRAINT FavoriteArticle_PK Primary KEY(UserID, ArticleId)
ALTER TABLE
    Article ADD AuthorsList NVARCHAR(255) default('') NOT NULL

