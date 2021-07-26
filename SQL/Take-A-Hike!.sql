USE [master]
GO

IF db_id('Take_A_Hike') IS NOT NULL
BEGIN
  ALTER DATABASE [Take_A_Hike] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE [Take_A_Hike]
END
GO

CREATE DATABASE [Take_A_Hike]
GO

USE [Take_A_Hike]
GO

---------------------------------------------------------------------------

CREATE TABLE [userType] (
  [id] int PRIMARY KEY IDENTITY NOT NULL,
  [name] nvarchar(20) NOT NULL
)
GO

CREATE TABLE [users] (
  [id] int PRIMARY KEY IDENTITY NOT NULL,
  [firstName] nvarchar(255) NOT NULL,
  [lastName] nvarchar(255) NOT NULL,
  [email] nvarchar(255) NOT NULL,
  [FirebaseUserId] nvarchar(255) NOT NULL,
  [userTypeId] int NOT NULL

  CONSTRAINT UQ_FirebaseUserId UNIQUE(FirebaseUserId),
  CONSTRAINT FK_users_userType FOREIGN KEY (userTypeId) REFERENCES userType(id)
)
GO

CREATE TABLE [parks] (
  [id] int PRIMARY KEY IDENTITY NOT NULL,
  [parkName] nvarchar(255) NOT NULL,
  [description] nvarchar(500) NOT NULL,
  [contactInfo] nvarchar(255) NOT NULL,
  [imageURL] nvarchar(255),
  [address] nvarchar(255) NOT NULL,
  [websiteLink] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [myHikes] (
  [id] int PRIMARY KEY IDENTITY NOT NULL,
  [userId] int NOT NULL,
  [parkId] int NOT NULL,
  [dateOfHike] dateTime NOT NULL,

  CONSTRAINT FK_myHikes_users FOREIGN KEY (userId) REFERENCES users(id),
  CONSTRAINT FK_myHikes_parks FOREIGN KEY (parkId) REFERENCES parks(id)
)
GO

---------------------------------------------------------------------------

SET IDENTITY_INSERT [userType] ON
INSERT INTO [userType]
  ([id], [name])
VALUES 
  (1, 'admin'), 
  (2, 'user');
SET IDENTITY_INSERT [userType] OFF

SET IDENTITY_INSERT [users] ON
INSERT INTO [users]
	([id], [firstName], [lastName], [email], [fireBaseUserId], [userTypeId])
VALUES
	(1, 'Brandon', 'Hill', 'brandonsemail@email.com', 'W0jMpDtpqcWhBd3Kh1Wbp69ryC13', 1)
SET IDENTITY_INSERT [users] OFF

SET IDENTITY_INSERT [parks] ON
INSERT INTO [parks]
	([id], [parkName], [description], [contactInfo], [imageURL], [address], [websiteLink])
VALUES
	(1, 'Radnor Lake', 'Radnor Lake State Natural Area, also known as, Radnor Lake State Park, is a popular state natural area and state park in Oak Hill, Tennessee within Nashville. The 1,368 acres nature preserve lies just outside Nashville. Five miles of unpaved trails wander through the woods surrounding the lake.', '(615) 373-3467', null, '1160 Otter Creek Rd, Nashville, TN 37220', 'https://tnstateparks.com/parks/radnor-lake')
SET IDENTITY_INSERT [parks] OFF