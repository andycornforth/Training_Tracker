CREATE TABLE [dbo].[Person]
(
	[PersonId] INT NOT NULL PRIMARY KEY IDENTITY,
	[Username] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(100) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [DOB] DATE NOT NULL, 
    [GenderId] INT NOT NULL, 

    CONSTRAINT [FK_Person_Gender] FOREIGN KEY ([GenderId]) REFERENCES [Gender]([GenderId]),
	CONSTRAINT [Unique_Username] UNIQUE(Username),
    CONSTRAINT [Unique_Email] UNIQUE(Email) 
)
