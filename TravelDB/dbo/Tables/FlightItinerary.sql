CREATE TABLE [dbo].[FlightItinerary] (
    [Id]                 BIGINT             IDENTITY (1, 1) NOT NULL,
    [TravelDataId]       BIGINT             NULL,
    [OriginAirpot]       VARCHAR (250)      NULL,
    [DestinationAirport] VARCHAR (250)      NULL,
    [DateDeparture]      DATETIMEOFFSET (7) NULL,
    [DateArraival]       DATETIMEOFFSET (7) NULL,
    [FlightCode]         VARCHAR (50)       NULL,
    [Aeroline]           VARCHAR (250)      NULL,
    CONSTRAINT [PK_FlightItinerary] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FlightItinerary_TravelData] FOREIGN KEY ([TravelDataId]) REFERENCES [dbo].[TravelData] ([Id])
);

