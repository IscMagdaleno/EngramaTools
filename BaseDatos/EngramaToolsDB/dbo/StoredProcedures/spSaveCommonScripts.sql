
CREATE PROCEDURE spSaveCommonScripts (

@iIdCommonScripts INT , 
@iIdCatProyectType INT , 
@vchName VARCHAR (250), 
@vchDescription VARCHAR (500), 
@vchCode VARCHAR  (MAX)
) 

AS 



DECLARE @trancount INT = - 1,
	@vchError VARCHAR(200) = '';

DECLARE @identity AS TABLE (ID INT NOT NULL);

DECLARE @Result AS TABLE (
	bResult BIT DEFAULT(1),
	vchMessage VARCHAR(500) DEFAULT(''),
	iIdCommonScripts INT DEFAULT( -1 ) 
	);

SET NOCOUNT ON
BEGIN TRY
	SET XACT_ABORT ON;

	SET @trancount = @@TRANCOUNT

	IF @trancount > 0
		SAVE TRANSACTION spSaveCommonScripts;
	ELSE
		BEGIN TRANSACTION
IF  ( @iIdCommonScripts <= 0) 
BEGIN 
	INSERT INTO dbo.CommonScripts
	 ( 

		iIdCatProyectType, 			vchName, 			vchDescription, 	
		vchCode 	
	)
	VALUES 
	(
		@iIdCatProyectType,		@vchName,		@vchDescription,
		@vchCode
	)
		 SET @iIdCommonScripts = @@IDENTITY

		END
		ELSE
		BEGIN
UPDATE  dbo.CommonScripts WITH(ROWLOCK) SET
	 iIdCatProyectType = @iIdCatProyectType, 
	 vchName = @vchName, 
	 vchDescription = @vchDescription, 
	 vchCode = @vchCode 

 WHERE  iIdCommonScripts  = @iIdCommonScripts;

		END

	GOTO _FinTran
	_RollBack:
	IF @trancount = 0
		ROLLBACK TRANSACTION;
	ELSE IF @trancount <> - 1
		AND XACT_STATE() <> - 1
		ROLLBACK TRANSACTION spSaveCommonScripts;
	GOTO _Fin
		IF @trancount = 0
 _FinTran:
		COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	SELECT @vchError = Concat( 'spSaveCommonScripts: ', ERROR_MESSAGE( ), ' ', ERROR_PROCEDURE( ), ' ', ERROR_LINE( ) )
	PRINT CONCAT ( 'spSaveCommonScripts: ', ERROR_MESSAGE(), ' ', ERROR_PROCEDURE(), ' ', ERROR_LINE() )
	IF @trancount = 0
		ROLLBACK TRANSACTION
	ELSE IF @trancount <> - 1
		IF XACT_STATE() <> - 1
			ROLLBACK TRANSACTION spSaveCommonScripts;
END CATCH
_Fin:
SET NOCOUNT OFF
IF (LEN(@vchError) > 0)
BEGIN
	INSERT INTO @Result (
		bResult,
		vchMessage
		)
	SELECT 0,
		@vchError
END
ELSE
	INSERT INTO @Result (
		bResult,
		vchMessage,
		iIdCommonScripts
		)
	SELECT 1,
		'',
		@iIdCommonScripts
SELECT *
FROM @Result;

GO

