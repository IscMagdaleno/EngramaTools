using Engrama.API.EngramaLevels.Dominio.BusinessLogic;
using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.API.EngramaLevels.Infrastructure.Interfaces;
using Engrama.Share.Entity.DataBase;
using Engrama.Share.Helpers;
using Engrama.Share.Objetos.DataBaseArea;
using Engrama.Share.PostClass;
using Engrama.Share.PostClass.DataBase;

using EngramaCoreStandar.Extensions;
using EngramaCoreStandar.Mapper;
using EngramaCoreStandar.Results;


namespace Engrama.API.EngramaLevels.Dominio.Core
{
	public class DataBaseDominio : IDataBaseDominio
	{
		private readonly IDataBaseRepository dataBaseRepository;
		private readonly MapperHelper mapper;
		private readonly IResponseHelper responseHelper;

		/// <summary>
		/// Initialize the fields receiving the interfaces on the builder
		/// </summary>
		public DataBaseDominio(IDataBaseRepository dataBaseRepository,
			MapperHelper mapper,
			IResponseHelper responseHelper)
		{
			this.dataBaseRepository = dataBaseRepository;
			this.mapper = mapper;
			this.responseHelper = responseHelper;
		}


		public async Task<Response<ConnectionString>> ProcesoConnectionString(PostSaveConnectionString DAOmodel)
		{
			try
			{

				var validation = new Response<ConnectionString>();

				var postModel = new PostGetTables
				{
					ConnectionString = DAOmodel.vchConnectionString
				};
				var result = await GetTables(postModel);

				if (result.IsSuccess)
				{
					validation = await SaveConnectionString(DAOmodel);

				}
				else
				{
					validation.IsSuccess = false;
					validation.Message = "No se pudo conectar a la base de datos";
					validation.Data = new ConnectionString();
				}


				return validation;
			}
			catch (Exception ex)
			{
				return Response<ConnectionString>.BadResult(ex.Message, new ConnectionString());
			}
		}


		public async Task<Response<ConnectionString>> SaveConnectionString(PostSaveConnectionString DAOmodel)
		{
			try
			{



				var model = mapper.Get<PostSaveConnectionString, spSaveConnectionString.Request>(DAOmodel);

				var result = await dataBaseRepository.spSaveConnectionString(model);
				var validation = responseHelper.Validacion<spSaveConnectionString.Result, ConnectionString>(result);

				if (validation.IsSuccess)
				{
					var tmpModel = validation.Data;
					validation.Data = mapper.Get<PostSaveConnectionString, ConnectionString>(DAOmodel);
					validation.Data.iIdConnectionString = tmpModel.iIdConnectionString;
				}

				return validation;
			}
			catch (Exception ex)
			{
				return Response<ConnectionString>.BadResult(ex.Message, new ConnectionString());
			}
		}

		public async Task<Response<IEnumerable<ConnectionString>>> GetConnectionString(PostGetConnectionString DAOmodel)
		{
			try
			{
				var model = mapper.Get<PostGetConnectionString, spGetConnectionString.Request>(DAOmodel);

				var result = await dataBaseRepository.spGetConnectionString(model);
				var validation = responseHelper.Validacion<spGetConnectionString.Result, ConnectionString>(result);
				if (validation.IsSuccess)
				{
					validation.Data = validation.Data;
				}
				return validation;

			}
			catch (Exception ex)
			{
				return Response<IEnumerable<ConnectionString>>.BadResult(ex.Message, new List<ConnectionString>());
			}
		}

		public async Task<Response<IEnumerable<Table>>> GetTables(PostGetTables DAOmodel)
		{
			try
			{
				string scrypt = ScriptQueryBD.GetAllTables.Replace("@vchTabla", $"''");

				var result = await dataBaseRepository.sqGetTables(scrypt, DAOmodel.ConnectionString);
				var response = responseHelper.Validacion<sqGetTables.Result, Table>(result);
				if (response.IsSuccess)
				{
					var factory = new FactoryTable(mapper);
					var lstTablas = factory.TablasFromList(result);

					response.Data = lstTablas;
				}

				return response;

			}
			catch (Exception ex)
			{
				return Response<IEnumerable<Table>>.BadResult(ex.Message, new List<Table>());
			}
		}


		public async Task<Response<IEnumerable<StoredProcedure>>> GetAllStoredProcedures(PostGetAllStoredProcedures DAOmodel)
		{
			try
			{
				string scrypt = ScriptQueryBD.GetStoredProcedures;

				var result = await dataBaseRepository.sqGetAllStoredProcedures(scrypt, DAOmodel.ConnectionString);
				var response = responseHelper.Validacion<sqGetAllStoredProcedures.Result, StoredProcedure>(result);



				return response;
			}
			catch (Exception ex)
			{
				return Response<IEnumerable<StoredProcedure>>.BadResult(ex.Message, new List<StoredProcedure>());
			}
		}

