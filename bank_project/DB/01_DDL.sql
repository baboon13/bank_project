CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Account NVARCHAR(50) NOT NULL, -- 扣款帳號
    Password1 NVARCHAR(50) NOT NULL  -- 新增密碼欄位
);

CREATE TABLE Product (
    ProductID INT IDENTITY(1,1) PRIMARY KEY, -- 產品流水號
    ProductName NVARCHAR(100) NOT NULL,
    Price DECIMAL(18,2) ,
    FeeRate DECIMAL(5,2)  -- 手續費率(%) ex: 0.1 (10%) / 0.01 (1%)
);

CREATE TABLE LikeList (
    SN INT PRIMARY KEY IDENTITY(1,1), -- 流水序號
    OrderName NVARCHAR(50), 
    OrderNUM INT ,
    Account NVARCHAR(50) NOT NULL, -- 扣款帳號
    TotalFee INT, -- 總手續費用(台幣計價)
    TotalAmount INT, -- 預計扣款總金額
);
