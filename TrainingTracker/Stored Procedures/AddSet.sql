CREATE PROCEDURE [dbo].[AddSet]
	@LogId INT,
	@ExerciseId INT,
	@Weight float,
	@Reps INT,
	@Position INT

AS
	
	Insert into [Set] values (@LogId, @ExerciseId, @Weight, @Reps, @Position)