CREATE PROCEDURE [dbo].[AddPerson]
	@Username nvarchar(50),
	@Password nvarchar(50),
    @FirstName nvarchar(50),
	@LastName nvarchar(50),
    @Email nvarchar(100),
	@DOB date,
	@Gender int

AS
	
	Insert into [Person] values (@Username, @Password, @FirstName, @LastName, @Email, @DOB, @Gender)

