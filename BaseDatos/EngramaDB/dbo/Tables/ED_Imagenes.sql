CREATE TABLE [dbo].[ED_Imagenes] (
    [iIdED_Imagenes]       INT           IDENTITY (1, 1) NOT NULL,
    [vchImagen]            VARCHAR (500) NOT NULL,
    [vchDescripcion]       VARCHAR (500) NOT NULL,
    [vchDescripcionIngles] VARCHAR (500) NOT NULL,
    [vchAlt]               VARCHAR (250) NOT NULL
);
GO

ALTER TABLE [dbo].[ED_Imagenes]
    ADD CONSTRAINT [PK_ED_Imagenes] PRIMARY KEY CLUSTERED ([iIdED_Imagenes] ASC);
GO

