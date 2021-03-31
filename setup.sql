IF NOT EXISTS 
   (
     SELECT name FROM master.dbo.sysdatabases 
     WHERE name = 'Commerce'
    )
BEGIN
    CREATE DATABASE Commerce
END

GO
USE Commerce
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='User' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[User] (
		[Id]           INT          IDENTITY (1, 1) NOT NULL,
		[Username]         VARCHAR (50) NOT NULL,
		[EmailAddress] VARCHAR (50) NOT NULL,
		[CreationDate] DATETIME     NOT NULL,
		[DisplayName]  VARCHAR (75) NOT NULL,
		[Password] VARCHAR(MAX) NOT NULL, 
		CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
	)
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Product' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Product] (
		[Id]           INT             IDENTITY (1, 1) NOT NULL,
		[Name]         VARCHAR (50)    NOT NULL,
		[Description]  VARCHAR (250)   NOT NULL,
		[Price]        DECIMAL (10, 2) NOT NULL,
		[CreationDate] DATETIME        NOT NULL,
		CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
	)
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Order' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Order] (
		[Id]           INT      IDENTITY (1, 1) NOT NULL,
		[UserId]       INT      NOT NULL,
		[CreationDate] DATETIME NOT NULL,
		CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
		CONSTRAINT [FK_Order_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
	)
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ProductOrder' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[ProductOrder] (
		[Id]         INT      IDENTITY (1, 1) NOT NULL,
		[OrderId]    INT          NOT NULL,
		[ProductId]  INT          NOT NULL,
		[UnityPrice] DECIMAL (10, 2) NOT NULL,
		[Quantity]   INT          NOT NULL,
		CONSTRAINT [PK_ProductOrde] PRIMARY KEY CLUSTERED ([Id] ASC),
		CONSTRAINT [FK_ProductOrder_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
		CONSTRAINT [FK_ProductOrder_Product] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
	)
END