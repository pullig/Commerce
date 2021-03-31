CREATE TABLE [dbo].[User] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Username]         VARCHAR (50) NOT NULL,
    [EmailAddress] VARCHAR (50) NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    [DisplayName]  VARCHAR (75) NOT NULL,
    [Password] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

