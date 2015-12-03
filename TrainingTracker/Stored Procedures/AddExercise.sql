CREATE PROCEDURE [dbo].[AddExercise]
	@Title nvarchar(50)

AS
	

	IF NOT EXISTS (SELECT [Title] from dbo.Exercise WHERE [Title] = @Title)
	BEGIN
		Insert into [Exercise] values (@Title)
	END

	SELECT Top 1 [ExerciseId], [Title] from dbo.Exercise WHERE [Title] = @Title