		public async Task<Response<DetailsStoreProcedure>> GetStoreProcedure(PostGetStoreProcedure DAOmodel)
		{
			try
			{
				var response = new Response<DetailsStoreProcedure>();

				var factory = new FactoryStoredProcedure();

				var model = new sp_helptext.Request { objname = DAOmodel.vchName };
				var CodigoSP = await dataBaseRepository.sqGetStoreProcedure(model, DAOmodel.ConnectionString);

				var details = factory.GetDetailsStoredProcedure(CodigoSP, DAOmodel.vchName);

				response.Data = details;
				response.IsSuccess = true;

				return response;
			}
			catch (Exception ex)
			{
				return Response<DetailsStoreProcedure>.BadResult(ex.Message, new());
			}
		}



		public async Task<Response<IList<StoredProcedure>>> GetAllStoredProcedureDetails(PostGetAllStoredProcedureDetails DAOmodel)
		{
			try
			{
				// Initialize the response object that will be returned
				var response = new Response<IList<StoredProcedure>>
				{
					Data = new List<StoredProcedure>()  // Ensure the Data property is initialized
				};

				// Create a request model for retrieving all stored procedures
				var postModelGetAllStoredProcedures = new PostGetAllStoredProcedures()
				{
					ConnectionString = DAOmodel.ConnectionString
				};

				// Retrieve all stored procedure names
				var result = await GetAllStoredProcedures(postModelGetAllStoredProcedures);

				if (result.IsSuccess)
				{
					// Process each stored procedure name retrieved
					foreach (var item in result.Data)
					{
						// Retrieve details for each stored procedure
						var postModelGetStoreProcedure = new PostGetStoreProcedure()
						{
							ConnectionString = DAOmodel.ConnectionString,
							vchName = item.vchName
						};
						var resultSP = await GetStoreProcedure(postModelGetStoreProcedure);
						// If successful, add the details to the response
						if (resultSP.IsSuccess)
						{
							item.Details = resultSP.Data;
						}
					}
					response.Data = result.Data.ToList();
					// Mark the response as successful if any stored procedure details were added
					response.IsSuccess = response.Data.Count > 0;
				}

				return response;
			}
			catch (Exception ex)
			{
				// Return a bad result with exception details in case of errors
				return Response<IList<StoredProcedure>>.BadResult(ex.Message, new List<StoredProcedure>());
			}
		}



		//public async Task<Response<CodeAnalyze>> AnalyzeSP(PostAnalyzeSP postModel)
		//{
		//	// Initialize the OpenAI client with the API key
		//	using var api = new OpenAIClient("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");

		//	// Extract C# code content from the posted model
		//	string sqlContent = postModel.vchStoreProcedure;

		//	// Prepare messages to be sent to the OpenAI API for code analysis
		//	var messages = CreateMessages(sqlContent);

		//	// Create a chat request using the prepared messages
		//	var chatRequest = new ChatRequest(messages);

		//	// Get the response for the chat request asynchronously
		//	var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);

		//	var choice = response.FirstChoice;

		//	// Create a response object to store the analyzed code details
		//	var respuesta = CreateResponse(postModel, choice);

		//	// Log the response details for debugging purposes
		//	//Console.WriteLine($"[{choice.Index}] {choice.Message.Role}: {choice.Message} | Finish Reason: {choice.FinishReason}");

		//	// Return the successful response
		//	return respuesta;
		//}

		//// Create message list for the chat request
		//private static List<Message> CreateMessages(string sqlContent)
		//{
		//	return new List<Message>
		//	{
		//		// System message setting the capabilities and constraints of the assistant
		//		new Message(Role.System, "You are a helpful assistant expert in SQL Server and help the user to comment and improve the code." +
		//								 "Don't change the names of the parameters or variables." +
		//								 "Always add comments at the code"),
		//		// User's input SQL code encapsulated in a message
		//		new Message(Role.User, "There is my SQL code.  [*" + sqlContent + "*]"),
		//	};
		//}

		//private static Response<CodeAnalyze> CreateResponse(PostAnalyzeSP postModel, Choice choice)
		//{
		//	var CodeAnalyze = new CodeAnalyze
		//	{
		//		vchCode = postModel.vchStoreProcedure,
		//		vchCodeAnalized = choice.Message
		//	};
		//	CodeAnalyze.FillDetails();

