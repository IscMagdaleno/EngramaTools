// Engrama.API/Controllers/AgenteEngramaController.cs (Nuevo Controlador)
using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.Share.PostClass.AgenteEngrama.Engrama.Share.PostModels.AgenteEngrama;

using Microsoft.AspNetCore.Mvc;

namespace Engrama.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AgenteEngramaController : ControllerBase
	{
		private readonly IAgentOrchestrationService _agentOrchestrationService;

		public AgenteEngramaController(IAgentOrchestrationService agentOrchestrationService)
		{
			_agentOrchestrationService = agentOrchestrationService;
		}

		/// <summary>
		/// Envía una consulta al Agente Engrama (Semantic Kernel) para obtener una respuesta contextualizada.
		/// </summary>
		/// <param name="postModel">Modelo de entrada con el prompt del usuario y el historial del chat.</param>
		[HttpPost("PostAskAgent")]
		public async Task<IActionResult> PostAskAgent([FromBody] PostAskAgent postModel)
		{
			// El controlador se mantiene ligero, delegando la lógica al Dominio.
			var result = await _agentOrchestrationService.ProcessUserQueryAsync(postModel);


			return Ok(result);

			// Retorna BadRequest si IsSuccess es falso, siguiendo la convención de Engrama
		}
	}
}