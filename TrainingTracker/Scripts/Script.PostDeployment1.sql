/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

PRINT 'Merging Table: [Gender]'
SET QUOTED_IDENTIFIER ON
SET XACT_ABORT ON
BEGIN TRY
BEGIN TRAN
DECLARE @now DATETIME = GETUTCDATE();

MERGE INTO [Gender] AS Target
USING (VALUES

-- DATA START ------------------------------------------------------------
(1, 'Male')
,(2, 'Female')
-- DATA END --------------------------------------------------------------

) AS Source ([GenderId],[Sex])
ON (Target.[GenderId] = Source.[GenderId])
WHEN NOT MATCHED BY TARGET THEN
INSERT([GenderId],[Sex])
VALUES(Source.[GenderId],Source.[Sex])
WHEN MATCHED THEN
UPDATE SET Sex = Source.Sex;

COMMIT TRAN
END TRY
BEGIN CATCH
SELECT
    ERROR_NUMBER() as ErrorNumber,
    ERROR_MESSAGE() as ErrorMessage;
IF (XACT_STATE()) = -1
BEGIN
    --The transaction is in an uncommittable state. 
    ROLLBACK TRANSACTION;
END;
IF (XACT_STATE()) = 1
BEGIN
    --The transaction is committable.
    COMMIT TRANSACTION;
END;
END CATCH
GO