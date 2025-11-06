CREATE TABLE [dbo].[ED_Descripcion] (
    [iIdED_Descripcion]    INT           IDENTITY (1, 1) NOT NULL,
    [vchDescripcion]       VARCHAR (500) NOT NULL,
    [vchDescripcionIngles] VARCHAR (500) NOT NULL,
    [iIdED_Imagenes]       INT           NOT NULL
);
GO

ALTER TABLE [dbo].[ED_Descripcion]
    ADD CONSTRAINT [FK_ED_Descripcion_iIdED_Imagenes] FOREIGN KEY ([iIdED_Imagenes]) REFERENCES [dbo].[ED_Imagenes] ([iIdED_Imagenes]);
GO

ALTER TABLE [dbo].[ED_Descripcion]
    ADD CONSTRAINT [PK_ED_Descripcion] PRIMARY KEY CLUSTERED ([iIdED_Descripcion] ASC);
GO

