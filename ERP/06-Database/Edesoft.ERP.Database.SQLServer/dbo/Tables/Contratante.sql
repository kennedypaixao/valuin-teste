CREATE TABLE [dbo].[Contratante] (
    [Id]           [dbo].[DT_ID] NOT NULL,
    [IdCorporacao] [dbo].[DT_ID] NOT NULL,
    [Nome]         VARCHAR (100) NOT NULL,
    [Ativo]        BIT           NOT NULL,
    CONSTRAINT [PK_Contratante] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contratante_Corporacao] FOREIGN KEY ([IdCorporacao]) REFERENCES [dbo].[Corporacao] ([Id])
);

