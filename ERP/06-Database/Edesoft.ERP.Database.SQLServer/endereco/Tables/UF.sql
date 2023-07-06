CREATE TABLE [endereco].[UF] (
    [Id]         [dbo].[DT_ID] NOT NULL,
    [IdPais]     [dbo].[DT_ID] NOT NULL,
    [Sigla]      VARCHAR (2)   NOT NULL,
    [Nome]       VARCHAR (50)  NOT NULL,
    [CodigoIBGE] INT           NOT NULL,
    CONSTRAINT [PK_UF] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UF_Pais1] FOREIGN KEY ([IdPais]) REFERENCES [endereco].[Pais] ([Id])
);

