
IF NOT EXISTS (SELECT * FROM sysobjects o WHERE o.NAME = 'ED_Imagenes')
	BEGIN

		CREATE TABLE ED_Imagenes(
			iIdED_Imagenes INT NOT NULL IDENTITY(1, 1)
			CONSTRAINT PK_ED_Imagenes
			PRIMARY KEY (iIdED_Imagenes),

			vchImagen VARCHAR(500) NOT NULL,
			vchDescripcion VARCHAR(500) NOT NULL,
			vchDescripcionIngles VARCHAR(500) NOT NULL,
			vchAlt VARCHAR(250) NOT NULL

)
       END


IF NOT EXISTS (SELECT * FROM sysobjects o WHERE o.NAME = 'ED_Proceso')
	BEGIN

		CREATE TABLE ED_Proceso(
			iIdED_Proceso INT NOT NULL IDENTITY(1, 1)
			CONSTRAINT PK_ED_Proceso
			PRIMARY KEY (iIdED_Proceso),

			vchTitle VARCHAR(50) NOT NULL,
			vchDescripcion VARCHAR(500) NOT NULL,
			vchDescripcionIngles VARCHAR(500) NOT NULL,
			iIdED_Imagenes INT NOT NULL
			Constraint FK_ED_Proceso_iIdED_Imagenes  Foreign Key (iIdED_Imagenes )
			References ED_Imagenes(iIdED_Imagenes )
			)
       END


	   

IF NOT EXISTS (SELECT * FROM sysobjects o WHERE o.NAME = 'ED_Descripcion')
	BEGIN

		CREATE TABLE ED_Descripcion(
			iIdED_Descripcion INT NOT NULL IDENTITY(1, 1)
			CONSTRAINT PK_ED_Descripcion
			PRIMARY KEY (iIdED_Descripcion),

			iIdED_Proceso INT NOT NULL
			Constraint FK_ED_Descripcion_iIdED_Proceso  Foreign Key (iIdED_Proceso )
			References ED_Proceso(iIdED_Proceso ),

			vchDescripcion VARCHAR(500) NOT NULL,
			vchDescripcionIngles VARCHAR(500) NOT NULL,

			iIdED_Imagenes INT NOT NULL
			Constraint FK_ED_Descripcion_iIdED_Imagenes  Foreign Key (iIdED_Imagenes )
			References ED_Imagenes(iIdED_Imagenes )

)
       END
