CREATE PROCEDURE [dbo].[GetLatestSetForLog]
	@LogId INT

AS

SELECT TOP 1 [SetId]
      ,[LogId]
      ,[ExerciseId]
      ,[Weight]
      ,[Reps]
      ,[PositionInLog]
  FROM [Set]
  WHERE [Set].LogId = @LogId ORDER BY PositionInLog DESC