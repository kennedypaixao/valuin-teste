CREATE TABLE [dbo].[ContratanteModules] (
    [IdContratante] [dbo].[DT_ID] NOT NULL,
    [IdModule]      [dbo].[DT_ID] NOT NULL,
    [Ativo]         BIT           NOT NULL,
    CONSTRAINT [PK_ContratanteModules] PRIMARY KEY CLUSTERED ([IdContratante] ASC, [IdModule] ASC),
    CONSTRAINT [FK_ContratanteModules_Contratante] FOREIGN KEY ([IdContratante]) REFERENCES [dbo].[Contratante] ([Id]),
    CONSTRAINT [FK_ContratanteModules_Modules] FOREIGN KEY ([IdModule]) REFERENCES [dbo].[Modules] ([Id])
);

