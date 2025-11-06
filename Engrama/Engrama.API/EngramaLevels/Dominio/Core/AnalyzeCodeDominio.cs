using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.API.EngramaLevels.Infrastructure.Interfaces;

using EngramaCoreStandar.Mapper;
using EngramaCoreStandar.Results;


namespace Engrama.API.EngramaLevels.Dominio.Core
{
	public class AnalyzeCodeDominio : IAnalyzeCodeDominio
	{
		private readonly IDataBaseRepository dataBaseRepository;
		private readonly MapperHelper mapper;
		private readonly IResponseHelper responseHelper;

		/// <summary>
		/// Initialize the fields receiving the interfaces on the builder
		/// </summary>
		public AnalyzeCodeDominio(IDataBaseRepository dataBaseRepository,
			MapperHelper mapper,
			IResponseHelper responseHelper)
		{
			this.dataBaseRepository = dataBaseRepository;
			this.mapper = mapper;
			this.responseHelper = responseHelper;
		}

		//public async Task<Response<CodeAnalyze>> AnalyzeCSharp(PostAnalyzeCSharp postModel)
		//{
		//	// Initialize the OpenAI client with the API key
		//	using var api = new OpenAIClient("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");

		//	// Extract C# code content from the posted model
		//	string CSharpContent = postModel.vchCSharpCode;

		//	// Prepare messages to be sent to the OpenAI API for code analysis
		//	var messages = CreateMessages(CSharpContent);

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
		//private static List<Message> CreateMessages(string CSharpContent)
		//{
		//	return new List<Message>
		//	{
		//		new Message(Role.System, "You are a helpful assistant expert in .NET and C#, help me to comment and  improve the code." +
		//		"Don't change the names of the parameters or variables, " +
		//		"If you need to split the code on diferent methods is ok, " +
		//		"Add the script \t/// <summary>\r\n\t\t/// \r\n\t\t/// </summary>\r\n\t\t/// <param name=\"DAOmodel\"></param>\r\n\t\t/// <returns></returns>\r\n on the top of the method and fill it with the description of the method " +
		//		"Always add comments at the code"),
		//		new Message(Role.User, "There is my C# code.  [*" + CSharpContent + "*]"),
		//	};
		//}

		//// Create and populate the response object
		//private static Response<CodeAnalyze> CreateResponse(PostAnalyzeCSharp postModel, Choice choice)
		//{
		//	var CodeAnalyze = new CodeAnalyze
		//	{
		//		vchCode = postModel.vchCSharpCode,
		//		vchCodeAnalized = choice.Message
		//	};
		//	CodeAnalyze.FillDetails();

		//	return new Response<CodeAnalyze>
		//	{
		//		IsSuccess = true,
		//		Data = CodeAnalyze

		//	};
		//}
	}
}
