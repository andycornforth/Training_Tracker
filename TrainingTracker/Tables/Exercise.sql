CREATE TABLE [dbo].[Exercise]
(
	[ExerciseId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL,
	CONSTRAINT [Unique_Title] UNIQUE(Title) 
)
