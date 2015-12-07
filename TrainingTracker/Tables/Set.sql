CREATE TABLE [dbo].[Set]
(
	[SetId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LogId] INT NOT NULL, 
	[ExerciseId] INT NOT NULL,
    [Weight] FLOAT NOT NULL, 
    [Reps] INT NOT NULL, 
    [PositionInLog] INT NOT NULL,
	CONSTRAINT Set_PositionInLog UNIQUE ([LogId], [PositionInLog])
)
