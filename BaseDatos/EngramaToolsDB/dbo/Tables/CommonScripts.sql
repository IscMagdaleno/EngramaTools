CREATE TABLE [dbo].[CommonScripts] (
    [iIdCommonScripts]  INT           IDENTITY (1, 1) NOT NULL,
    [iIdCatProyectType] INT           NOT NULL,
    [vchName]           VARCHAR (250) NOT NULL,
    [vchDescription]    VARCHAR (500) NOT NULL,
    [vchCode]           VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_CommonScripts] PRIMARY KEY CLUSTERED ([iIdCommonScripts] ASC),
    CONSTRAINT [FK_CommonScripts_iIdCatProyectType] FOREIGN KEY ([iIdCatProyectType]) REFERENCES [dbo].[CatProyectType] ([iIdCatProyectType])
);


GO

