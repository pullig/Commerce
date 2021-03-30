CREATE TABLE [dbo].[Product] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50)    NOT NULL,
    [Description]  VARCHAR (250)   NOT NULL,
    [Price]        DECIMAL (10, 2) NOT NULL,
    [CreationDate] DATETIME        NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

