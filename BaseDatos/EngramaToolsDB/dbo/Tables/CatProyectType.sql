CREATE TABLE [dbo].[CatProyectType] (
    [iIdCatProyectType] INT           NOT NULL,
    [vchDescription]    VARCHAR (250) NOT NULL,
    [bStatus]           BIT           NOT NULL,
    CONSTRAINT [PK_CatProyectType] PRIMARY KEY CLUSTERED ([iIdCatProyectType] ASC)
);


GO

