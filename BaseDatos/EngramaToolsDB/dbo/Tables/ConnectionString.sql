CREATE TABLE [dbo].[ConnectionString] (
    [iIdConnectionString] INT           IDENTITY (1, 1) NOT NULL,
    [iIdUser]             INT           NOT NULL,
    [vchConnectionString] VARCHAR (250) NOT NULL,
    [vchNota]             VARCHAR (250) NOT NULL,
    [bActivo]             BIT           NOT NULL,
    CONSTRAINT [PK_ConnectionString] PRIMARY KEY CLUSTERED ([iIdConnectionString] ASC),
    CONSTRAINT [FK_ConnectionString_iIdUser] FOREIGN KEY ([iIdUser]) REFERENCES [dbo].[Users] ([iIdUsers])
);


GO

