CREATE PROCEDURE [dbo].[FetchOrders]
    @Month INT = NULL,
    @Year INT = NULL,
    @Status NVARCHAR(50) = NULL,
    @ProductId INT = NULL
AS
    SELECT * 
    FROM [dbo].[Order]
    WHERE (@Month IS NULL OR MONTH(CreatedDate) = @Month)
        AND (@Year IS NULL OR YEAR(CreatedDate) = @Year)
        AND (@Status IS NULL OR Status = @Status)
        AND (@ProductID IS NULL OR ProductID = @ProductID)
RETURN 0;