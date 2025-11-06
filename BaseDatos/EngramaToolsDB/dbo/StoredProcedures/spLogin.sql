CREATE PROCEDURE spLogin
(
    @vchEmail VARCHAR(50),
    @vchPassword VARCHAR(max)
)
AS

/*
** Name:        spLogin
** Purpose:     Authenticate a user by their email and password.
*/

-- Create a temporary table to store the response
CREATE TABLE #Result
(
    bResult BIT DEFAULT (1),
    vchMessage VARCHAR(500) DEFAULT(''),
    iIdUsers INT DEFAULT (-1),
    iIdRoles INT DEFAULT (-1),
    vchName VARCHAR(50) DEFAULT (''),
    vchNameRole VARCHAR(50) DEFAULT (''),
    vchEmail VARCHAR(150) DEFAULT (''),
    bStatus BIT DEFAULT (0)
);

-- Disable row count messages
SET NOCOUNT ON;

BEGIN TRY
    -- Variable to hold the stored hash of the password
    DECLARE @StoredHash VARBINARY(32);  -- Use VARBINARY for consistency with HASHBYTES
    
    -- Retrieve the stored hash for the given email
    SELECT @StoredHash = CAST(vchPass AS VARBINARY(32))  -- Ensure the correct conversion to VARBINARY
    FROM Users
    WHERE vchEmail = @vchEmail;

    -- Check if the email exists
    IF @StoredHash IS NULL
    BEGIN
        -- Email does not exist
        INSERT INTO #Result (bResult, vchMessage)
        SELECT 0, 'Invalid email or password.';
    END
    ELSE
    BEGIN
        -- Hash the provided password
        DECLARE @ProvidedHash VARBINARY(32);
        SET @ProvidedHash = HASHBYTES('SHA2_256', @vchPassword);

        -- Check if the provided hash matches the stored hash
        IF @ProvidedHash = @StoredHash
        BEGIN
            -- Insert user details into the temporary table from Users and Roles tables
            INSERT INTO #Result
                (
                iIdUsers, iIdRoles, vchName, vchNameRole,
                vchEmail, bStatus
                )
            SELECT
                U.iIdUsers, U.iIdRoles, U.vchName, R.vchName,
                U.vchEmail, U.bStatus
            FROM
                dbo.Users U WITH(NOLOCK)
                INNER JOIN
                dbo.Roles R WITH(NOLOCK) ON R.iIdRoles = U.iIdRoles
            WHERE 
                U.vchEmail = @vchEmail;
        END
        ELSE
        BEGIN
            -- Password does not match
            INSERT INTO #Result (bResult, vchMessage)
            SELECT 0, 'Invalid email or password.';
        END
    END

    -- Ensure there's a response in the temporary table
    IF NOT EXISTS (SELECT 1 FROM #Result)
    BEGIN
        INSERT INTO #Result (bResult, vchMessage)
        SELECT 0, 'Invalid email or password.';
    END

END TRY
BEGIN CATCH
    -- Handle errors and insert into the temporary table
    INSERT INTO #Result (bResult, vchMessage)
    SELECT 0, CONCAT(ERROR_PROCEDURE(), ' : ', ERROR_MESSAGE(), ' - ', ERROR_LINE());

    -- Optionally print the error for debugging
    PRINT CONCAT(ERROR_PROCEDURE(), ' : ', ERROR_MESSAGE(), ' - ', ERROR_LINE());
END CATCH

-- Select the response from the temporary table
SELECT * FROM #Result;

-- Drop the temporary table
DROP TABLE #Result;

GO

