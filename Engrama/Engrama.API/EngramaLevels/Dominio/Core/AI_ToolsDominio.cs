using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.API.EngramaLevels.Dominio.Servicios;
using Engrama.API.EngramaLevels.Dominio.Servicios.Models;
using Engrama.API.EngramaLevels.Infrastructure.Interfaces;
using Engrama.Share.Objetos.AI_Tools;
using Engrama.Share.PostClass.AI_Tools;

using EngramaCoreStandar.Mapper;

using EngramaCoreStandar.Results;

using Newtonsoft.Json;

using System.Text;

namespace Engrama.API.EngramaLevels.Dominio.Core
{
	public class AI_ToolsDominio : IAI_ToolsDominio
	{

		private readonly IDataBaseRepository dataBaseRepository;
		private readonly MapperHelper mapper;
		private readonly IResponseHelper responseHelper;

		/// <summary>
		/// Initialize the fields receiving the interfaces on the builder
		/// </summary>
		public AI_ToolsDominio(IDataBaseRepository dataBaseRepository,
			MapperHelper mapper,
			IResponseHelper responseHelper)
		{
			this.dataBaseRepository = dataBaseRepository;
			this.mapper = mapper;
			this.responseHelper = responseHelper;

		}




		public async Task<Response<ResultOpenAI>> CreateCodeByTables(PostCreateCodeByTables postModel)
		{

			var validation = new Response<ResultOpenAI>();

			var openIAService = new AzureIAService();
			var request = new RequestOpenAI()
			{
				Prompt = CretePromt(postModel),
				Configuration = CreateConfiguration()
			};


			var valor = await openIAService.CallAzureOpenIA(request);

			var FianlResul = new ResultOpenAI
			{
				ResultaAI = valor.Content[0].Text
			};
			validation.Data = FianlResul;
			validation.IsSuccess = true;

			return validation;
		}



		public async Task<Response<ResultOpenAI>> CreateNewTable(PostCreateNewTableIA postModel)
		{


			var validation = new Response<ResultOpenAI>();

			var openIAService = new AzureIAService();
			var request = new RequestOpenAI()
			{
				Prompt = CreatePromptNewTablea(postModel),
				Configuration = ConfigNewTable()
			};

			var valor = await openIAService.CallAzureOpenIA(request);

			var FianlResul = new ResultOpenAI
			{
				ResultaAI = valor.Content[0].Text
			};
			validation.Data = FianlResul;
			validation.IsSuccess = true;

			return validation;
		}

		public async Task<Response<ResultOpenAI>> ImproveSPwithAI(PostImproveSPwithAI postModel)
		{


			var validation = new Response<ResultOpenAI>();

			var openIAService = new AzureIAService();
			var request = new RequestOpenAI()
			{
				Prompt = CretePromptImproveSP(postModel),
				Configuration = CreateConfiguration()
			};

			var valor = await openIAService.CallAzureOpenIA(request);

			var FianlResul = new ResultOpenAI
			{
				ResultaAI = valor.Content[0].Text
			};
			validation.Data = FianlResul;
			validation.IsSuccess = true;

			return validation;
		}


		public async Task<Response<ResultOpenAI>> ImproveCSharpCodeAI(PostImproveCSharpCode postModel)
		{


			var validation = new Response<ResultOpenAI>();

			var openIAService = new AzureIAService();
			var request = new RequestOpenAI()
			{
				Prompt = PromptImproveCSharpCode(postModel),
				Configuration = ConfigImproveCSharpCode()
			};

			var valor = await openIAService.CallAzureOpenIA(request);

			var FianlResul = new ResultOpenAI
			{
				ResultaAI = valor.Content[0].Text
			};
			validation.Data = FianlResul;
			validation.IsSuccess = true;

			return validation;
		}



		private string CreateConfiguration()
		{
			var config = new StringBuilder();
			config.AppendLine("Eres un experto en SQL Server y desarrollo de bases de datos." +
			" Tu tarea es ayudarme a escribir código SQL limpio," +
			" estructurado y que siga buenas prácticas para " +
			"Microsoft SQL Server. Cada respuesta debe incluir:\r\n\r\n" +
			"1.- Código SQL ejecutable compatible con SQL Server.\r\n\r\n" +
			"2.- Uso de buenas prácticas, como nombres consistentes, chequeo de existencia (IF NOT EXISTS), uso de tipos de datos apropiados, y evitar ambigüedades.\r\n\r\n" +
			"3.- Estructura clara y legible, con indentación adecuada y orden lógico (ej. CREATE TABLE con CONSTRAINTS al final, etc.).\r\n\r\n" +
			"4.- Ligeros comentarios en el código, solo los necesarios para entender rápidamente qué hace cada bloque.\r\n\r\n" +
			"5.- Evita redundancias y código innecesario. Prioriza claridad, mantenimiento y ejecución segura.");

			config.AppendLine("En caso de solicitar un procedimiento almacenado hay que seguir las siguientes arquitecturas.");
			config.AppendLine("-------------------------------------------------");
			config.AppendLine(LoadGetProcedureStructure());
			config.AppendLine("-------------------------------------------------");
			config.AppendLine(LoadSaveProcedureStructure());
			return config.ToString();
		}


