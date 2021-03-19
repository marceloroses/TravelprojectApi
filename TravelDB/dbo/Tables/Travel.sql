CREATE TABLE [dbo].[Travel] (
    [Id]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (250) NULL,
    CONSTRAINT [PK_Travel] PRIMARY KEY CLUSTERED ([Id] ASC)
);

