CREATE TABLE [dbo].[Catalogues] (
    [iIdCatalogues]  INT           IDENTITY (1, 1) NOT NULL,
    [vchName]        VARCHAR (250) NOT NULL,
    [siInitialRange] SMALLINT      NOT NULL,
    [siFinalRange]   SMALLINT      NOT NULL,
    [vchNote]        VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Catalogues] PRIMARY KEY CLUSTERED ([iIdCatalogues] ASC)
);


GO

