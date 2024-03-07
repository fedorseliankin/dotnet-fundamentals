CREATE PROCEDURE [dbo].[DeleteOrders]
    @Month int = NULL,
    @Year int = NULL,
    @Status varchar(50) = NULL,
    @ProductID int = NULL
AS
BEGIN
    DELETE FROM [dbo].[Order]
    WHERE 
        (@Month IS NULL OR MONTH(CreatedDate) = @Month) AND
        (@Year IS NULL OR YEAR(CreatedDate) = @Year) AND
        (@Status IS NULL OR Status = @Status) AND
        (@ProductId IS NULL OR ProductID = @ProductID)
END