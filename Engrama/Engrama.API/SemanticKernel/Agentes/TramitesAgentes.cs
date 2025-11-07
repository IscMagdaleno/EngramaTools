// Engrama.API/SemanticKernel/Agentes/TramitesAgentes.cs
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

using System.Text;

namespace Engrama.API.SemanticKernel.Agentes
{
	/// <summary>
	/// Orquestador de Agente Engrama (MCP): Carga el Kernel, los Plugins y el Contexto de Engrama.
	/// </summary>
	public class TramitesAgentes
	{
		private readonly Kernel _kernel;
		private readonly ILogger<TramitesAgentes> _logger;
		private readonly IChatCompletionService _chatCompletionService;

		private readonly PromptExecutionSettings _executionSettings;
		// Asume que los archivos de documentación de la metodología están en esta ruta relativa.
		private readonly string _basePath = AppDomain.CurrentDomain.BaseDirectory;

		public TramitesAgentes(Kernel kernel, ILogger<TramitesAgentes> logger)
		{
			_kernel = kernel;
			_logger = logger;

			// Obtenemos el servicio de chat del Kernel
			_chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

			// CAMBIO CLAVE 2: Usar la clase base sin configuración específica de OpenAI.
			// Esto permite que el conector de Gemini maneje la llamada a funciones correctamente.
			_executionSettings = new PromptExecutionSettings();

			_logger.LogInformation("TramitesAgentes inicializado con el Kernel y el servicio de chat.");
		}

		/// Inicia o continúa una conversación con el asistente.
		/// </summary>
		/// <param name="userMessage">El mensaje del usuario.</param>
		/// <param name="chatHistory">El historial de chat actual para la conversación.</param>
		/// <returns>La respuesta del asistente.</returns>
		public async Task<string> ChatAsync(string userMessage, ChatHistory chatHistory) // Modificado para aceptar ChatHistory
		{
			_logger.LogInformation($"Usuario: {userMessage}");

			chatHistory.AddUserMessage(userMessage);

			var result = await _chatCompletionService.GetChatMessageContentAsync(
				chatHistory,
				executionSettings: _executionSettings,
				kernel: _kernel // Se pasa el kernel para que el servicio tenga acceso a los plugins registrados
			);

			string assistantResponse = result.Content ?? "No pude generar una respuesta.";

			chatHistory.AddAssistantMessage(assistantResponse);

			_logger.LogInformation($"Asistente: {assistantResponse}");
			return assistantResponse;
		}




		/// <summary>
		/// Construye el System Prompt inyectando la documentación de la metodología Engrama.
		/// </summary>
		public string BuildSystemPrompt()
		{
			var sb = new StringBuilder();
			sb.AppendLine("Eres un asistente experto llamado 'AgenteEngrama'. Tu propósito es ayudar al usuario a entender y desarrollar aplicaciones usando la 'Metodología Engrama'." +
			" Tienes acceso a funciones para consultar la base de datos de la aplicación y a la documentación oficial de la metodología. " +
			"Usa tus herramientas (plugins) siempre que el usuario pregunte por datos técnicos de la base de datos (tablas, SPs, etc.)." +
			" Si el usuario pregunta sobre la metodología, usa tu contexto interno.");

			sb.AppendLine("\n--- DOCUMENTACIÓN DE LA METODOLOGÍA ENGRAMA ---");

			// Leer y adjuntar el contenido de los archivos de documentación
			// NOTA: Esta implementación asume que los archivos .md están en la raíz o en una ubicación accesible. 
			// En un entorno de producción, la lectura de archivos debe ser robusta.
			sb.AppendLine($"\n{ReadDocumentationFile("1- Base de Datos.md")}");
			sb.AppendLine($"\n{ReadDocumentationFile("2- Stored Procedures.md")}");
			sb.AppendLine($"\n{ReadDocumentationFile("3- Solucion base y proyecto Share.md")}");
			sb.AppendLine($"\n{ReadDocumentationFile("4- API y Capra Infraestrucura.md")}");
			sb.AppendLine($"\n{ReadDocumentationFile("5- Dominio.md")}");
			sb.AppendLine($"\n{ReadDocumentationFile("6- Servicio.md")}");
			sb.AppendLine($"\n{ReadDocumentationFile("7- Cliente.md")}");
			sb.AppendLine("--- FIN DOCUMENTACIÓN ---");

			return sb.ToString();
		}

		private string ReadDocumentationFile(string fileName)
		{
			try
			{
				var filePath = Path.Combine($@"C:\Users\ing_s\OneDrive\ngrok_recovery_codes\Engrama\Docs\Metodologia", fileName);
				if (File.Exists(filePath))
				{
					return $"## Archivo: {fileName}\n{File.ReadAllText(filePath)}";
				}
				// Si el archivo no existe en el path directo (típico en la API), intenta buscarlo en la carpeta de recursos.
				// Para este ejemplo y evitar errores, devolvemos un mensaje.
				return $"[ERROR: Archivo {fileName} no encontrado en la ruta de ejecución.]";

			}
			catch (Exception ex)
			{
				return $"[ERROR al leer {fileName}: {ex.Message}]";
			}
		}
	}
}