using Engrama.API.EngramaLevels.Infrastructure.Entity.WorkFlow;
using Engrama.API.EngramaLevels.Infrastructure.Interfaces;

using EngramaCoreStandar.Dapper;
using EngramaCoreStandar.Extensions;

namespace Engrama.API.EngramaLevels.Infrastructure.Repository
{
	public class WorkFlowRepository : IWorkFlowRepository
	{
		private readonly IDapperManagerHelper managerHelper;

		public WorkFlowRepository(IDapperManagerHelper managerHelper)
		{
			this.managerHelper = managerHelper;
		}

		public async Task<spSaveWorkPlan.Result> spSaveWorkPlan(spSaveWorkPlan.Request DAOmodel)
		{
			var result = await managerHelper.GetAsync<spSaveWorkPlan.Result, spSaveWorkPlan.Request>(DAOmodel);
			if (result.Ok)
			{
				return result.Data;
			}
			// Retorna una respuesta de error estandarizada
			return new() { bResult = false, vchMessage = $"[{(result.Ex.NotNull() ? result.Ex.Message : "")}] - [{result.Msg}]" };
		}

		public async Task<spGetWorkPlan.Result> spGetWorkPlan(spGetWorkPlan.Request DAOmodel)
		{
			var result = await managerHelper.GetAsync<spGetWorkPlan.Result, spGetWorkPlan.Request>(DAOmodel);
			if (result.Ok)
			{
				return result.Data;
			}
			return new() { bResult = false, vchMessage = $"[{(result.Ex.NotNull() ? result.Ex.Message : "")}] - [{result.Msg}]" };
		}
	}
}
