CREATE TABLE [dbo].[Log]
(
	[LogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Log_To_Person] FOREIGN KEY ([Username]) REFERENCES [Person]([Username]),
	CONSTRAINT [UC_Username_Title] UNIQUE ([Username],[Title])
)
