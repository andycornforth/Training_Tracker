CREATE PROCEDURE [dbo].[AddSet]
	@LogId INT,
	@ExerciseId INT,
	@Weight float,
	@Reps INT,
	@Position INT

AS

	DECLARE @SetId INT
	
	SET @SetId = (Select [SetId] from dbo.[Set] Where [LogId] = @LogId and [PositionInLog] = @Position)

	IF  (@SetId IS NULL)
	BEGIN
		Insert into [Set] values (@LogId, @ExerciseId, @Weight, @Reps, @Position)
	END
	ELSE
	BEGIN
		Update [Set] 
			Set [ExerciseId] = @ExerciseId
			, [Weight] = @Weight
			, [Reps] = @Reps
			, [PositionInLog] = @Position
			Where [SetId] = @SetId
	END