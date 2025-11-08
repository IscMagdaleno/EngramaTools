using Engrama.API.EngramaLevels.Infrastructure.Entity.WorkFlow;

namespace Engrama.API.EngramaLevels.Infrastructure.Interfaces
{
	public interface IWorkFlowRepository
	{
		Task<spSaveWorkPlan.Result> spSaveWorkPlan(spSaveWorkPlan.Request DAOmodel);
		Task<spGetWorkPlan.Result> spGetWorkPlan(spGetWorkPlan.Request DAOmodel);
	}
}
