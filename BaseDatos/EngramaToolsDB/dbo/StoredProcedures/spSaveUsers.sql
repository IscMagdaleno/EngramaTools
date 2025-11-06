
ALTER PROCEDURE spSaveUsers
    (
    @iIdUsers INT,
    @iIdRoles INT,
    @vchName VARCHAR(50),
    @vchEmail VARCHAR(150),
    @vchPass VARCHAR(MAX),
    @vchNickName VARCHAR(50),
    @bStatus BIT
)
AS 
BEGIN
    -- Variables to manage transaction and error handling
    DECLARE @trancount INT = -1,
            @vchError VARCHAR(200) = '';

    -- Table variable for the response
    DECLARE @Result AS TABLE (
        bResult BIT DEFAULT(1),
        vchMessage VARCHAR(500) DEFAULT(''),
        iIdUsers INT DEFAULT(-1)
    );

    -- Disable row count messages
    SET NOCOUNT ON;

    BEGIN TRY
        SET XACT_ABORT ON;

        
        -- Checked that the registered email does not exist
        IF EXISTS (SELECT 1
            FROM dbo.Users U
            WHERE ( U.vchEmail = @vchEmail OR @vchNickName = @vchNickName)
                AND U.iIdUsers <> @iIdUsers
                AND U.bStatus = 1)
                BEGIN
                SET @vchError = 'The email O nickname already exists.'
                	
             GOTO _Fin

        END;


        -- Get current transaction count
        SET @trancount = @@TRANCOUNT;

        -- Save transaction state if already in transaction
        IF @trancount > 0
            SAVE TRANSACTION spSaveUsers;
        ELSE
            BEGIN TRANSACTION;


        -- Check if it's an insert or update operation
        IF @iIdUsers <= 0 
        BEGIN
        -- Insert new user
        INSERT INTO dbo.Users
            (
            iIdRoles, vchName,
            vchEmail, vchPass, bStatus, vchNickName
            )
        VALUES
            (
                3, @vchName,
                @vchEmail, HASHBYTES('SHA2_256', @vchPass), 1, @vchNickName
            );

        -- Retrieve the newly inserted identity
        SET @iIdUsers = SCOPE_IDENTITY();
    END
        ELSE
        BEGIN
        -- Update existing user
        UPDATE dbo.Users WITH(ROWLOCK)
            SET
                iIdRoles = @iIdRoles,
                vchName = @vchName,
                vchEmail = @vchEmail,
                bStatus = @bStatus,
                vchNickName =@vchNickName
            WHERE iIdUsers = @iIdUsers;
    END

        -- Commit transaction if it was started by this procedure
        IF @trancount = 0
            COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        -- Capture error details
        SELECT @vchError = CONVERT(VARCHAR(200), CONCAT('spSaveUsers: ', ERROR_MESSAGE(), ' in ', ERROR_PROCEDURE(), ' at line ', ERROR_LINE()));
        PRINT CONCAT('spSaveUsers: ', ERROR_MESSAGE(), ' in ', ERROR_PROCEDURE(), ' at line ', ERROR_LINE());
        
        -- Rollback transaction if needed
        IF @trancount = 0
            ROLLBACK TRANSACTION;
        ELSE IF @trancount <> -1 AND XACT_STATE() <> -1
            ROLLBACK TRANSACTION spSaveUsers;
    END CATCH;

_Fin:
    -- Prepare response
    IF LEN(@vchError) > 0
    BEGIN
        INSERT INTO @Result
            (bResult, vchMessage)
        VALUES
            (0, @vchError);
    END
    ELSE
    BEGIN
        INSERT INTO @Result
            (bResult, vchMessage, iIdUsers)
        VALUES
            (1, '', @iIdUsers);
    END

    -- Return response
    SELECT *
    FROM @Result;

    -- Enable row count messages back
    SET NOCOUNT OFF;
END

GO

