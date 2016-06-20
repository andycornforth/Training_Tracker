CREATE PROCEDURE [dbo].[GetLogById]
	@LogId INT

AS

	  SELECT [LogId]
      ,[PersonId]
      ,[Title]
      ,[DateAdded]
	  ,(SELECT COUNT (LogId) FROM [Set] WHERE LogId = @LogId) as 'SetCount' 
	  FROM [Log] 
	  WHERE LogId = @LogId