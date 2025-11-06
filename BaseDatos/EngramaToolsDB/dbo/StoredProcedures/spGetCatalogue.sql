
CREATE PROCEDURE spGetCatalogue(
    @vchCatalogueName VARCHAR(250)
)
AS
/*
** Nombre:                        spGetCatalogue
** Proposito:                     Retrieve the catalog details based on the provided catalog name.
*/

BEGIN
    -- Local table variable to capture response instead of a temporary table
    DECLARE @Result TABLE (
        bResult BIT DEFAULT (1),
        vchMessage VARCHAR(500) DEFAULT(''),
        iId INT DEFAULT(-1),
        vchDescription VARCHAR(250) DEFAULT('')
    );

    DECLARE @Query NVARCHAR(MAX);  -- Use NVARCHAR for dynamic SQL

    SET NOCOUNT ON;

    BEGIN TRY
        -- Construct dynamic SQL query string using safe concatenation
        SET @Query = N'SELECT iId'+@vchCatalogueName +', vchDescription FROM dbo.' + QUOTENAME(@vchCatalogueName) + ' WITH (NOLOCK)';

        PRINT @Query;  -- Print the query for debugging

        -- Execute the dynamic SQL and insert the results into the local table variable
        INSERT INTO @Result (iId, vchDescription)
        EXEC sp_executesql @Query;  -- Use sp_executesql for improved security and flexibility

        -- Check if no rows were returned
        IF NOT EXISTS (SELECT 1 FROM @Result WHERE iId <> -1)
        BEGIN
            INSERT INTO @Result (bResult, vchMessage)
            VALUES (0, 'No records found.');
        END

    END TRY
    BEGIN CATCH
        -- Handle errors by inserting error details into the response table variable
        INSERT INTO @Result (bResult, vchMessage)
        VALUES (0, CONCAT(ERROR_PROCEDURE(), ' : ', ERROR_MESSAGE(), ' - ', ERROR_LINE()));

        PRINT CONCAT(ERROR_PROCEDURE(), ' : ', ERROR_MESSAGE(), ' - ', ERROR_LINE());  -- Print the error details for debugging
    END CATCH

    -- Select the response to return it to the caller
    SELECT * FROM @Result;

END

GO

