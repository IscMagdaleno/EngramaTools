-- spSaveGetWorkPlan.sql

-- ---------------------
-- SP de Guardado (Save)
-- ---------------------

IF OBJECT_ID( 'spSaveWorkPlan' ) IS NULL
	EXEC ('CREATE PROCEDURE spSaveWorkPlan AS SET NOCOUNT ON;') 
GO
ALTER PROCEDURE spSaveWorkPlan
    (
    @iIdWorkPlan INT,
    @iIdUser INT,
    @vchInitialDescription VARCHAR(MAX),
    @vchImprovedDescription VARCHAR(MAX),
    @vchFullPlanJSON VARCHAR(MAX) -- Se utiliza el JSON para la persistencia
)
AS 
BEGIN
    DECLARE @vchError VARCHAR(500) = '';
    DECLARE @Result AS TABLE (
        bResult BIT DEFAULT(1),
        vchMessage VARCHAR(500) DEFAULT(''),
        iIdWorkPlan INT DEFAULT(-1)
    );
    
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF @iIdWorkPlan <= 0 
        BEGIN
            -- INSERT: Insertar el plan principal
            INSERT INTO dbo.WorkPlan
                (iIdUser, vchInitialDescription, vchImprovedDescription, vchFullPlanJSON)
            VALUES
                (@iIdUser, @vchInitialDescription, @vchImprovedDescription, @vchFullPlanJSON);

            SET @iIdWorkPlan = SCOPE_IDENTITY();
            -- NOTA: La inserción anidada de Módulos/Fases/Pasos debería ir aquí, 
            -- ya sea parseando el JSON o usando TVPs, pero se omite para el esqueleto.
        END
        ELSE
        BEGIN
            -- UPDATE: Actualizar el plan principal (incluye el JSON completo)
            UPDATE dbo.WorkPlan WITH(ROWLOCK)
            SET
                vchInitialDescription = @vchInitialDescription,
                vchImprovedDescription = @vchImprovedDescription,
                vchFullPlanJSON = @vchFullPlanJSON
            WHERE iIdWorkPlan = @iIdWorkPlan;
            -- NOTA: La lógica para actualizar Módulos/Fases/Pasos se vuelve compleja (Delete/Insert/Update)
            -- y debe ser manejada aquí o simplificada con un borrado y reinserción.
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT @vchError = CONCAT( 'spSaveWorkPlan: ', ERROR_MESSAGE(), ' ', ERROR_PROCEDURE(), ' ', ERROR_LINE() );
    END CATCH;

    -- Devolver Respuesta Estandarizada
    IF LEN(@vchError) > 0
        INSERT INTO @Result (bResult, vchMessage) VALUES (0, @vchError);
    ELSE
        INSERT INTO @Result (bResult, vchMessage, iIdWorkPlan) VALUES (1, '', @iIdWorkPlan);

    SELECT * FROM @Result;
END
GO

