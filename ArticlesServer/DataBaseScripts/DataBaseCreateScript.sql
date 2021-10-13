create database ArticlesDB
go

use ArticlesDB
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
    Text TEXT NOT NULL,
    ArticleName NVARCHAR(255) NOT NULL
);

CREATE TABLE AuthorsArticle(
    UsersID INT identity(1,1) NOT NULL,
    ArticleID INT NOT NULL
);

CREATE TABLE Interests(
    InterestID INT identity(1,1) NOT NULL PRIMARY KEY,
    InterestName NVARCHAR(255) NOT NULL
);

CREATE TABLE FollwedInterests(
    UserId INT NOT NULL,
    InterestID INT NOT NULL
);
CREATE TABLE ArticleIntrestType(
    ArticleID INT identity(1,1) NOT NULL,
    IntrestID INT NOT NULL
);
CREATE TABLE Followedusers(
    UserID INT identity(1,1) NOT NULL,
    FollowingID INT NOT NULL
);
CREATE TABLE Comment(
    ComentID INT identity(1,1) NOT NULL PRIMARY KEY,
    Text NVARCHAR(255) NOT NULL,
    UserID INT NOT NULL,
    ArticleID INT NOT NULL
);

CREATE TABLE Report(
    ReportID INT identity(1,1) NOT NULL PRIMARY KEY,
    Text NVARCHAR(255) NOT NULL
);

CREATE TABLE UserReport(
    ReportID INT identity(1,1) NOT NULL,
    UserIdReported INT NOT NULL
);
CREATE TABLE FavoriteArticle(
    ArticleID INT identity(1,1) NOT NULL,
    UserID INT NOT NULL
);
CREATE TABLE Message(
    MessageID INT identity(1,1) NOT NULL PRIMARY KEY,
    SenderID INT NOT NULL,
    Text NVARCHAR(255) NOT NULL,
    ReceiverID INT NOT NULL
);

ALTER TABLE
    AuthorsArticle ADD CONSTRAINT authorsarticle_usersid_foreign FOREIGN KEY(UsersID) REFERENCES Users(UserID);
ALTER TABLE
    FollwedInterests ADD CONSTRAINT follwedinterests_userid_foreign FOREIGN KEY(UserId) REFERENCES Users(UserID);
ALTER TABLE
    Followedusers ADD CONSTRAINT followedusers_userid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
ALTER TABLE
    Followedusers ADD CONSTRAINT followedusers_followingid_foreign FOREIGN KEY(FollowingID) REFERENCES Users(UserID);
ALTER TABLE
    AuthorsArticle ADD CONSTRAINT authorsarticle_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
ALTER TABLE
    ArticleIntrestType ADD CONSTRAINT articleintresttype_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
ALTER TABLE
    FollwedInterests ADD CONSTRAINT follwedinterests_interestid_foreign FOREIGN KEY(InterestID) REFERENCES Interests(InterestID);
ALTER TABLE
    ArticleIntrestType ADD CONSTRAINT articleintresttype_intrestid_foreign FOREIGN KEY(IntrestID) REFERENCES Interests(InterestID);
ALTER TABLE
    UserReport ADD CONSTRAINT userreport_reportid_foreign FOREIGN KEY(ReportID) REFERENCES Report(ReportID);
ALTER TABLE
    Comment ADD CONSTRAINT comment_userid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
ALTER TABLE
    Comment ADD CONSTRAINT comment_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
ALTER TABLE
    UserReport ADD CONSTRAINT userreport_useridreported_foreign FOREIGN KEY(UserIdReported) REFERENCES Users(UserID);
ALTER TABLE
    FavoriteArticle ADD CONSTRAINT favoritearticle_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
ALTER TABLE
    FavoriteArticle ADD CONSTRAINT favoritearticle_userid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
ALTER TABLE
    Message ADD CONSTRAINT message_senderid_foreign FOREIGN KEY(SenderID) REFERENCES Users(UserID);
ALTER TABLE
    Message ADD CONSTRAINT message_receiverid_foreign FOREIGN KEY(ReceiverID) REFERENCES Users(UserID);