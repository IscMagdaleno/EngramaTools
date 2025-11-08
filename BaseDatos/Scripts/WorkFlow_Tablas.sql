-- WorkPlanTables.sql
-- Creación de la jerarquía de tablas para el Plan de Trabajo Engrama

IF NOT EXISTS (SELECT * FROM sysobjects o WHERE o.NAME = 'WorkPlan')
BEGIN
    CREATE TABLE WorkPlan (
        iIdWorkPlan INT NOT NULL IDENTITY(1, 1)
            CONSTRAINT PK_WorkPlan PRIMARY KEY (iIdWorkPlan),
            
        iIdUser INT NOT NULL, -- FK a la tabla Users
        vchInitialDescription VARCHAR(MAX) NOT NULL,
        vchImprovedDescription VARCHAR(MAX) NOT NULL,
        vchFullPlanJSON VARCHAR(MAX) NOT NULL, -- Almacena la estructura completa para la reconstrucción
        dtCreationDate DATETIME NOT NULL DEFAULT GETDATE(),
        bStatus BIT NOT NULL DEFAULT 1
    );
    -- Agregar la restricción de clave foránea a Users si es necesario (se omite aquí por no tener la tabla Users en este script, pero debería existir en la DB)
END

IF NOT EXISTS (SELECT * FROM sysobjects o WHERE o.NAME = 'ModulePlan')
BEGIN
    CREATE TABLE ModulePlan (
        iIdModulePlan INT NOT NULL IDENTITY(1, 1)
            CONSTRAINT PK_ModulePlan PRIMARY KEY (iIdModulePlan),
            
        iIdWorkPlan INT NOT NULL
            Constraint FK_ModulePlan_iIdWorkPlan Foreign Key (iIdWorkPlan)
            References WorkPlan(iIdWorkPlan),

        vchModuleName VARCHAR(150) NOT NULL,
        vchModulePurpose VARCHAR(500) NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects o WHERE o.NAME = 'Phase')
BEGIN
    CREATE TABLE Phase (
        iIdPhase INT NOT NULL IDENTITY(1, 1)
            CONSTRAINT PK_Phase PRIMARY KEY (iIdPhase),
            
        iIdModulePlan INT NOT NULL
            Constraint FK_Phase_iIdModulePlan Foreign Key (iIdModulePlan)
            References ModulePlan(iIdModulePlan),

        vchPhaseName VARCHAR(150) NOT NULL,
        vchPhaseObjective VARCHAR(500) NOT NULL
    );
END

IF NOT EXISTS (SELECT * FROM sysobjects o WHERE o.NAME = 'Step')
BEGIN
    CREATE TABLE Step (
        iIdStep INT NOT NULL IDENTITY(1, 1)
            CONSTRAINT PK_Step PRIMARY KEY (iIdStep),
            
        iIdPhase INT NOT NULL
            Constraint FK_Step_iIdPhase Foreign Key (iIdPhase)
            References Phase(iIdPhase),

        vchStepDescription VARCHAR(MAX) NOT NULL,
        vchLayer VARCHAR(50) NOT NULL, -- Capa Engrama
        bIsCompleted BIT NOT NULL DEFAULT 0
    );
END