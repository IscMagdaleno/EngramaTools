ALTER PROCEDURE spSaveConnectionString
(
    @iIdConnectionString INT,
    @iIdUser INT,
    @vchConnectionString VARCHAR(250),
    @vchNota VARCHAR(250),
    @bActivo BIT
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @vchError VARCHAR(200) = '';
    DECLARE @Result TABLE (
        bResult BIT,
        vchMessage VARCHAR(500),
        iIdConnectionString INT
    );

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @iIdConnectionString <= 0
        BEGIN
            INSERT INTO dbo.ConnectionString (iIdUser, vchConnectionString, vchNota, bActivo)
            VALUES (@iIdUser, @vchConnectionString, @vchNota, @bActivo);
            
            SET @iIdConnectionString = SCOPE_IDENTITY();
        END
        ELSE
        BEGIN
            UPDATE dbo.ConnectionString WITH (ROWLOCK)
            SET vchConnectionString = @vchConnectionString,
                vchNota = @vchNota,
                bActivo = @bActivo
            WHERE iIdConnectionString = @iIdConnectionString;
        END

        COMMIT TRANSACTION;

        INSERT INTO @Result (bResult, vchMessage, iIdConnectionString)
        VALUES (1, '', @iIdConnectionString);
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        SET @vchError = CONCAT('spSaveConnectionString: ', ERROR_MESSAGE(), ' Line: ', ERROR_LINE());

        INSERT INTO @Result (bResult, vchMessage, iIdConnectionString)
        VALUES (0, @vchError, -1);
    END CATCH

    SET NOCOUNT OFF;

    SELECT * FROM @Result;
END;
GO