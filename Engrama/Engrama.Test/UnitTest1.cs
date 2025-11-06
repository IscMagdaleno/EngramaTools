using Engrama.Share.Objetos.AnalyzeCodeArea;

using EngramaCoreStandar.Extensions;

namespace Engrama.Test
{
	public class CodeAfterAnalyzedTests
	{
		[Fact]
		public void SetData_ValidInput_SetsAllPropertiesCorrectly()
		{
			// Arrange
			var codeAfterAnalyzed = new CodeAfterAnalyzed();
			var vchCode = "Title";
			var vchCodeAnalyzed = TextoPrueba();

			// Act
			codeAfterAnalyzed.SetData(vchCode, vchCodeAnalyzed);

			// Assert
			Assert.Equal("Title", codeAfterAnalyzed.vchTitle);
			Assert.True(codeAfterAnalyzed.vchDescriptinos.NotEmpty());
			Assert.True(codeAfterAnalyzed.vchCodeImproved.NotEmpty());
			Assert.True(codeAfterAnalyzed.vchImprovements.NotEmpty());
		}


		private string TextoPrueba()
		{
			return "Claro, aquí tienes el código SQL para crear una tabla de usuarios siguiendo tus instrucciones." +
			" El nombre de la tabla será Usuario (en singular), se aplican los prefijos solicitados," +
			" el campo ID es iIdUsuario, es PRIMARY KEY e IDENTITY(1,1), y todos los campos son NOT NULL." +
			" Asumo atributos comunes para una tabla de usuarios:\r\n\r\n" +
			"```sql\r\nCREATE TABLE Usuario (\r\n    iIdUsuario         INT IDENTITY(1,1) NOT NULL PRIMARY KEY," +
			"  -- Llave primaria, autoincremental\r\n    nvchNombre         NVARCHAR(100) NOT NULL,  " +
			"                -- Nombre del usuario\r\n    nvchApellido       NVARCHAR(100) NOT NULL,   " +
			"               -- Apellido del usuario\r\n    vchEmail           VARCHAR(200) NOT NULL,  " +
			"                 -- Email del usuario\r\n    vchPassword        VARCHAR(255) NOT NULL,  " +
			"                 -- Contraseña (encriptada idealmente)\r\n    dtFechaRegistro    DATETIME NOT NULL, " +
			"                      -- Fecha de registro\r\n    bActivo            BIT NOT NULL            " +
			"                 -- Indica si el usuario está activo\r\n);\r\n```\r\n\r\n**Comentarios y mejoras:**\r\n\r\n" +
			"- Se usaron tipos que se ajustan a los campos e incluyen los prefijos requeridos.\r\n" +
			"- Todos los campos tienen NOT NULL como pediste.\r\n- Se sugiere almacenar las contraseñas" +
			" con hash.\r\n- Puedes agregar más campos según tus necesidades, por ejemplo: teléfono, " +
			"fecha de último acceso, etc.\r\n\r\n¿Quieres agregar algún campo más concreto?";
		}
	}
}

