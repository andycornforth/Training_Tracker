CREATE PROCEDURE [dbo].[AddLog]
	@PersonId nvarchar(50),
	@Title nvarchar(50),
	@Date datetime

AS
	
	Insert into [Log] values (@PersonId, @Title, @Date)
