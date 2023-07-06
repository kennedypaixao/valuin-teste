CREATE TABLE [dbo].[Ativos] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Codigo]     VARCHAR (30)     NOT NULL,
    [Nome]       VARCHAR (200)    NOT NULL,
    [SetorId]    UNIQUEIDENTIFIER NOT NULL,
    [SubSetorId] UNIQUEIDENTIFIER NOT NULL,
    [Logotipo]   VARCHAR (MAX)    NULL,
    CONSTRAINT [PK_Ativos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AtivosSetor] FOREIGN KEY ([SetorId]) REFERENCES [dbo].[Setores] ([Id]),
    CONSTRAINT [FK_AtivosSubSetor] FOREIGN KEY ([SubSetorId]) REFERENCES [dbo].[SubSetores] ([Id])
);