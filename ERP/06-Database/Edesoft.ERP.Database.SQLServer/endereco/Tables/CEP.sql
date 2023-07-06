CREATE TABLE [endereco].[CEP] (
    [Id]         [dbo].[DT_ID] NOT NULL,
    [NrCep]      VARCHAR (8)   NOT NULL,
    [IdCidade]   [dbo].[DT_ID] NOT NULL,
    [Logradouro] VARCHAR (200) NOT NULL,
    [Bairro]     VARCHAR (100) NOT NULL,
    [CodigoIBGE] INT           NOT NULL,
    CONSTRAINT [PK_CEP] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CEP_Cidade] FOREIGN KEY ([IdCidade]) REFERENCES [endereco].[Cidade] ([Id])
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_CEP]
    ON [endereco].[CEP]([NrCep] ASC);

