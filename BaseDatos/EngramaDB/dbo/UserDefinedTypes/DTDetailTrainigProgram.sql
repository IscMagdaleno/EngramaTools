CREATE TYPE [dbo].[DTDetailTrainigProgram] AS TABLE (
    [iIdPC_DetailTrainigProgram] INT           DEFAULT ((-1)) NULL,
    [iIdPC_DayTraining]          INT           DEFAULT ((-1)) NULL,
    [iIdPC_Workouts]             INT           DEFAULT ((-1)) NULL,
    [iNoSeries]                  INT           DEFAULT ((-1)) NULL,
    [iNoRepetitions]             INT           DEFAULT ((-1)) NULL,
    [vchObservation]             VARCHAR (500) DEFAULT ('') NULL);


GO

