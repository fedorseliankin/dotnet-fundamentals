IF (NOT EXISTS(SELECT * FROM dbo.Address))  
BEGIN  
   INSERT INTO [dbo].[Address]  VALUES ('some street', 'some city', null, null);
   INSERT INTO [dbo].[Address]  VALUES ('another street', 'another city', null, null);
   INSERT INTO [dbo].[Person]  VALUES ('Ivan', 'Ivanov');
   INSERT INTO [dbo].[Person]  VALUES ('John', 'Johnson');
   INSERT INTO [dbo].[Company]  VALUES ('Best Company', 1);
   INSERT INTO [dbo].[Company]  VALUES ('Worst Company', 2);
   INSERT INTO [dbo].[Company]  VALUES ('Some Company', 2);
   INSERT INTO [dbo].[Employee]  VALUES (1, 1, 'Some Company', 'director', 'Ivan Ivanov');
   INSERT INTO [dbo].[Employee]  VALUES (2, 2, 'Best Company', 'director', 'John Johnson');
END  