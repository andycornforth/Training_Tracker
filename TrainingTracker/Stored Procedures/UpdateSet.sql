CREATE PROCEDURE [dbo].[UpdateSet]
	@SetId INT,
	@ExerciseId INT,
	@Weight float,
	@Reps INT,
	@Position INT

AS
	
	Update [Set] 
	Set [ExerciseId] = @ExerciseId
	, [Weight] = @Weight
	, [Reps] = @Reps
	, [PositionInLog] = @Position
	Where [SetId] = @SetId