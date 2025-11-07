using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.API.SemanticKernel.Agentes;
using Engrama.Share.Objetos.AgenteEngrama;
using Engrama.Share.PostClass.AgenteEngrama.Engrama.Share.PostModels.AgenteEngrama;

using Microsoft.SemanticKernel.ChatCompletion;

using System.Collections.Concurrent;

namespace Engrama.API.EngramaLevels.Dominio.Servicios
{
	public class AgentOrchestrationService : IAgentOrchestrationService
	{
		private readonly TramitesAgentes _tramitesAgentes;
		private readonly ILogger<AgentOrchestrationService> _logger;
		// Almacenamiento en memoria para el historial de chat por usuario.
		// En un entorno de producción, esto debería ser un almacenamiento persistente (ej. base de datos, caché distribuida).
		private static readonly ConcurrentDictionary<string, ChatHistory> _chatHistories = new ConcurrentDictionary<string, ChatHistory>();

		public AgentOrchestrationService(
			TramitesAgentes tramitesAgentes,
			ILogger<AgentOrchestrationService> logger)
		{
			_tramitesAgentes = tramitesAgentes;
			_logger = logger;
		}

		/// <summary>
		/// Procesa una consulta de usuario utilizando el Agente de Ventas de Productos.
		/// Mantiene el contexto de la conversación para cada usuario.
		/// </summary>
		/// <param name="userQuery">La consulta de texto del usuario.</param>
		/// <param name="userId">El identificador único del usuario para mantener el contexto de la conversación.</param>
		/// <returns>Un objeto ChatResponseDto con la respuesta del agente.</returns>
		public async Task<AgentResponse> ProcessUserQueryAsync(PostAskAgent postModel)
		{
			_logger.LogInformation($"Orquestando consulta para usuario '{postModel.nvchUserId}': '{postModel.vchPrompt}'");

			// Obtener o crear el historial de chat para el usuario
			ChatHistory chatHistory = _chatHistories.GetOrAdd(postModel.nvchUserId, (id) =>
			{
				var newHistory = new ChatHistory();
				newHistory.AddSystemMessage(_tramitesAgentes.BuildSystemPrompt()); // Añadir el prompt del sistema al inicio de la nueva conversación
				_logger.LogInformation($"Nuevo historial de chat creado para el usuario '{id}'.");
				return newHistory;
			});

			try
			{
				// Invocar al ProductSalesAgent con la consulta del usuario y el historial de chat
				string agentResponse = await _tramitesAgentes.ChatAsync(postModel.vchPrompt, chatHistory);


				return new AgentResponse { vchResponse = agentResponse };
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error al procesar la consulta del usuario '{postModel.nvchUserId}': {ex.Message}");
				// Devolver una respuesta amigable en caso de error
				return new AgentResponse { vchResponse = "Lo siento, hubo un error al procesar tu solicitud. Por favor, inténtalo de nuevo más tarde." };
			}
		}
	}

}
