CREATE TABLE [dbo].[ProductOrder] (
    [OrderId]    INT          NOT NULL,
    [ProductId]  INT          NOT NULL,
    [UnityPrice] DECIMAL (10, 2) NOT NULL,
    [Quantity]   INT          NOT NULL,
    CONSTRAINT [FK_ProductOrder_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_ProductOrder_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