		private string LoadGetProcedureStructure()
		{
			// Ruta base donde se ejecuta el programa
			string basePath = AppDomain.CurrentDomain.BaseDirectory;

			// Ruta relativa a la carpeta Files
			string filesPath = Path.Combine(basePath, "../../../EngramaLevels\\Dominio");
			filesPath = Path.Combine(filesPath, "Files");

			// Leer los archivos
			string getSPPath = Path.Combine(filesPath, "GetSP.txt");

			if (File.Exists(getSPPath))
				return File.ReadAllText(getSPPath);

			return string.Empty;

		}


		private string LoadSaveProcedureStructure()
		{
			// Ruta base donde se ejecuta el programa
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			//C:\ReposEngrama\EngramaTools\Engrama\Engrama.API\EngramaLevels\Dominio\Files\GetSP.txt
			// Ruta relativa a la carpeta Files
			string filesPath = Path.Combine(basePath, "../../../EngramaLevels\\Dominio");
			filesPath = Path.Combine(filesPath, "Files");

			// Leer los archivos
			string saveSPPath = Path.Combine(filesPath, "SaveSP.txt");


			if (File.Exists(saveSPPath))
				return File.ReadAllText(saveSPPath);

			return string.Empty;
		}


		private string CretePromt(PostCreateCodeByTables postModel)
		{
			var promt = "Teniendo las siguientes tablas SQL con los campos correspondientes:\r\n\r\n";
			string json = JsonConvert.SerializeObject(postModel.LstTablas, Formatting.Indented);
			promt += json + "\r\n\r\n";
			promt += "Necesito que realice las siguientes instrucciones: \r\n\r\n";
			promt += $"[{postModel.Prompt}]";
			return promt;
		}





		private string ConfigNewTable()
		{
			return "Eres un asistente útil experto en SQL Server y ayudas al usuario a comentar y mejorar el código." +
										 "Necesito crear una nueva tabla en código SQL para ejecutar en SQL Server. " +
										 "Agrega los prefijos " +
										 "[i para INTEGER] " +
										 "[vch para VARCHAR] " +
										 "[nvch para NVARCHAR] " +
										 "[dt para DATETIME] " +
										 "[m para MONEY] " +
										 "[sm para SMALLINT] " +
										 "[bi para BIGINT] " +
										 "[fl para FLOAT] " +
										 "[d para DATE] " +
										 "[b para BIT] " +
										 "a los atributo de las tablas," +
										 "siempre crea el primer registro con el mismo nombre de la tabla como primery key y que sea  IDENTITY(1, 1) y que tenga como prefijo iId," +
										 "agrega  a todos los campos NOT NULL," +
										 "Si el usuario no te proporciona atributos, tu crea los necesario según el contexto." +
										 "La tabla tiene que tener nombre en singular.";


		}

		private string CreatePromptNewTablea(PostCreateNewTableIA postModel)
		{
			var promt = $"Crea las tablas a partir de las siguientes instrucciones: {postModel.PromptCreation}";
			return promt;
		}



		private string CretePromptImproveSP(PostImproveSPwithAI postModel)
		{
			string json = JsonConvert.SerializeObject(postModel.LstTablas, Formatting.Indented);


			var promt = $"Mejora el procedimiento almacenado con las siguientes instrucciones: {postModel.Prompt} " +
			$"El procedimiento en cuestión es [{postModel.StoredProcedure.Details.Code}] " +
			$"Si el usuario se refiere a tablas, toma como referencia esta siguientes [{json}]";
			return promt;
		}

		private string ConfigImproveCSharpCode()
		{
			var promt = $"Eres un experto en C# con conocimientos avanzados en buenas prácticas, rendimiento, legibilidad y" +
			$" mantenimiento de código.\r\n\r\n" +
			$"Quiero que actúes como revisor y optimizar de métodos C#, siguiendo los siguientes pasos en cada respuesta:" +
			$"\r\n\r\nAnaliza el código que te voy a dar.\r\n" +
			$"Describe brevemente cuál es su funcionalidad y su propósito.\r\n\r\n" +
			$"Optimiza el método, manteniendo exactamente la misma funcionalidad, pero:\r\n\r\n" +
			$"Mejorando el rendimiento si es posible.\r\n\r\n" +
			$"Mejorando la legibilidad y limpieza del código.\r\n\r\n" +
			$"Aplicando buenas prácticas de C# modernas (nombres claros, uso eficiente de LINQ, async si aplica, etc.).\r\n\r\n" +
			$"Comenta los cambios realizados y explica por qué son una mejora.\r\n\r\n" +
			$"Devuélveme:\r\n\r\n" +
			$"El código optimizado, en un bloque ```csharp.\r\n\r\n" +
			$"Una explicación clara y puntual de los cambios.\r\n\r\n" +
			$"No cambies la lógica del negocio, solo mejora el código.";
			return promt;
		}

		private string PromptImproveCSharpCode(PostImproveCSharpCode postModel)
		{
			var promt = $"Mejora el código C#  con las siguientes instrucciones: {postModel.Prompt} " +
			$"El código en C# en cuestión es [{postModel.CSharpCode}]";
			return promt;
		}
	}

}


