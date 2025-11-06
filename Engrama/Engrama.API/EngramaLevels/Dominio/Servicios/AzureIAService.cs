using Azure;
using Azure.AI.OpenAI;

using Engrama.API.EngramaLevels.Dominio.Servicios.Models;

using OpenAI.Chat;

namespace Engrama.API.EngramaLevels.Dominio.Servicios
{
	public class AzureIAService
	{

		private readonly AzureOpenAIClient azureClient;
		private readonly ChatClient chatClient;
		private readonly string _deploymentName = "gpt-4.1";
		private readonly Uri endpoint = new Uri("https://engramaiaservice.openai.azure.com/");

		public AzureIAService()
		{

			var apiKey = "52tHSAQzrAe2pBIlHP6cJxsJx6HrdnPjye94YsItVtn8mm3cx17BJQQJ99BGACYeBjFXJ3w3AAABACOG8KdP";



			azureClient = new(
			   endpoint,
			   new AzureKeyCredential(apiKey));

			chatClient = azureClient.GetChatClient(_deploymentName);

		}


		public async Task<ChatCompletion> CallAzureOpenIA(RequestOpenAI request)
		{


			var requestOptions = new ChatCompletionOptions()
			{
				Temperature = 1.0f,
				TopP = 1.0f,
				FrequencyPenalty = 0.0f,
				PresencePenalty = 0.0f,

			};

			List<ChatMessage> messages = new List<ChatMessage>()
			{
				new SystemChatMessage(request.Configuration),
				new UserChatMessage($"{request.Prompt}"),

			};


			var response = chatClient.CompleteChat(messages, requestOptions);


			return response.Value;

		}


	}
}
