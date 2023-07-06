CREATE TABLE [endereco].[Pais] (
    [Id]         [dbo].[DT_ID] NOT NULL,
    [Nome]       VARCHAR (50)  NOT NULL,
    [CodigoIBGE] INT           NOT NULL,
    CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED ([Id] ASC)
);

