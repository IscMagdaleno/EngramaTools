using Engrama.API.EngramaLevels.Infrastructure.Entity;
using Engrama.API.EngramaLevels.Infrastructure.Entity.CommonScripts;
using Engrama.API.EngramaLevels.Infrastructure.Interfaces;

using EngramaCoreStandar.Dapper;

namespace Engrama.API.EngramaLevels.Infrastructure.Repository
{
	public class CommonScriptsRepository : ICommonScriptsRepository
	{
		private readonly IDapperManagerHelper managerHelper;


		public CommonScriptsRepository(IDapperManagerHelper managerHelper)
		{
			this.managerHelper = managerHelper;
		}


		public async Task<spSaveCommonScripts.Result> spSaveCommonScripts(spSaveCommonScripts.Request DAOmodel)
		{
			var result = await managerHelper.GetAsync<spSaveCommonScripts.Result, spSaveCommonScripts.Request>(DAOmodel, "");
			if (result.Ok)
			{
				return result.Data;

			}
			return new();

		}

		public async Task<IEnumerable<spGetCommonScripts.Result>> spGetCommonScripts(spGetCommonScripts.Request DAOmodel)
		{
			var result = await managerHelper.GetAllAsync<spGetCommonScripts.Result, spGetCommonScripts.Request>(DAOmodel, "");
			if (result.Ok)
			{
				return result.Data;

			}
			return new List<spGetCommonScripts.Result>() { new() { bResult = false, vchMessage = result.Msg } };

		}


		public async Task<IEnumerable<spGetCatalogue.Result>> spGetCatalogue(spGetCatalogue.Request DAOmodel)
		{
			var result = await managerHelper.GetAllAsync<spGetCatalogue.Result, spGetCatalogue.Request>(DAOmodel, "");
			if (result.Ok)
			{
				return result.Data;

			}
			return new List<spGetCatalogue.Result>() { new() { bResult = false, vchMessage = result.Msg } };

		}



	}
}