		//	return new Response<CodeAnalyze>
		//	{
		//		IsSuccess = true,
		//		Data = CodeAnalyze

		//	};
		//}


		//public async Task<Response<CodeByIA>> CreateNewTable(PostCreateNewTableIA postModel)
		//{
		//	using var api = new OpenAIClient("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");


		//	var messages = CreateMessajeNewTable(postModel.PromptCreation);

		//	var chatRequest = new ChatRequest(messages);

		//	var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);

		//	var choice = response.FirstChoice;

		//	var respuesta = new CodeByIA()
		//	{
		//		Code = choice.Message,
		//	};

		//	var finalResult = new Response<CodeByIA>
		//	{
		//		IsSuccess = true,
		//		Data = respuesta
		//	};

		//	return finalResult;
		//}



		//private static List<Message> CreateMessajeNewTable(string prmpt)
		//{
		//	return new List<Message>
		//	{
		//		// System message setting the capabilities and constraints of the assistant
		//		new Message(Role.System, "You are a helpful assistant expert in SQL Server and help the user to comment and improve the code." +
		//								 "I need create a new table in SQL code to run in SQL server. " +
		//								 "Agrega los prefijos " +
		//								 "[i para INTEGER] "+
		//								 "[vch para VARCHAR] "+
		//								 "[nvch para NVARCHAR] "+
		//								 "[dt para DATETIME] "+
		//								 "[m para MONEY] "+
		//								 "[sm para SMALLINT] "+
		//								 "[bi para BIGINT] "+
		//								 "[fl para FLOAT] "+
		//								 "[d para DATE] "+
		//								 "[B para BIT] " +
		//								 "a los atributo de las tablas," +
		//								 "siemrpe crea el primer regist con el mismot nombre de la tabla como primery key y que sea  IDENTITY(1, 1) y que tenga como prefijo iId," +
		//								 "agrega  a todos los campos NOT NULL," +
		//								 "Si el usuario no te proporciona atributos, tu crea los necesario segun el contexto." +
		//								 "La tabla tiene que tener nombre en singular."

		//								 ),
		//		// User's input SQL code encapsulated in a message
		//		new Message(Role.User, "Crea la tabla a partir de las siguentes intrucciones.  [*" + prmpt + "*]"),
		//	};
		//}

		public async Task<Response<IEnumerable<StoredProcedure>>> GetProceduresByKey(PostGetItemByKey DAOmodel)
		{
			try
			{
				// Initialize the response object that will be returned


				string scrypt = ScriptQueryBD.GetProceduresByKey.Replace("@vchkey", DAOmodel.vchKey);

				var result = await dataBaseRepository.sqGetProceduresByKey(scrypt, DAOmodel.ConnectionString);
				var response = responseHelper.Validacion<sqGetProceduresByKey.Result, StoredProcedure>(result);


				if (result.NotEmpty())
				{
					// Process each stored procedure name retrieved
					foreach (var item in response.Data)
					{
						// Retrieve details for each stored procedure
						var postModelGetStoreProcedure = new PostGetStoreProcedure()
						{
							ConnectionString = DAOmodel.ConnectionString,
							vchName = item.vchName
						};
						var resultSP = await GetStoreProcedure(postModelGetStoreProcedure);
						// If successful, add the details to the response
						if (resultSP.IsSuccess)
						{
							item.Details = resultSP.Data;
						}
					}

					// Mark the response as successful if any stored procedure details were added
					response.IsSuccess = (response.Data.Any());
				}

				return response;

			}
			catch (Exception ex)
			{
				return Response<IEnumerable<StoredProcedure>>.BadResult(ex.Message, new List<StoredProcedure>());
			}
		}


		public async Task<Response<IEnumerable<Table>>> GetTablesByKey(PostGetItemByKey DAOmodel)
		{
			try
			{
				// Initialize the response object that will be returned
				var response = new Response<IEnumerable<Table>>();

				string scrypt = ScriptQueryBD.GetTablesbykey.Replace("@vchkey", DAOmodel.vchKey);

				var result = await dataBaseRepository.sqGetTablesByKey(scrypt, DAOmodel.ConnectionString);

				if (result.Any())
				{
					response = await GetTables(new PostGetTables { ConnectionString = DAOmodel.ConnectionString });

					response.Data = response.Data
						.Where(t => result.Any(r => r.vchName == t.vchName))
						.ToList();

					// Mark the response as successful if any stored procedure details were added



					response.IsSuccess = (response.Data.Any());
				}

				return response;

			}
			catch (Exception ex)
			{
				return Response<IEnumerable<Table>>.BadResult(ex.Message, new List<Table>());
			}
		}


	}
}
