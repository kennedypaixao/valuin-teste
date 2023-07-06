CREATE TABLE [dbo].[Modules] (
    [Id]             [dbo].[DT_ID] NOT NULL,
    [InternalRoleId] FLOAT (53)    NOT NULL,
    [IdParentModule] [dbo].[DT_ID] NULL,
    [Nome]           VARCHAR (50)  NOT NULL,
    [Icon]           VARCHAR (50)  NOT NULL,
    [Slug]           VARCHAR (50)  NOT NULL,
    [OrdMenu]        FLOAT (53)    NOT NULL,
    [Ativo]          BIT           CONSTRAINT [DF__ModuloSis__Ativo__31D75E8D] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ModuloSistema] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ModuloSistema_ModuloSistema] FOREIGN KEY ([IdParentModule]) REFERENCES [dbo].[Modules] ([Id])
);



