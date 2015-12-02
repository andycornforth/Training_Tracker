CREATE TABLE [dbo].[Log]
(
	[LogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    [DateAdded] DATETIME NOT NULL, 
    CONSTRAINT [FK_Log_To_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([PersonId]),
	CONSTRAINT [UC_Username_Title] UNIQUE ([PersonId],[Title])
)
