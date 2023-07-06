CREATE TABLE [dbo].[Corporacao] (
    [Id]    [dbo].[DT_ID] NOT NULL,
    [Nome]  VARCHAR (100) NOT NULL,
    [Ativo] BIT           NOT NULL,
    CONSTRAINT [PK_Corporacao] PRIMARY KEY CLUSTERED ([Id] ASC)
);

