CREATE TRIGGER trg_InsertEmployee
ON Employee
FOR INSERT
AS
BEGIN
    INSERT INTO Company (Name, AdressID)
    SELECT i.CompanyName, i.AdressId
    FROM inserted i
END;