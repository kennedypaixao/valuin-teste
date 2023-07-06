CREATE TABLE [dbo].[Role] (
    [Id]             [dbo].[DT_ID] NOT NULL,
    [InternalRoleId] FLOAT (53)    NOT NULL,
    [IdParentRole]   [dbo].[DT_ID] NULL,
    [Name]           VARCHAR (50)  NOT NULL,
    [Icon]           VARCHAR (50)  NOT NULL,
    [Slug]           VARCHAR (50)  NOT NULL,
    [Hash]           VARCHAR (50)  NULL,
    [OrdMenu]        FLOAT (53)    NOT NULL,
    [Ativo]          BIT           NOT NULL,
    CONSTRAINT [PK_MenuSideBar] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MenuSideBar_MenuSideBar] FOREIGN KEY ([IdParentRole]) REFERENCES [dbo].[Role] ([Id])
);



