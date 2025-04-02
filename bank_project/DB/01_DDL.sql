-- 使用者表
CREATE TABLE UserData (
    UserID VARCHAR(20) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Account NVARCHAR(50) NOT NULL
);

-- 產品表
CREATE TABLE ProductData (
    No VARCHAR(20) PRIMARY KEY,
    [Product Name] VARCHAR(50) NOT NULL,
    Price INT NOT NULL,
    [Fee rate] DECIMAL(18,4) NOT NULL
);

-- 喜好清單表
CREATE TABLE LikeListData (
    SN INT IDENTITY(1,1) PRIMARY KEY,
    OrderName INT NOT NULL,
    Account NVARCHAR(50) NOT NULL,
    TotalFee INT NULL,
    TotalAmount INT NULL,
    ProductNo VARCHAR(20) NOT NULL,
    FOREIGN KEY (Account) REFERENCES UserData(Account),
    FOREIGN KEY (ProductNo) REFERENCES ProductData(No)
);