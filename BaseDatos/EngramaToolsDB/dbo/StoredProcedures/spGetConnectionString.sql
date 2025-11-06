ALTER PROCEDURE spGetConnectionString(
	@iIdUser INT, 
	@bActivo INT
)
AS
	CREATE TABLE #Result (
		bResult             BIT DEFAULT (1),
		vchMessage          VARCHAR( 500 ) DEFAULT(''),
		iIdConnectionString INT DEFAULT( -1 ),
		iIdUser             INT DEFAULT( -1 ),
		vchConnectionString VARCHAR ( 250 ) DEFAULT(''),
		vchNota             VARCHAR ( 250 ) DEFAULT(''),
		bActivo             BIT DEFAULT( 0 ),
	);

	SET NOCOUNT ON

	BEGIN TRY
		INSERT INTO #Result
		(
			iIdConnectionString,iIdUser,vchConnectionString,
			vchNota,bActivo
		)
		SELECT CS.iIdConnectionString,CS.iIdUser,CS.vchConnectionString,
			   CS.vchNota,CS.bActivo
		FROM
			dbo.ConnectionString CS WITH(NOLOCK)
		WHERE
			CS.iIdUser = @iIdUser
			AND(CS.bActivo = @bActivo OR  @bActivo = 0)

		IF NOT EXISTS ( SELECT 1
						FROM
							#Result )
		BEGIN
			INSERT INTO #Result
			(
				bResult,vchMessage
			)
			SELECT 0,'No register found.';

		END

	END TRY
	BEGIN CATCH
		INSERT INTO #Result
		(
			bResult,vchMessage
		)
		SELECT 0,CONCAT( ERROR_PROCEDURE( ), ' : ', ERROR_MESSAGE( ), ' - ', ERROR_LINE( ) );

		PRINT CONCAT( ERROR_PROCEDURE( ), ' : ', ERROR_MESSAGE( ), ' - ', ERROR_LINE( ) );

	END CATCH

	SELECT *
	FROM
		#Result;

	DROP TABLE #Result;

GO 


EXEC spGetConnectionString 2,0
EXEC spGetConnectionString 2,1

