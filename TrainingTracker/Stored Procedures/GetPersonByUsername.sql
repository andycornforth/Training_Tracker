CREATE PROCEDURE [dbo].[GetPersonByUsername] ( @Username NVARCHAR (254))

AS
BEGIN
SELECT *
FROM [dbo].[Person] 
WHERE [Username] = @Username;
END

