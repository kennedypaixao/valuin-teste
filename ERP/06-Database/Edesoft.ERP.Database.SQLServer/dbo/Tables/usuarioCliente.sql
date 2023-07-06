CREATE TABLE [dbo].[UsuarioCliente] (
    [IdUsuario] UNIQUEIDENTIFIER NOT NULL,
    [IdCliente] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UsuarioCliente] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdCliente] ASC),
    CONSTRAINT [FK_UsuarioCliente_Cliente] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Clientes] ([Id]),
    CONSTRAINT [FK_UsuarioCliente_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([Id])
);

