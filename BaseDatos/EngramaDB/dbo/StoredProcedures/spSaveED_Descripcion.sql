ALTER PROCEDURE spSaveED_Descripcion (
	@iIdED_Descripcion    INT,
	@iIdED_Proceso        int,
	@vchTitulo             VARCHAR (500),
	@vchDescripcion       VARCHAR (3500),
	@vchDescripcionIngles VARCHAR (500),
	@iIdED_Imagenes       INT
)
AS
BEGIN
	DECLARE @vchError VARCHAR(200) = '';

	DECLARE @Result AS TABLE (
		bResult           BIT DEFAULT(1),
		vchMessage        VARCHAR( 500 ) DEFAULT(''),
		iIdED_Descripcion INT DEFAULT( -1 )
	);

	SET NOCOUNT ON

	SET XACT_ABORT ON;

	BEGIN TRY
		BEGIN TRANSACTION

		IF (@iIdED_Descripcion <= 0)
		BEGIN
			INSERT INTO dbo.ED_Descripcion
			(
				iIdED_Proceso,vchDescripcion,vchDescripcionIngles,
				iIdED_Imagenes,vchTitulo
			)
			VALUES
			(
				@iIdED_Proceso,@vchDescripcion,@vchDescripcionIngles,
				@iIdED_Imagenes,@vchTitulo
			)
			SET @iIdED_Descripcion = @@IDENTITY
		END
		ELSE
		BEGIN
			UPDATE dbo.ED_Descripcion WITH(ROWLOCK)
			SET
				vchDescripcion = @vchDescripcion,
				vchDescripcionIngles = @vchDescripcionIngles,
				iIdED_Imagenes = @iIdED_Imagenes,
				vchTitulo = @vchTitulo
			WHERE
				iIdED_Descripcion = @iIdED_Descripcion;

		END

		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;

		SELECT @vchError = CONCAT( 'spSaveED_Descripcion: ', ERROR_MESSAGE( ), ' ', ERROR_PROCEDURE( ), ' ', ERROR_LINE( ) );
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
			bResult,vchMessage,iIdED_Descripcion
		)
		VALUES
		(
			1,'',@iIdED_Descripcion
		)
	;

	SELECT *
	FROM
		@Result;

	SELECT *
	FROM
		@Result;

	SET NOCOUNT OFF;
END;

GO 
