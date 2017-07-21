
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/13/2016 17:37:57
-- Generated from EDMX file: C:\Users\user\documents\visual studio 2015\Projects\myshop\myshop\Models\Carts..edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Carts];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MyorderMyorderDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MyorderDetails] DROP CONSTRAINT [FK_MyorderMyorderDetail];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Myorders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Myorders];
GO
IF OBJECT_ID(N'[dbo].[MyorderDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyorderDetails];
GO
IF OBJECT_ID(N'[dbo].[ProductComments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductComments];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [商品編號] int IDENTITY(1,1) NOT NULL,
    [商品名稱] nvarchar(100)  NOT NULL,
    [商品類別] nvarchar(max)  NULL,
    [類別編號] int  NULL,
    [製造日期] datetime  NOT NULL,
    [商品描述] nvarchar(max)  NULL,
    [價格] decimal(18,0)  NOT NULL,
    [是否庫存] bit  NOT NULL,
    [商品圖] nvarchar(max)  NULL
);
GO

-- Creating table 'Myorders'
CREATE TABLE [dbo].[Myorders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [RecieverName] nvarchar(max)  NOT NULL,
    [RecieverPhone] nvarchar(max)  NOT NULL,
    [RecieverAddress] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MyorderDetails'
CREATE TABLE [dbo].[MyorderDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MyorderId] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [price] decimal(18,0)  NOT NULL,
    [stuff] int  NOT NULL
);
GO

-- Creating table 'ProductComments'
CREATE TABLE [dbo].[ProductComments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [會員編號] nvarchar(max)  NOT NULL,
    [留言內容] nvarchar(max)  NOT NULL,
    [留言時間] datetime  NOT NULL,
    [留言產品編號] int  NOT NULL,
    [會員名稱] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [商品編號] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([商品編號] ASC);
GO

-- Creating primary key on [Id] in table 'Myorders'
ALTER TABLE [dbo].[Myorders]
ADD CONSTRAINT [PK_Myorders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MyorderDetails'
ALTER TABLE [dbo].[MyorderDetails]
ADD CONSTRAINT [PK_MyorderDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductComments'
ALTER TABLE [dbo].[ProductComments]
ADD CONSTRAINT [PK_ProductComments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MyorderId] in table 'MyorderDetails'
ALTER TABLE [dbo].[MyorderDetails]
ADD CONSTRAINT [FK_MyorderMyorderDetail]
    FOREIGN KEY ([MyorderId])
    REFERENCES [dbo].[Myorders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MyorderMyorderDetail'
CREATE INDEX [IX_FK_MyorderMyorderDetail]
ON [dbo].[MyorderDetails]
    ([MyorderId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------