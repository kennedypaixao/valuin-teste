CREATE TABLE [endereco].[Cidade] (
    [Id]         [dbo].[DT_ID] NOT NULL,
    [IdUF]       [dbo].[DT_ID] NOT NULL,
    [Nome]       VARCHAR (50)  NOT NULL,
    [CodigoIBGE] INT           NOT NULL,
    CONSTRAINT [PK_Cidade] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cidade_UF] FOREIGN KEY ([IdUF]) REFERENCES [endereco].[UF] ([Id])
);



