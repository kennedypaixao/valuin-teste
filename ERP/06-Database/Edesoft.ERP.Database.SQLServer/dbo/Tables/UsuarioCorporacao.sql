CREATE TABLE [dbo].[UsuarioCorporacao] (
    [IdUsuario]    UNIQUEIDENTIFIER NOT NULL,
    [IdCorporacao] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UsuarioCorporacao] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdCorporacao] ASC),
    CONSTRAINT [FK_UsuarioCorporacao_Corporacoes] FOREIGN KEY ([IdCorporacao]) REFERENCES [dbo].[Corporacoes] ([Id]),
    CONSTRAINT [FK_UsuarioCorporacao_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([Id])
);

