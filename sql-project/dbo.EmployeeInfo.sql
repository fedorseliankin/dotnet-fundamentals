CREATE VIEW EmployeeInfo AS
SELECT 
    E.Id as EmployeeId,
    COALESCE(E.EmployeeName, CONCAT(P.FirstName, ' ', P.LastName)) as EmployeeFullName,
    CONCAT(A.ZipCode, '_', A.State, ', ', A.City, '-', A.Street) as EmployeeFullAddress,
    CONCAT(C.Name, '(', E.Position, ')') as EmployeeCompanyInfo
FROM 
    [dbo].[Employee] E
JOIN 
    [dbo].[Company] C ON C.Name = E.CompanyName
JOIN [dbo].[Address] A ON A.Id = E.Id
JOIN [dbo].[Person] P ON P.Id = E.PersonId
ORDER BY 
    C.Name, A.City