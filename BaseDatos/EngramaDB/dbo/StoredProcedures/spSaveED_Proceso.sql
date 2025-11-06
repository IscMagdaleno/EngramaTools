
ALTER PROCEDURE spSaveED_Proceso (
	@iIdED_Proceso        INT,
	@vchTitle             VARCHAR (50),
	@vchDescripcion       VARCHAR (500),
	@vchDescripcionIngles VARCHAR (500),
	@vchImage        VARCHAR (500)
)
AS
BEGIN
	DECLARE @vchError VARCHAR(200) = '';

	DECLARE @Result AS TABLE (
		bResult       BIT DEFAULT(1),
		vchMessage    VARCHAR( 500 ) DEFAULT(''),
		iIdED_Proceso INT DEFAULT( -1 )
	);

	SET NOCOUNT ON

	SET XACT_ABORT ON;

	BEGIN TRY
		BEGIN TRANSACTION

		IF (@iIdED_Proceso <= 0)
		BEGIN
			INSERT INTO dbo.ED_Proceso
			(
				vchTitle,vchDescripcion,vchDescripcionIngles,
				vchImage
			)
			VALUES
			(
				@vchTitle,@vchDescripcion,@vchDescripcionIngles,
				@vchImage
			)
			SET @iIdED_Proceso = @@IDENTITY
		END
		ELSE
		BEGIN
			UPDATE dbo.ED_Proceso WITH(ROWLOCK)
			SET
				vchTitle = @vchTitle,
				vchDescripcion = @vchDescripcion,
				vchDescripcionIngles = @vchDescripcionIngles,
				vchImage = @vchImage
			WHERE
				iIdED_Proceso = @iIdED_Proceso;

		END

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;

		SELECT @vchError = CONCAT( 'spSaveED_Proceso: ', ERROR_MESSAGE( ), ' ', ERROR_PROCEDURE( ), ' ', ERROR_LINE( ) );
		PRINT @vchError;
	END CATCH

	IF LEN( @vchError ) > 0
	BEGIN
		INSERT INTO @Result
		(
			bResult,vchMessage
		)
		VALUES
		(
			0,@vchError
		)
		;
	END
	ELSE
	INSERT INTO @Result
		(
			bResult,vchMessage,iIdED_Proceso
		)
		VALUES
		(
			1,'',@iIdED_Proceso
		)
		;

	SELECT *
	FROM
		@Result;

	SET NOCOUNT OFF;
END;
GO

