CREATE TABLE [dbo].[RoleModules] (
    [Id]       [dbo].[DT_ID] NOT NULL,
    [IdModule] [dbo].[DT_ID] NOT NULL,
    [IdRole]   [dbo].[DT_ID] NOT NULL,
    CONSTRAINT [PK_ModuloSistemaMenuSideBar_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ModuloSistemaMenuSideBar_MenuSideBar1] FOREIGN KEY ([IdRole]) REFERENCES [dbo].[Role] ([Id]),
    CONSTRAINT [FK_ModuloSistemaMenuSideBar_ModuloSistema] FOREIGN KEY ([IdModule]) REFERENCES [dbo].[Modules] ([Id])
);

