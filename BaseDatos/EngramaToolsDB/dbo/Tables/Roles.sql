CREATE TABLE [dbo].[Roles] (
    [iIdRoles] INT          IDENTITY (1, 1) NOT NULL,
    [vchName]  VARCHAR (50) NOT NULL,
    [bStatus]  BIT          NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([iIdRoles] ASC)
);


GO

