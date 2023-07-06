CREATE TABLE [dbo].[UsuarioPerfis] (
    [IdUsuario] [dbo].[DT_ID] NOT NULL,
    [IdPerfil]  [dbo].[DT_ID] NOT NULL,
    CONSTRAINT [PK_UsuarioPerfis] PRIMARY KEY CLUSTERED ([IdUsuario] ASC, [IdPerfil] ASC),
    CONSTRAINT [FK_UsuarioPerfis_Perfis] FOREIGN KEY ([IdPerfil]) REFERENCES [dbo].[Perfis] ([Id]),
    CONSTRAINT [FK_UsuarioPerfis_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([Id])
);

