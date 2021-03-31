CREATE TABLE [dbo].[ProductOrder] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [OrderId]    INT          NOT NULL,
    [ProductId]  INT          NOT NULL,
    [UnityPrice] DECIMAL (10, 2) NOT NULL,
    [Quantity]   INT          NOT NULL,
    CONSTRAINT [PK_ProductOrde] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductOrder_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_ProductOrder_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

