CREATE PROCEDURE [dbo].[GetSetsByLogId]
	@LogId INT

AS

	SELECT s.SetId, s.LogId, s.ExerciseId, s.Weight, s.Reps, s.PositionInLog, l.PersonId, e.Title FROM [Set] s 
	INNER JOIN [Log] l on l.LogId = s.LogId
	INNER JOIN [Exercise] e on e.ExerciseId = s.ExerciseId
	WHERE s.LogId = @LogId