CREATE PROCEDURE [dbo].[GetAllLogsByPersonId]
	@PersonId INT

AS

	  SELECT [LogId]
      ,[PersonId]
      ,[Title]
      ,[DateAdded]
	  ,(SELECT COUNT (s.LogId) FROM [Set] s where s.LogId = l.LogId) as 'SetCount' 
	   FROM [Log] l
	   WHERE PersonId = @PersonId