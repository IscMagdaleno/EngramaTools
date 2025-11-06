CREATE TABLE [dbo].[Test_Table] (
    [iIdTest_Table] INT           IDENTITY (1, 1) NOT NULL,
    [vchName]       VARCHAR (100) NOT NULL,
    [vchEmail]      VARCHAR (100) NOT NULL,
    [dtRegistered]  DATETIME      NOT NULL,
    CONSTRAINT [PK_Test_Table] PRIMARY KEY CLUSTERED ([iIdTest_Table] ASC)
);


GO

