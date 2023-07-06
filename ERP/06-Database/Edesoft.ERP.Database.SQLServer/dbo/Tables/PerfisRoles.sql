CREATE TABLE [dbo].[PerfisRoles] (
    [IdPerfil]  [dbo].[DT_ID] NOT NULL,
    [IdRole]    [dbo].[DT_ID] NOT NULL,
    [CanPost]   BIT           NOT NULL,
    [CanPut]    BIT           NOT NULL,
    [CanDelete] BIT           NOT NULL,
    [CanGet]    BIT           NOT NULL,
    CONSTRAINT [PK_PerfisRoles] PRIMARY KEY CLUSTERED ([IdPerfil] ASC, [IdRole] ASC),
    CONSTRAINT [FK_PerfisRoles_Perfis] FOREIGN KEY ([IdPerfil]) REFERENCES [dbo].[Perfis] ([Id]),
    CONSTRAINT [FK_PerfisRoles_Role] FOREIGN KEY ([IdRole]) REFERENCES [dbo].[Role] ([Id])
);

