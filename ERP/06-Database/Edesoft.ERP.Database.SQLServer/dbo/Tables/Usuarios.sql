CREATE TABLE [dbo].[Usuarios] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [IdContratante] [dbo].[DT_ID] NOT NULL,
    [Nome]  VARCHAR (100)    NOT NULL,
    [Email] VARCHAR (50)     NOT NULL,
    [Senha] VARCHAR (50)     NOT NULL,
    [Ativo] BIT              NOT NULL,
    CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Usuarios_Contratante] FOREIGN KEY ([IdContratante]) REFERENCES [dbo].[Contratante] ([Id])
);







