CREATE TABLE [dbo].[Corporacoes] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Nome] VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_Corporacoes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

