CREATE TABLE [dbo].[Clientes] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [Nome]  VARCHAR (100)    NOT NULL,
    [Email] VARCHAR (50)     NOT NULL,
    [Ativo] BIT              NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cliente_Usuarios] FOREIGN KEY ([Id]) REFERENCES [dbo].[Usuarios] ([Id]) ON DELETE CASCADE
);





