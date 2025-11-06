CREATE TABLE [dbo].[Users] (
    [iIdUsers] INT            IDENTITY (1, 1) NOT NULL,
    [iIdRoles] INT            NOT NULL,
    [vchName]  VARCHAR (250)  NOT NULL,
    [vchEmail] VARCHAR (150)  NOT NULL,
    [vchPass]  VARBINARY (32) NOT NULL,
    [bStatus]  BIT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([iIdUsers] ASC),
    CONSTRAINT [FK_Users_iIdRoles] FOREIGN KEY ([iIdRoles]) REFERENCES [dbo].[Roles] ([iIdRoles])
);


GO

