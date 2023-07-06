CREATE TABLE [dbo].[Perfis] (
    [Id]            [dbo].[DT_ID] NOT NULL,
    [IdContratante] [dbo].[DT_ID] NOT NULL,
    [Nome]          VARCHAR (100) NOT NULL,
    [Ativo]         BIT           NOT NULL,
    CONSTRAINT [PK_Perfis] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Perfis_Contratante] FOREIGN KEY ([IdContratante]) REFERENCES [dbo].[Contratante] ([Id])
);

