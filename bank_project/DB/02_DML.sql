-- 初始使用者資料
INSERT INTO UserData VALUES 
('A1236456789', '王小明', 'test@email.com', '1111999666');
GO

-- 初始產品資料
INSERT INTO ProductData VALUES
('P001', '玉山基金', 5000, 0.015),
('P002', '科技ETF', 300, 0.01);
GO

-- 建立取得喜好清單的儲存過程
CREATE PROCEDURE sp_GetLikeLists
    @Account NVARCHAR(50)
AS
BEGIN
    SELECT 
        l.SN, p.[Product Name] AS ProductName, p.Price, p.[Fee rate] AS FeeRate,
        l.OrderName, l.TotalAmount, l.TotalFee, u.Email
    FROM LikeListData l
    JOIN ProductData p ON l.ProductNo = p.No
    JOIN UserData u ON l.Account = u.Account
    WHERE l.Account = @Account
END
GO

-- 建立新增喜好清單的儲存過程
CREATE PROCEDURE sp_AddLikeList
    @ProductNo VARCHAR(20),
    @OrderName INT,
    @Account NVARCHAR(50)
AS
BEGIN
    DECLARE @Price INT, @FeeRate DECIMAL(18,4)
    
    -- 取得產品價格和費率
    SELECT @Price = Price, @FeeRate = [Fee rate]
    FROM ProductData
    WHERE No = @ProductNo
    
    -- 計算總金額和手續費
    DECLARE @TotalAmount INT = @Price * @OrderName
    DECLARE @TotalFee INT = @TotalAmount * @FeeRate
    
    -- 插入喜好清單
    INSERT INTO LikeListData (OrderName, Account, TotalFee, TotalAmount, ProductNo)
    VALUES (@OrderName, @Account, @TotalFee, @TotalAmount, @ProductNo)
END
Go