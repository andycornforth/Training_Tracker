CREATE PROCEDURE [dbo].[AddLog]
	@PersonId INT,
	@Title nvarchar(50),
	@Date datetime

AS
	
	Insert into [Log] values (@PersonId, @Title, @Date)
	SELECT CAST(SCOPE_IDENTITY() AS INT)
