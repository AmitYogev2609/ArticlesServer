﻿
//create database ArticlesDB
//go

//use ArticlesDB
//go


//CREATE TABLE Users(
//    UserID INT identity(1,1) NOT NULL PRIMARY KEY,
//    FirstName NVARCHAR(255) NOT NULL,
//    LastName NVARCHAR(255) NOT NULL,
//    UserName NVARCHAR(255) NOT NULL,
//    BirthDay DATETIME NOT NULL,
//    Email NVARCHAR(255) NOT NULL,
//    Pswd NVARCHAR(255) NOT NULL,
//    IsManger bit default(0) NOT NULL
//);

//CREATE TABLE Article(
//    ArticleID INT identity(1,1) NOT NULL PRIMARY KEY,
//    Text TEXT NOT NULL,
//    ArticleName NVARCHAR(255) NOT NULL
//);

//CREATE TABLE AuthorsArticle(
//    UsersID INT identity(1,1) NOT NULL,
//    ArticleID INT NOT NULL
//);

//CREATE TABLE Interests(
//    InterestID INT identity(1,1) NOT NULL PRIMARY KEY,
//    InterestName NVARCHAR(255) NOT NULL
//);

//CREATE TABLE FollwedInterests(
//    UserId INT NOT NULL,
//    InterestID INT NOT NULL
//);
//CREATE TABLE ArticleIntrestType(
//    ArticleID INT identity(1,1) NOT NULL,
//    IntrestID INT NOT NULL
//);
//CREATE TABLE Followedusers(
//    UserID INT identity(1,1) NOT NULL,
//    FollowingID INT NOT NULL
//);
//CREATE TABLE Comment(
//    ComentID INT identity(1,1) NOT NULL PRIMARY KEY,
//    Text NVARCHAR(255) NOT NULL,
//    UserID INT NOT NULL,
//    ArticleID INT NOT NULL
//);

//CREATE TABLE Report(
//    ReportID INT identity(1,1) NOT NULL PRIMARY KEY,
//    Text NVARCHAR(255) NOT NULL
//);

//CREATE TABLE UserReport(
//    ReportID INT identity(1,1) NOT NULL,
//    UserIdReported INT NOT NULL
//);
//CREATE TABLE FavoriteArticle(
//    ArticleID INT identity(1,1) NOT NULL,
//    UserID INT NOT NULL
//);
//CREATE TABLE Message(
//    MessageID INT identity(1,1) NOT NULL PRIMARY KEY,
//    SenderID INT NOT NULL,
//    Text NVARCHAR(255) NOT NULL,
//    ReceiverID INT NOT NULL
//);

//ALTER TABLE
//    AuthorsArticle ADD CONSTRAINT authorsarticle_usersid_foreign FOREIGN KEY(UsersID) REFERENCES Users(UserID);
//ALTER TABLE
//    FollwedInterests ADD CONSTRAINT follwedinterests_userid_foreign FOREIGN KEY(UserId) REFERENCES Users(UserID);
//ALTER TABLE
//    Followedusers ADD CONSTRAINT followedusers_userid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
//ALTER TABLE
//    Followedusers ADD CONSTRAINT followedusers_followingid_foreign FOREIGN KEY(FollowingID) REFERENCES Users(UserID);
//ALTER TABLE
//    AuthorsArticle ADD CONSTRAINT authorsarticle_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
//ALTER TABLE
//    ArticleIntrestType ADD CONSTRAINT articleintresttype_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
//ALTER TABLE
//    FollwedInterests ADD CONSTRAINT follwedinterests_interestid_foreign FOREIGN KEY(InterestID) REFERENCES Interests(InterestID);
//ALTER TABLE
//    ArticleIntrestType ADD CONSTRAINT articleintresttype_intrestid_foreign FOREIGN KEY(IntrestID) REFERENCES Interests(InterestID);
//ALTER TABLE
//    UserReport ADD CONSTRAINT userreport_reportid_foreign FOREIGN KEY(ReportID) REFERENCES Report(ReportID);
//ALTER TABLE
//    Comment ADD CONSTRAINT comment_userid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
//ALTER TABLE
//    Comment ADD CONSTRAINT comment_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
//ALTER TABLE
//    UserReport ADD CONSTRAINT userreport_useridreported_foreign FOREIGN KEY(UserIdReported) REFERENCES Users(UserID);
//ALTER TABLE
//    FavoriteArticle ADD CONSTRAINT favoritearticle_articleid_foreign FOREIGN KEY(ArticleID) REFERENCES Article(ArticleID);
//ALTER TABLE
//    FavoriteArticle ADD CONSTRAINT favoritearticle_userid_foreign FOREIGN KEY(UserID) REFERENCES Users(UserID);
//ALTER TABLE
//    Message ADD CONSTRAINT message_senderid_foreign FOREIGN KEY(SenderID) REFERENCES Users(UserID);
//ALTER TABLE
//    Message ADD CONSTRAINT message_receiverid_foreign FOREIGN KEY(ReceiverID) REFERENCES Users(UserID);


