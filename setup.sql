CREATE DATABASE Commerce;
GO

USE Commerce;
GO

CREATE TABLE [dbo].[User] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50) NOT NULL,
    [EmailAddress] VARCHAR (50) NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    [DisplayName]  VARCHAR (75) NOT NULL,
    [Password] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Product] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50)    NOT NULL,
    [Description]  VARCHAR (250)   NOT NULL,
    [Price]        DECIMAL (10, 2) NOT NULL,
    [CreationDate] DATETIME        NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);



CREATE TABLE [dbo].[Order] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [UserId]       INT      NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

CREATE TABLE [dbo].[ProductOrder] (
    [OrderId]    INT          NOT NULL,
    [ProductId]  INT          NOT NULL,
    [UnityPrice] DECIMAL (10, 2) NOT NULL,
    [Quantity]   INT          NOT NULL,
    CONSTRAINT [FK_ProductOrder_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_ProductOrder_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

