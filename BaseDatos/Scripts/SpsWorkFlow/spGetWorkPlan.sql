
-- ---------------------
-- SP de Consulta (Get)
-- ---------------------

IF OBJECT_ID( 'spGetWorkPlan' ) IS NULL
	EXEC ('CREATE PROCEDURE spGetWorkPlan AS SET NOCOUNT ON;') 
GO

ALTER PROCEDURE spGetWorkPlan
    (
    @iIdWorkPlan INT
)
AS 
BEGIN
    CREATE TABLE #Result (
        bResult BIT DEFAULT (1),
        vchMessage VARCHAR(500) DEFAULT(''),
        iIdWorkPlan INT DEFAULT (-1),
        iIdUser INT DEFAULT (-1),
        vchInitialDescription VARCHAR(MAX) DEFAULT (''),
        vchImprovedDescription VARCHAR(MAX) DEFAULT (''),
        dtCreationDate DATETIME DEFAULT ('1900-01-01'),
        bStatus BIT DEFAULT (0),
        vchFullPlanJSON VARCHAR(MAX) DEFAULT('')
    );

    SET NOCOUNT ON;

    BEGIN TRY
        -- Insertar el plan de trabajo principal
        INSERT INTO #Result
            (iIdWorkPlan, iIdUser, vchInitialDescription, vchImprovedDescription, dtCreationDate, bStatus, vchFullPlanJSON)
        SELECT
            WP.iIdWorkPlan, WP.iIdUser, WP.vchInitialDescription, WP.vchImprovedDescription, WP.dtCreationDate, WP.bStatus, WP.vchFullPlanJSON
        FROM dbo.WorkPlan WP WITH(NOLOCK)
        WHERE 
            WP.iIdWorkPlan = @iIdWorkPlan;

        IF NOT EXISTS (SELECT 1 FROM #Result)
            INSERT INTO #Result (bResult, vchMessage) VALUES (0, 'Plan de trabajo no encontrado.');
    END TRY
    BEGIN CATCH
        INSERT INTO #Result (bResult, vchMessage)
        SELECT 0, CONCAT(ERROR_PROCEDURE(), ' : ', ERROR_MESSAGE(), ' - ', ERROR_LINE());
    END CATCH

    SELECT * FROM #Result;
    DROP TABLE #Result;
END
GO