IF NOT EXISTS (SELECT *
FROM sysobjects o
WHERE o.NAME = 'Roles')
	BEGIN

        CREATE TABLE Roles
        (
                iIdRoles INT NOT NULL IDENTITY(1, 1)
                        CONSTRAINT PK_Roles
			PRIMARY KEY (iIdRoles),

                vchName VARCHAR(50) NOT NULL,
                bStatus BIT NOT NULL,

        )
END;
GO

IF NOT EXISTS (SELECT *
FROM sysobjects o
WHERE o.NAME = 'Users')
	BEGIN

        CREATE TABLE Users
        (
                iIdUsers INT NOT NULL IDENTITY(1, 1)
                        CONSTRAINT PK_Users
			PRIMARY KEY (iIdUsers),

                iIdRoles INT NOT NULL
                        Constraint FK_Users_iIdRoles  Foreign Key (iIdRoles )
			References Roles(iIdRoles ),
                vchName VARCHAR(250) NOT NULL,
                vchEmail VARCHAR(150) NOT NULL,
                vchPass VARBINARY(32) NOT NULL,
                bStatus BIT NOT NULL
        )
END;

GO


IF NOT EXISTS (SELECT * FROM sysobjects o WHERE o.NAME = 'Catalogues ')
	BEGIN

		CREATE TABLE Catalogues (
			iIdCatalogues  INT NOT NULL IDENTITY(1, 1)
			CONSTRAINT PK_Catalogues 
			PRIMARY KEY (iIdCatalogues ),


			vchName  VARCHAR(250) NOT NULL,
			siInitialRange   SMALLINT NOT NULL,
			siFinalRange    SMALLINT NOT NULL,
			vchNote     VARCHAR (500)  NOT NULL,

)
       END



       
--Se tiene que agregar la tabla a la tabla de Catalogos

IF NOT EXISTS ( SELECT *
                                FROM
                                        sysobjects o
                                WHERE
                                  o.NAME = 'CatProyectType ' )
BEGIN
        CREATE TABLE CatProyectType (
                iIdCatProyectType        INT NOT NULL,
                        CONSTRAINT PK_CatProyectType  PRIMARY KEY (iIdCatProyectType        ),
                vchDescription VARCHAR( 250 ) NOT NULL,
                bStatus             BIT NOT NULL
        )
END;

GO

IF NOT EXISTS (SELECT * FROM sysobjects o WHERE o.NAME = 'CommonScripts')
	BEGIN

		CREATE TABLE CommonScripts(
			iIdCommonScripts INT NOT NULL IDENTITY(1, 1)
			CONSTRAINT PK_CommonScripts
			PRIMARY KEY (iIdCommonScripts),

			iIdCatProyectType INT NOT NULL
			Constraint FK_CommonScripts_iIdCatProyectType  Foreign Key (iIdCatProyectType )
			References CatProyectType(iIdCatProyectType ),  

			vchName VARCHAR(250) NOT NULL,
			vchDescription VARCHAR(500) NOT NULL,
			vchCode VARCHAR(MAX) NOT NULL,

)
       END