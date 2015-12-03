CREATE PROCEDURE [dbo].[GetAllLogsByPersonId]
	@PersonId INT

AS

	SELECT * FROM [Log] WHERE PersonId = @PersonId