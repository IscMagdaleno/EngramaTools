using Engrama.Share.Objetos.Workflow;
using Engrama.Share.PostClass.WorkFlow;

using EngramaCoreStandar.Results;

namespace Engrama.API.EngramaLevels.Dominio.Interfaces
{
	public interface IWorkFlowDominio
	{
		Task<Response<WorkPlan>> GenerateWorkPlan(PostGenerateWorkPlan postModel);
		Task<Response<WorkPlan>> GetWorkPlanById(int iIdWorkPlan);
		Task<Response<WorkPlan>> UpdateWorkPlanDetails(PostUpdateWorkPlanDetails postModel);
	}
}
