
CREATE PROCEDURE spGetCommonScripts

AS 

CREATE TABLE #Result (

	bResult BIT DEFAULT (1),

	vchMessage VARCHAR(500) DEFAULT(''),

	 iIdCommonScripts INT DEFAULT( -1 ),

	 iIdCatProyectType INT DEFAULT( -1 ),

	 vchName VARCHAR (250) DEFAULT( -1 ),

	 vchDescription VARCHAR (500) DEFAULT( -1 ),

	 vchCode VARCHAR (MAX)  DEFAULT( '' ),

);


SET NOCOUNT ON

	BEGIN TRY

	INSERT INTO  #Result
    (

    iIdCommonScripts,iIdCatProyectType, vchName, vchDescription,
    vchCode )
SELECT
    CS.iIdCommonScripts, CS.iIdCatProyectType, CS.vchName, CS.vchDescription,
    CS.vchCode
FROM
    dbo.CommonScripts CS  WITH(NOLOCK)  



		IF NOT EXISTS (SELECT 1
FROM #Result)

			BEGIN

    INSERT INTO #Result
        (bResult, vchMessage)

    SELECT 0, 'No register found.';

END

	END TRY

	BEGIN CATCH

		INSERT INTO #Result
    (bResult, vchMessage)

SELECT 0, CONCAT(ERROR_PROCEDURE(), ' : ', ERROR_MESSAGE(), ' - ', ERROR_LINE());

		PRINT CONCAT(ERROR_PROCEDURE(), ' : ', ERROR_MESSAGE(), ' - ', ERROR_LINE());

	END CATCH

	SELECT *
FROM #Result;

	DROP TABLE #Result;

GO

