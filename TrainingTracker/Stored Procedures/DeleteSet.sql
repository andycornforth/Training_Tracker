CREATE PROCEDURE [dbo].[DeleteSet]
	@LogId INT,
	@SetId INT

AS

  DECLARE @Pos INT 
  SET @Pos = (select [PositionInLog] from [Set] where [SetId] = @SetId) 
	
  DELETE FROM [Set] WHERE SetId = @SetId

  UPDATE [Set] Set PositionInLog = (PositionInLog - 1) WHERE LogId = @LogId AND PositionInLog > @Pos 