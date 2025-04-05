INSERT INTO [UserData] (UserID, UserName, Email, Account ,password1)
VALUES
('U001', 'John Smith', 'ming@example.com', 'ACC12345678','123'),
('U002', 'Emily Johnson', 'tong@example.com', 'ACC23456789','123'),
('U003', 'Michael Brown', 'mei@example.com', 'ACC34567890','123'),
('U004', 'Sarah Davis', 'hua@example.com', 'ACC45678901','123'),
('U005', 'David Wilson', 'wei@example.com', 'ACC56789012','123'),
('U006', 'Jennifer Lee', 'yu@example.com', 'ACC67890123','123'),
('U007', 'Robert Taylor', 'zhong@example.com', 'ACC78901234','123'),
('U008', 'Jessica Clark', 'fen@example.com', 'ACC89012345','123'),
('U009', 'Thomas Martinez', 'wen@example.com', 'ACC90123456','123'),
('U010', 'Amanda Rodriguez', 'lan@example.com', 'ACC01234567','123');
INSERT INTO ProductData ("No", ProductName, Price, FeeRate)
VALUES
('P001', 'Global Equity Fund', 10000, 0.01),
('P002', 'Tech Growth ETF', 5000, 0.015),
('P003', 'Bond Plus', 8000, 0.02),
('P004', 'Emerging Markets', 12000, 0.025),
('P005', 'Sustainable Energy', 7000, 0.03),
('P006', 'Healthcare Innovators', 6000, 0.01),
('P007', 'Dividend Champions', 9000, 0.015),
('P008', 'Real Estate Trust', 3000, 0.02),
('P009', 'Small Cap Opportunities', 10000, 0.01),
('P010', 'Commodity Tracker', 15000, 0.025);
INSERT INTO LikeListData (SN, OrderName, Account, TotalFee, TotalAmount)
VALUES
(1, '5 unit', 'ACC12345678', 50, 5000),
(2, '3 unit', 'ACC23456789', 22.5, 1500),
(3, '2 unit', 'ACC34567890', 32, 1600),
(4, '10 unit', 'ACC45678901', 150, 7500),
(5, '1 unit', 'ACC56789012', 12, 1200),
(6, '8 unit', 'ACC67890123', 96, 4800),
(7, '4 unit', 'ACC78901234', 36, 3600),
(8, '7 unit', 'ACC89012345', 105, 5250),
(9, '6 unit', 'ACC90123456', 90, 9000),
(10, '2 unit', 'ACC01234567', 30, 3000);


ALTER TABLE ProductData ALTER COLUMN FeeRate DECIMAL(18,2);
ALTER TABLE ProductData ALTER COLUMN Price DECIMAL(18,2);

ALTER TABLE LikeListData ALTER COLUMN TotalFee DECIMAL(18,2);
ALTER TABLE LikeListData ALTER COLUMN TotalAmount DECIMAL(18,2);