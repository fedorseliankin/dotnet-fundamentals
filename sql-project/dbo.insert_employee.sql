CREATE PROCEDURE insertEmployee
    @EmployeeName NVARCHAR(50) = NULL, 
    @FirstName NVARCHAR(50)= NULL, 
    @LastName NVARCHAR(50)= NULL, 
    @CompanyName NVARCHAR(50), 
    @Position NVARCHAR(50)= NULL,
    @Street NVARCHAR(50),
    @City NVARCHAR(50)= NULL,
    @State NVARCHAR(50)= NULL,
    @ZipCode NVARCHAR(50)= NULL
AS
BEGIN
    IF (ISNULL(@EmployeeName, '') = '' AND ISNULL(@FirstName, '') = '' AND ISNULL(@LastName, '') = '')
    BEGIN
        RAISERROR('At least one of the name fields (EmployeeName, FirstName, LastName) must be provided and not be empty or contain only spaces.', 16, 1);
        RETURN;
    END
    IF (LEN(@CompanyName) > 20)
    BEGIN
        SET @CompanyName = LEFT(@CompanyName, 20);
    END
	INSERT INTO [dbo].Address (
		Street,
		City,
		State,
		ZipCode
	)
	VALUES (
		@Street,
		@City,
		@State,
		@ZipCode
	);
	DECLARE @AdressID int;
	SET @AdressID = SCOPE_IDENTITY();
	INSERT INTO [dbo].Person (
		FirstName,
		LastName
	)
	VALUES (
		@FirstName,
		@LastName
	);
	DECLARE @PersonID int;
	SET @PersonID = SCOPE_IDENTITY();
    INSERT INTO [dbo].Employee (
        EmployeeName, 
        CompanyName, 
        Position,
		PersonId,
		AdressId
    )
    VALUES (
        LTRIM(RTRIM(@EmployeeName)), 
        @CompanyName, 
        @Position,
		@PersonID,
		@AdressID
    );
END;