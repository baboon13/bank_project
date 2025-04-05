-- 使用者表
CREATE TABLE [dbo].[UserData] (
    [UserID]    VARCHAR (20)  NOT NULL,
    [UserName]  NVARCHAR (50) NOT NULL,
    [Email]     NVARCHAR (50) NULL,
    [Account]   NVARCHAR (50) NOT NULL,
    [password1] NCHAR (10)    NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

-- 產品表
CREATE TABLE [dbo].[ProductData] (
    [No]          VARCHAR (20)    NOT NULL,
    [ProductName] VARCHAR (50)    NOT NULL,
    [Price]       DECIMAL (18, 2) NULL,
    [FeeRate]     DECIMAL (18, 2) NULL,
    PRIMARY KEY CLUSTERED ([No] ASC)
);

-- 喜好清單表
CREATE TABLE [dbo].[LikeListData] (
    [SN]          INT             NOT NULL,
    [OrderName]   NCHAR (10)      NOT NULL,
    [Account]     NVARCHAR (50)   NOT NULL,
    [TotalFee]    DECIMAL (18, 2) NULL,
    [TotalAmount] DECIMAL (18, 2) NULL,
    PRIMARY KEY CLUSTERED ([SN] ASC)
);