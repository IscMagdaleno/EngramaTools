CREATE TABLE [dbo].[ED_Proceso] (
    [iIdED_Proceso]        INT           IDENTITY (1, 1) NOT NULL,
    [vchTitle]             VARCHAR (50)  NOT NULL,
    [vchDescripcion]       VARCHAR (500) NOT NULL,
    [vchDescripcionIngles] VARCHAR (500) NOT NULL,
    [iIdED_Imagenes]       INT           NOT NULL
);
GO

ALTER TABLE [dbo].[ED_Proceso]
    ADD CONSTRAINT [FK_ED_Proceso_iIdED_Imagenes] FOREIGN KEY ([iIdED_Imagenes]) REFERENCES [dbo].[ED_Imagenes] ([iIdED_Imagenes]);
GO

ALTER TABLE [dbo].[ED_Proceso]
    ADD CONSTRAINT [PK_ED_Proceso] PRIMARY KEY CLUSTERED ([iIdED_Proceso] ASC);
GO

