
alter PROCEDURE spSaveED_Imagenes (
	@iIdED_Imagenes       INT,
	@vchImagen            VARCHAR (500),
	@vchDescripcion       VARCHAR (500),
	@vchDescripcionIngles VARCHAR (500),
	@vchAlt               VARCHAR (250)
)
AS
BEGIN
	DECLARE @vchError VARCHAR(200) = '';

	DECLARE @Result AS TABLE (
		bResult        BIT DEFAULT(1),
		vchMessage     VARCHAR( 500 ) DEFAULT(''),
		iIdED_Imagenes INT DEFAULT( -1 )
	);

	SET NOCOUNT ON

	SET XACT_ABORT ON;

	BEGIN TRY
		BEGIN TRANSACTION

		IF (@iIdED_Imagenes <= 0)
		BEGIN
			INSERT INTO dbo.ED_Imagenes
			(
				vchImagen,vchDescripcion,vchDescripcionIngles,
				vchAlt
			)
			VALUES
			(
				@vchImagen,@vchDescripcion,@vchDescripcionIngles,
				@vchAlt
			)
			SET @iIdED_Imagenes = @@IDENTITY
		END
		ELSE
		BEGIN
			UPDATE dbo.ED_Imagenes WITH(ROWLOCK)
			SET
				vchImagen = @vchImagen,
				vchDescripcion = @vchDescripcion,
				vchDescripcionIngles = @vchDescripcionIngles,
				vchAlt = @vchAlt
			WHERE
				iIdED_Imagenes = @iIdED_Imagenes;

		END

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;

		SELECT @vchError = CONCAT( 'spSaveED_Imagenes: ', ERROR_MESSAGE( ), ' ', ERROR_PROCEDURE( ), ' ', ERROR_LINE( ) );
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
	BEGIN
		INSERT INTO @Result
		(
			bResult,vchMessage, iIdED_Imagenes
		)
		VALUES
		(
			1,'', @iIdED_Imagenes
		)
		;
	END
	SELECT *
	FROM
		@Result;

	SET NOCOUNT OFF;
END;
GO

