IF OBJECT_ID( 'spSaveConnectionString' ) IS NULL

	EXEC ('CREATE PROCEDURE spSaveConnectionString AS SET NOCOUNT ON;') 

GO

ALTER PROCEDURE spSaveConnectionString
    (

    @iIdConnectionString INT ,
    @iIdUser INT ,
    @vchConnectionString VARCHAR(250) ,
    @vchNota VARCHAR(250) ,
    @bActivo BIT
)

AS 



DECLARE @trancount INT = - 1,
	@vchError VARCHAR(200) = '';

DECLARE @identity AS TABLE (ID INT NOT NULL);

DECLARE @Result AS TABLE (
    bResult BIT DEFAULT(1),
    vchMessage VARCHAR(500) DEFAULT(''),
    iIdConnectionString INT DEFAULT( -1 ) 
	);

SET NOCOUNT ON
BEGIN TRY
	SET XACT_ABORT ON;

	SET @trancount = @@TRANCOUNT

	IF @trancount > 0
		SAVE TRANSACTION spSaveConnectionString;
	ELSE
		BEGIN TRANSACTION
IF  ( @iIdConnectionString <= 0) 
BEGIN
    INSERT INTO dbo.ConnectionString
        (

        iIdUser, vchConnectionString, vchNota,
        bActivo
        )
    VALUES
        (
            @iIdUser, @vchConnectionString, @vchNota,
            @bActivo
	)
    SET @iIdConnectionString = @@IDENTITY

END
		ELSE
		BEGIN
    UPDATE  dbo.ConnectionString WITH(ROWLOCK) SET
	 iIdUser = @iIdUser, 
	 vchConnectionString = @vchConnectionString, 
	 vchNota = @vchNota, 
	 bActivo = @bActivo 

 WHERE  iIdConnectionString  = @iIdConnectionString;

END

	GOTO _FinTran
	_RollBack:
	IF @trancount = 0
		ROLLBACK TRANSACTION;
	ELSE IF @trancount <> - 1
    AND XACT_STATE() <> - 1
		ROLLBACK TRANSACTION spSaveConnectionString;
	GOTO _Fin
		IF @trancount = 0
 _FinTran:
		COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	SELECT @vchError = Concat( 'spSaveConnectionString: ', ERROR_MESSAGE( ), ' ', ERROR_PROCEDURE( ), ' ', ERROR_LINE( ) )
	PRINT CONCAT ( 'spSaveConnectionString: ', ERROR_MESSAGE(), ' ', ERROR_PROCEDURE(), ' ', ERROR_LINE() )
	IF @trancount = 0
		ROLLBACK TRANSACTION
	ELSE IF @trancount <> - 1
		IF XACT_STATE() <> - 1
			ROLLBACK TRANSACTION spSaveConnectionString;
END CATCH
_Fin:
SET NOCOUNT OFF
IF (LEN(@vchError) > 0)
BEGIN
    INSERT INTO @Result
        (
        bResult,
        vchMessage
        )
    SELECT 0,
        @vchError
END
ELSE
	INSERT INTO @Result
    (
    bResult,
    vchMessage,
    iIdConnectionString
    )
SELECT 1,
    '',
    @iIdConnectionString
SELECT *
FROM @Result;
 GO

