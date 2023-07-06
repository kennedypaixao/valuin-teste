CREATE TABLE [dbo].[SubSetores] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [SetorId] UNIQUEIDENTIFIER NOT NULL,
    [Nome]    VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_SubSetores] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubSetoresSetores] FOREIGN KEY ([SetorId]) REFERENCES [dbo].[Setores] ([Id])
);