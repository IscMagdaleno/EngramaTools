using Engrama.Share.Objetos.AgenteEngrama;
using Engrama.Share.PostClass.AgenteEngrama.Engrama.Share.PostModels.AgenteEngrama;

namespace Engrama.API.EngramaLevels.Dominio.Interfaces
{
	public interface IAgentOrchestrationService
	{
		Task<AgentResponse> ProcessUserQueryAsync(PostAskAgent postModel);
	}
}
