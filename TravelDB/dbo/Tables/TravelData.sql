CREATE TABLE [dbo].[TravelData] (
    [Id]            BIGINT             IDENTITY (1, 1) NOT NULL,
    [TravelId]      BIGINT             NULL,
    [PassengerName] VARCHAR (250)      NULL,
    [Birthdate]     DATETIMEOFFSET (7) NULL,
    [Passport]      VARCHAR (50)       NULL,
    [Email]         VARCHAR (250)      NULL,
    CONSTRAINT [PK_TravelData] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TravelData_Travel] FOREIGN KEY ([TravelId]) REFERENCES [dbo].[Travel] ([Id])
);

