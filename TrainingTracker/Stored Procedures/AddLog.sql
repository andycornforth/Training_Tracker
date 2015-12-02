CREATE PROCEDURE [dbo].[AddLog]
	@PersonId nvarchar(50),
	@Title nvarchar(50)

AS
	
	Insert into [Log] values (@PersonId, @Title)