//INSERT INTO Interests (InterestName) VALUES('Activism')
//INSERT INTO Interests (InterestName) VALUES('Addiction')
//INSERT INTO Interests (InterestName) VALUES('Africa')
//INSERT INTO Interests (InterestName) VALUES('Aging')
//INSERT INTO Interests (InterestName) VALUES('Agriculture')
//INSERT INTO Interests (InterestName) VALUES('AI')
//INSERT INTO Interests (InterestName) VALUES('AIDS')
//INSERT INTO Interests (InterestName) VALUES('Algorithm')
//INSERT INTO Interests (InterestName) VALUES('Aliens')
//INSERT INTO Interests (InterestName) VALUES('Alzheimer')
//INSERT INTO Interests (InterestName) VALUES('Ancient world')
//INSERT INTO Interests (InterestName) VALUES('Animals')
//INSERT INTO Interests (InterestName) VALUES('Animation')
//INSERT INTO Interests (InterestName) VALUES('Antarctica')
//INSERT INTO Interests (InterestName) VALUES('Anthropocene')
//INSERT INTO Interests (InterestName) VALUES('Anthropology')
//INSERT INTO Interests (InterestName) VALUES('Archaeology')
//INSERT INTO Interests (InterestName) VALUES('Architecture')
//INSERT INTO Interests (InterestName) VALUES('Art')
//INSERT INTO Interests (InterestName) VALUES('Asia')
//INSERT INTO Interests (InterestName) VALUES('Asteroid')
//INSERT INTO Interests (InterestName) VALUES('Astrobiology')
//INSERT INTO Interests (InterestName) VALUES('Astronomy')
//INSERT INTO Interests (InterestName) VALUES('Atheism')
//INSERT INTO Interests (InterestName) VALUES('Audacious Project')
//INSERT INTO Interests (InterestName) VALUES('Augmented reality')
//INSERT INTO Interests (InterestName) VALUES('Autism spectrum disorder')
//INSERT INTO Interests (InterestName) VALUES('Bacteria')
//INSERT INTO Interests (InterestName) VALUES('Beauty')
//INSERT INTO Interests (InterestName) VALUES('Bees')
//INSERT INTO Interests (InterestName) VALUES('Behavioral economics')
//INSERT INTO Interests (InterestName) VALUES('Best of the Web')
//INSERT INTO Interests (InterestName) VALUES('Big Bang')
//INSERT INTO Interests (InterestName) VALUES('Biodiversity')
//INSERT INTO Interests (InterestName) VALUES('Bioethics')
//INSERT INTO Interests (InterestName) VALUES('Biology')
//INSERT INTO Interests (InterestName) VALUES('Biomimicry')
//INSERT INTO Interests (InterestName) VALUES('Bionics')
//INSERT INTO Interests (InterestName) VALUES('Biosphere')
//INSERT INTO Interests (InterestName) VALUES('Biotech')
//INSERT INTO Interests (InterestName) VALUES('Birds')
//INSERT INTO Interests (InterestName) VALUES('Blindness')
//INSERT INTO Interests (InterestName) VALUES('Blockchain')
//INSERT INTO Interests (InterestName) VALUES('Body language')
//INSERT INTO Interests (InterestName) VALUES('Books')
//INSERT INTO Interests (InterestName) VALUES('Botany')
//INSERT INTO Interests (InterestName) VALUES('Brain')
//INSERT INTO Interests (InterestName) VALUES('Brazil')
//INSERT INTO Interests (InterestName) VALUES('Buddhism')
//INSERT INTO Interests (InterestName) VALUES('Bullying')
//INSERT INTO Interests (InterestName) VALUES('Business')
//INSERT INTO Interests (InterestName) VALUES('Cancer')
//INSERT INTO Interests (InterestName) VALUES('Capitalism')
//INSERT INTO Interests (InterestName) VALUES('Chemistry')
//INSERT INTO Interests (InterestName) VALUES('China')
//INSERT INTO Interests (InterestName) VALUES('Christianity')
//INSERT INTO Interests (InterestName) VALUES('Cities')
//INSERT INTO Interests (InterestName) VALUES('Climate change')
//INSERT INTO Interests (InterestName) VALUES('Code')
//INSERT INTO Interests (InterestName) VALUES('Cognitive science')
//INSERT INTO Interests (InterestName) VALUES('Collaboration')
//INSERT INTO Interests (InterestName) VALUES('Comedy')
//INSERT INTO Interests (InterestName) VALUES('Communication')
//INSERT INTO Interests (InterestName) VALUES('Community')
//INSERT INTO Interests (InterestName) VALUES('Compassion')
//INSERT INTO Interests (InterestName) VALUES('Computers')
//INSERT INTO Interests (InterestName) VALUES('Conducting')
//INSERT INTO Interests (InterestName) VALUES('Consciousness')
//INSERT INTO Interests (InterestName) VALUES('Conservation')
//INSERT INTO Interests (InterestName) VALUES('Consumerism')
//INSERT INTO Interests (InterestName) VALUES('Coral reefs')
//INSERT INTO Interests (InterestName) VALUES('Coronavirus')
//INSERT INTO Interests (InterestName) VALUES('Corruption')
//INSERT INTO Interests (InterestName) VALUES('Countdown')
//INSERT INTO Interests (InterestName) VALUES('Creativity')
//INSERT INTO Interests (InterestName) VALUES('Crime')
//INSERT INTO Interests (InterestName) VALUES('CRISPR')
//INSERT INTO Interests (InterestName) VALUES('Crowdsourcing')
//INSERT INTO Interests (InterestName) VALUES('Cryptocurrency')
//INSERT INTO Interests (InterestName) VALUES('Culture')
//INSERT INTO Interests (InterestName) VALUES('Curiosity')
//INSERT INTO Interests (InterestName) VALUES('Cyber security')
//INSERT INTO Interests (InterestName) VALUES('Dance')
//INSERT INTO Interests (InterestName) VALUES('Dark matter')
//INSERT INTO Interests (InterestName) VALUES('Data')
//INSERT INTO Interests (InterestName) VALUES('Death')
//INSERT INTO Interests (InterestName) VALUES('Decision-making')
//INSERT INTO Interests (InterestName) VALUES('Deextinction')
//INSERT INTO Interests (InterestName) VALUES('Demo')
//INSERT INTO Interests (InterestName) VALUES('Democracy')
//INSERT INTO Interests (InterestName) VALUES('Depression')
//INSERT INTO Interests (InterestName) VALUES('Design')
//INSERT INTO Interests (InterestName) VALUES('Dinosaurs')
//INSERT INTO Interests (InterestName) VALUES('Disability')
//INSERT INTO Interests (InterestName) VALUES('Discovery')
//INSERT INTO Interests (InterestName) VALUES('Disease')
//INSERT INTO Interests (InterestName) VALUES('Diversity')
//INSERT INTO Interests (InterestName) VALUES('DNA')
//INSERT INTO Interests (InterestName) VALUES('Driverless cars')
//INSERT INTO Interests (InterestName) VALUES('Drones')
//INSERT INTO Interests (InterestName) VALUES('Drugs')
//INSERT INTO Interests (InterestName) VALUES('Ebola')
//INSERT INTO Interests (InterestName) VALUES('Ecology')
//INSERT INTO Interests (InterestName) VALUES('Economics')
//INSERT INTO Interests (InterestName) VALUES('Education')
//INSERT INTO Interests (InterestName) VALUES('Egypt')
//INSERT INTO Interests (InterestName) VALUES('Electricity')
//INSERT INTO Interests (InterestName) VALUES('Emotions')
//INSERT INTO Interests (InterestName) VALUES('Empathy')
//INSERT INTO Interests (InterestName) VALUES('Encryption')
//INSERT INTO Interests (InterestName) VALUES('Energy')
//INSERT INTO Interests (InterestName) VALUES('Engineering')
//INSERT INTO Interests (InterestName) VALUES('Entertainment')
//INSERT INTO Interests (InterestName) VALUES('Entrepreneur')
//INSERT INTO Interests (InterestName) VALUES('Environment')
//INSERT INTO Interests (InterestName) VALUES('Equality')
//INSERT INTO Interests (InterestName) VALUES('Ethics')
//INSERT INTO Interests (InterestName) VALUES('Europe')
//INSERT INTO Interests (InterestName) VALUES('Evolution')
//INSERT INTO Interests (InterestName) VALUES('Exercise')
//INSERT INTO Interests (InterestName) VALUES('Exploration')
//INSERT INTO Interests (InterestName) VALUES('Family')
//INSERT INTO Interests (InterestName) VALUES('Farming')
//INSERT INTO Interests (InterestName) VALUES('Fashion')
//INSERT INTO Interests (InterestName) VALUES('Fear')
//INSERT INTO Interests (InterestName) VALUES('Feminism')
//INSERT INTO Interests (InterestName) VALUES('Film')
//INSERT INTO Interests (InterestName) VALUES('Finance')
//INSERT INTO Interests (InterestName) VALUES('Fish')
//INSERT INTO Interests (InterestName) VALUES('Flight')
//INSERT INTO Interests (InterestName) VALUES('Food')
//INSERT INTO Interests (InterestName) VALUES('Forensics')
//INSERT INTO Interests (InterestName) VALUES('Fossil fuels')
//INSERT INTO Interests (InterestName) VALUES('Friendship')
//INSERT INTO Interests (InterestName) VALUES('Fungi')
//INSERT INTO Interests (InterestName) VALUES('Future')
//INSERT INTO Interests (InterestName) VALUES('Gaming')
//INSERT INTO Interests (InterestName) VALUES('Gardening')
//INSERT INTO Interests (InterestName) VALUES('Gender')
//INSERT INTO Interests (InterestName) VALUES('Genetics')
//INSERT INTO Interests (InterestName) VALUES('Geology')
//INSERT INTO Interests (InterestName) VALUES('Glaciers')
//INSERT INTO Interests (InterestName) VALUES('Global issues')
//INSERT INTO Interests (InterestName) VALUES('Goals')
//INSERT INTO Interests (InterestName) VALUES('Government')
//INSERT INTO Interests (InterestName) VALUES('Grammar')
//INSERT INTO Interests (InterestName) VALUES('Graphic design')
//INSERT INTO Interests (InterestName) VALUES('Happiness')
//INSERT INTO Interests (InterestName) VALUES('Health')
//INSERT INTO Interests (InterestName) VALUES('Health care')
//INSERT INTO Interests (InterestName) VALUES('Hearing')
//INSERT INTO Interests (InterestName) VALUES('Heart')
//INSERT INTO Interests (InterestName) VALUES('Hinduism')
//INSERT INTO Interests (InterestName) VALUES('History')
//INSERT INTO Interests (InterestName) VALUES('Homelessness')
//INSERT INTO Interests (InterestName) VALUES('Human body')
//INSERT INTO Interests (InterestName) VALUES('Human rights')
//INSERT INTO Interests (InterestName) VALUES('Humanities')
//INSERT INTO Interests (InterestName) VALUES('Humanity')
//INSERT INTO Interests (InterestName) VALUES('Humor')
//INSERT INTO Interests (InterestName) VALUES('Identity')
//INSERT INTO Interests (InterestName) VALUES('Illness')
//INSERT INTO Interests (InterestName) VALUES('Illusion')
//INSERT INTO Interests (InterestName) VALUES('Immigration')
//INSERT INTO Interests (InterestName) VALUES('Inclusion')
//INSERT INTO Interests (InterestName) VALUES('India')
//INSERT INTO Interests (InterestName) VALUES('Indigenous peoples')
//INSERT INTO Interests (InterestName) VALUES('Industrial design')
//INSERT INTO Interests (InterestName) VALUES('Infrastructure')
//INSERT INTO Interests (InterestName) VALUES('Innovation')
//INSERT INTO Interests (InterestName) VALUES('Insects')
//INSERT INTO Interests (InterestName) VALUES('International development')
//INSERT INTO Interests (InterestName) VALUES('International relations')
//INSERT INTO Interests (InterestName) VALUES('Internet')
//INSERT INTO Interests (InterestName) VALUES('Interview')
//INSERT INTO Interests (InterestName) VALUES('Invention')
//INSERT INTO Interests (InterestName) VALUES('Investing')
//INSERT INTO Interests (InterestName) VALUES('Islam')
//INSERT INTO Interests (InterestName) VALUES('Journalism')
//INSERT INTO Interests (InterestName) VALUES('Judaism')
//INSERT INTO Interests (InterestName) VALUES('Justice system')
//INSERT INTO Interests (InterestName) VALUES('Kids')
//INSERT INTO Interests (InterestName) VALUES('Language')
//INSERT INTO Interests (InterestName) VALUES('Law')
//INSERT INTO Interests (InterestName) VALUES('Leadership')
//INSERT INTO Interests (InterestName) VALUES('LGBTQIA+')
//INSERT INTO Interests (InterestName) VALUES('Library')
//INSERT INTO Interests (InterestName) VALUES('Life')
//INSERT INTO Interests (InterestName) VALUES('Literature')
//INSERT INTO Interests (InterestName) VALUES('Love')
//INSERT INTO Interests (InterestName) VALUES('Machine learning')
//INSERT INTO Interests (InterestName) VALUES('Magic')
//INSERT INTO Interests (InterestName) VALUES('Manufacturing')
//INSERT INTO Interests (InterestName) VALUES('Maps')
//INSERT INTO Interests (InterestName) VALUES('Marine biology')
//INSERT INTO Interests (InterestName) VALUES('Marketing')
//INSERT INTO Interests (InterestName) VALUES('Mars')
//INSERT INTO Interests (InterestName) VALUES('Math')
//INSERT INTO Interests (InterestName) VALUES('Media')
//INSERT INTO Interests (InterestName) VALUES('Medical imaging')
//INSERT INTO Interests (InterestName) VALUES('Medical research')
//INSERT INTO Interests (InterestName) VALUES('Medicine')
//INSERT INTO Interests (InterestName) VALUES('Meditation')
//INSERT INTO Interests (InterestName) VALUES('Memory')
//INSERT INTO Interests (InterestName) VALUES('Mental health')
//INSERT INTO Interests (InterestName) VALUES('Microbes')
//INSERT INTO Interests (InterestName) VALUES('Microbiology')
//INSERT INTO Interests (InterestName) VALUES('Middle East')
//INSERT INTO Interests (InterestName) VALUES('Military')
//INSERT INTO Interests (InterestName) VALUES('Mindfulness')
//INSERT INTO Interests (InterestName) VALUES('Mission Blue')
//INSERT INTO Interests (InterestName) VALUES('Money')
//INSERT INTO Interests (InterestName) VALUES('Moon')
//INSERT INTO Interests (InterestName) VALUES('Motivation')
//INSERT INTO Interests (InterestName) VALUES('Museums')
//INSERT INTO Interests (InterestName) VALUES('Music')
//INSERT INTO Interests (InterestName) VALUES('Nanotechnology')
//INSERT INTO Interests (InterestName) VALUES('NASA')
//INSERT INTO Interests (InterestName) VALUES('Natural disaster')
//INSERT INTO Interests (InterestName) VALUES('Natural resources')
//INSERT INTO Interests (InterestName) VALUES('Nature')
//INSERT INTO Interests (InterestName) VALUES('Neurology')
//INSERT INTO Interests (InterestName) VALUES('Neuroscience')
//INSERT INTO Interests (InterestName) VALUES('Nuclear energy')
//INSERT INTO Interests (InterestName) VALUES('Ocean')
//INSERT INTO Interests (InterestName) VALUES('Online privacy')
//INSERT INTO Interests (InterestName) VALUES('Pain')
//INSERT INTO Interests (InterestName) VALUES('Painting')
//INSERT INTO Interests (InterestName) VALUES('Paleontology')
//INSERT INTO Interests (InterestName) VALUES('Pandemic')
//INSERT INTO Interests (InterestName) VALUES('Parenting')
//INSERT INTO Interests (InterestName) VALUES('Performance')
//INSERT INTO Interests (InterestName) VALUES('Person')
//INSERT INTO Interests (InterestName) VALUES('Personal growth')
//INSERT INTO Interests (InterestName) VALUES('Personality')
//INSERT INTO Interests (InterestName) VALUES('Philanthropy')
//INSERT INTO Interests (InterestName) VALUES('Philosophy')
//INSERT INTO Interests (InterestName) VALUES('Photography')
//INSERT INTO Interests (InterestName) VALUES('Physics')
//INSERT INTO Interests (InterestName) VALUES('Planets')
//INSERT INTO Interests (InterestName) VALUES('Plants')
//INSERT INTO Interests (InterestName) VALUES('Plastic')
//INSERT INTO Interests (InterestName) VALUES('Podcast')
//INSERT INTO Interests (InterestName) VALUES('Poetry')
//INSERT INTO Interests (InterestName) VALUES('Policy')
//INSERT INTO Interests (InterestName) VALUES('Politics')
//INSERT INTO Interests (InterestName) VALUES('Pollution')
//INSERT INTO Interests (InterestName) VALUES('Potential')
//INSERT INTO Interests (InterestName) VALUES('Poverty')
//INSERT INTO Interests (InterestName) VALUES('Pregnancy')
//INSERT INTO Interests (InterestName) VALUES('Primates')
//INSERT INTO Interests (InterestName) VALUES('Prison')
//INSERT INTO Interests (InterestName) VALUES('Product design')
//INSERT INTO Interests (InterestName) VALUES('Productivity')
//INSERT INTO Interests (InterestName) VALUES('Prosthetics')
//INSERT INTO Interests (InterestName) VALUES('Protest')
//INSERT INTO Interests (InterestName) VALUES('Psychology')
//INSERT INTO Interests (InterestName) VALUES('PTSD')
//INSERT INTO Interests (InterestName) VALUES('Public health')
//INSERT INTO Interests (InterestName) VALUES('Public space')
//INSERT INTO Interests (InterestName) VALUES('Public speaking')
//INSERT INTO Interests (InterestName) VALUES('Quantum')
//INSERT INTO Interests (InterestName) VALUES('Race')
//INSERT INTO Interests (InterestName) VALUES('Refugees')
//INSERT INTO Interests (InterestName) VALUES('Relationships')
//INSERT INTO Interests (InterestName) VALUES('Religion')
//INSERT INTO Interests (InterestName) VALUES('Renewable energy')
//INSERT INTO Interests (InterestName) VALUES('Resources')
//INSERT INTO Interests (InterestName) VALUES('Rivers')
//INSERT INTO Interests (InterestName) VALUES('Robots')
//INSERT INTO Interests (InterestName) VALUES('Rocket science')
//INSERT INTO Interests (InterestName) VALUES('Science')
//INSERT INTO Interests (InterestName) VALUES('Science fiction')
//INSERT INTO Interests (InterestName) VALUES('Self')
//INSERT INTO Interests (InterestName) VALUES('Sex')
//INSERT INTO Interests (InterestName) VALUES('Sexual violence')
//INSERT INTO Interests (InterestName) VALUES('Shopping')
//INSERT INTO Interests (InterestName) VALUES('Sight')
//INSERT INTO Interests (InterestName) VALUES('Slavery')
//INSERT INTO Interests (InterestName) VALUES('Sleep')
//INSERT INTO Interests (InterestName) VALUES('Smell')
//INSERT INTO Interests (InterestName) VALUES('Social change')
//INSERT INTO Interests (InterestName) VALUES('Social media')
//INSERT INTO Interests (InterestName) VALUES('Society')
//INSERT INTO Interests (InterestName) VALUES('Sociology')
//INSERT INTO Interests (InterestName) VALUES('Software')
//INSERT INTO Interests (InterestName) VALUES('Solar energy')
//INSERT INTO Interests (InterestName) VALUES('Solar system')
//INSERT INTO Interests (InterestName) VALUES('Sound')
//INSERT INTO Interests (InterestName) VALUES('South America')
//INSERT INTO Interests (InterestName) VALUES('Space')
//INSERT INTO Interests (InterestName) VALUES('Spoken word')
//INSERT INTO Interests (InterestName) VALUES('Sports')
//INSERT INTO Interests (InterestName) VALUES('Statistics')
//INSERT INTO Interests (InterestName) VALUES('Storytelling')
//INSERT INTO Interests (InterestName) VALUES('Street art')
//INSERT INTO Interests (InterestName) VALUES('String theory')
//INSERT INTO Interests (InterestName) VALUES('Success')
//INSERT INTO Interests (InterestName) VALUES('Suicide')
//INSERT INTO Interests (InterestName) VALUES('Sun')
//INSERT INTO Interests (InterestName) VALUES('Surgery')
//INSERT INTO Interests (InterestName) VALUES('Surveillance')
//INSERT INTO Interests (InterestName) VALUES('Sustainability')
//INSERT INTO Interests (InterestName) VALUES('Synthetic biology')
//INSERT INTO Interests (InterestName) VALUES('Teaching')
//INSERT INTO Interests (InterestName) VALUES('Technology')
//INSERT INTO Interests (InterestName) VALUES('Telescopes')
//INSERT INTO Interests (InterestName) VALUES('Television')
//INSERT INTO Interests (InterestName) VALUES('Terrorism')
//INSERT INTO Interests (InterestName) VALUES('Theater')
//INSERT INTO Interests (InterestName) VALUES('Time')
//INSERT INTO Interests (InterestName) VALUES('Toys')
//INSERT INTO Interests (InterestName) VALUES('Transgender')
//INSERT INTO Interests (InterestName) VALUES('Transportation')
//INSERT INTO Interests (InterestName) VALUES('Travel')
//INSERT INTO Interests (InterestName) VALUES('Trees')
//INSERT INTO Interests (InterestName) VALUES('Trust')
//INSERT INTO Interests (InterestName) VALUES('Typography')
//INSERT INTO Interests (InterestName) VALUES('United States')
//INSERT INTO Interests (InterestName) VALUES('Universe')
//INSERT INTO Interests (InterestName) VALUES('Urban planning')
//INSERT INTO Interests (InterestName) VALUES('Vaccines')
//INSERT INTO Interests (InterestName) VALUES('Violence')
//INSERT INTO Interests (InterestName) VALUES('Virtual reality')
//INSERT INTO Interests (InterestName) VALUES('Virus')
//INSERT INTO Interests (InterestName) VALUES('Visualizations')
//INSERT INTO Interests (InterestName) VALUES('Vulnerability')
//INSERT INTO Interests (InterestName) VALUES('War')
//INSERT INTO Interests (InterestName) VALUES('Water')
//INSERT INTO Interests (InterestName) VALUES('Weather')
//INSERT INTO Interests (InterestName) VALUES('Wind energy')
//INSERT INTO Interests (InterestName) VALUES('Women')
//INSERT INTO Interests (InterestName) VALUES('Women in business')
//INSERT INTO Interests (InterestName) VALUES('Work')
//INSERT INTO Interests (InterestName) VALUES('Work-life balance')
//INSERT INTO Interests (InterestName) VALUES('Writing')
//INSERT INTO Interests (InterestName) VALUES('Youth')
