CREATE TABLE [dbo].[DettaglioListinoSet] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [Id_listino]  INT NULL,
    [Id_prodotto] INT NULL,
    [quantita]    INT NULL,
    CONSTRAINT [PK_DettaglioListinoSet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_ListinoProdotto] UNIQUE NONCLUSTERED ([Id_listino] ASC, [Id_prodotto] ASC),
    CONSTRAINT [FK_Prodotto] FOREIGN KEY ([Id_prodotto]) REFERENCES [dbo].[ProdottoSet] ([Id]),
    CONSTRAINT [FK_Listino] FOREIGN KEY ([Id_listino]) REFERENCES [dbo].[ListinoSet] ([Id])
);

