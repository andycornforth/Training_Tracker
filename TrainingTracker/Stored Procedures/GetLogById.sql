CREATE PROCEDURE [dbo].[GetLogById]
	@LogId INT

AS

	SELECT * FROM [Log] WHERE LogId = @LogId