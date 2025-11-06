IF  NOT EXISTS( SELECT 1 from  Roles where iIdRoles = -1) 
BEGIN 
SET IDENTITY_INSERT Roles ON;

	INSERT INTO dbo.Roles
	 ( 

		iIdRoles, 			vchName, 			bStatus 	

	)
    VALUES
	( 
	 
		 -1 , 			 'N/A' , 			 0  	

	)
SET IDENTITY_INSERT Roles OFF;
END



IF  NOT EXISTS( SELECT 1 from  Users where iIdUsers = -1) 
BEGIN 
SET IDENTITY_INSERT Users ON;

		INSERT INTO dbo.Users
	 ( 

		iIdUsers, 			iIdRoles, 			vchName, 	
		vchEmail, 			vchPass, 			bStatus 	

	)
	VALUES 
	( 
	 
		 -1 , 			 -1 , 			 '' , 	
		 '' , 			 -1 , 			 0  	

	)

SET IDENTITY_INSERT Users OFF;
END


IF NOT EXISTS (SELECT 1 FROM Catalogues WHERE vchName = 'CatProyectType' )
BEGIN
INSERT INTO Catalogues
	(vchName, siInitialRange, siFinalRange,  vchNote)
	VALUES
	('CatProyectType', 0,99,'Proyect types to divide the scripts that we will to save on the database')
END

IF  ( 0<= 0) 
BEGIN 
	INSERT INTO dbo.CatProyectType
	 ( 

		iIdCatProyectType, vchDescription, 			bStatus 	
	)
	VALUES 
	( -1, '' , 0  ),
	( 1, 'BASE DATOS' , 1  ),
	( 2, 'BLAZOR' , 1  ),
	( 3, 'API' , 1  )

END