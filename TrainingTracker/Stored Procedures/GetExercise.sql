CREATE PROCEDURE [dbo].[GetExercise]
	@Id INT

AS

	SELECT Top 1 [ExerciseId], [Title] from dbo.Exercise WHERE [ExerciseId] = @Id