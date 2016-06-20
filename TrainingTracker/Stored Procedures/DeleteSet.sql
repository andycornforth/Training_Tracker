CREATE PROCEDURE [dbo].[DeleteSet]
	@SetId INT

AS

	DELETE FROM [Set] WHERE SetId = @SetId