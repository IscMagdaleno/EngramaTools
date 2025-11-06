namespace Engrama.API.EngramaLevels.Dominio.Servicios
{
	public class OpenIAService
	{

		//public async Task<Response<ResultOpenAI>> CallOpenAPI(RequestOpenAI request)
		//{
		//	using var api = new OpenAIClient("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");


		//	var messages = CreateMessages(request);

		//	var chatRequest = new ChatRequest(messages);

		//	var response = await api.ChatEndpoint.GetCompletionAsync(chatRequest);

		//	var choice = response.FirstChoice;

		//	var respuesta = CreateResponse(choice);


		//	return respuesta;
		//}

		//// Create message list for the chat request
		//private static List<Message> CreateMessages(RequestOpenAI request)
		//{
		//	return new List<Message>
		//	{
		//		new Message(Role.System, request.Configuration),
		//		new Message(Role.User, request.Prompt),
		//	};
		//}


		//// Create and populate the response object
		//private static Response<ResultOpenAI> CreateResponse(Choice choice)
		//{
		//	var CodeAnalyze = new ResultOpenAI
		//	{
		//		ResultaAI = choice.Message
		//	};

		//	return new Response<ResultOpenAI>
		//	{
		//		IsSuccess = true,
		//		Data = CodeAnalyze

		//	};
		//}
	}
}
