CREATE PROCEDURE [dbo].[AddLog]
	@Username nvarchar(50),
	@Title nvarchar(50)

AS
	
	Insert into [Log] values (@Username, @Title)
