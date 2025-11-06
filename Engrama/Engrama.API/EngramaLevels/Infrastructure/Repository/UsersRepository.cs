using Engrama.API.EngramaLevels.Infrastructure.Interfaces;
using Engrama.Share.Entity;

using EngramaCoreStandar.Dapper;
using EngramaCoreStandar.Extensions;

namespace Engrama.API.EngramaLevels.Infrastructure.Repository
{
	public class UsersRepository : IUsersRepository
	{
		private readonly IDapperManagerHelper managerHelper;

		public UsersRepository(IDapperManagerHelper managerHelper)
		{
			this.managerHelper = managerHelper;
		}

		public async Task<spLogin.Result> spLogin(spLogin.Request DAOmodel)
		{
			var result = await managerHelper.GetAsync<spLogin.Result, spLogin.Request>(DAOmodel, "");
			if (result.Ok)
			{
				return result.Data;

			}
			return new() { bResult = false, vchMessage = $"[{(result.Ex.NotNull() ? result.Ex.Message : "")}] - [{result.Msg}]" };

		}


		public async Task<spSaveUsers.Result> spSaveUsers(spSaveUsers.Request DAOmodel)
		{
			var result = await managerHelper.GetAsync<spSaveUsers.Result, spSaveUsers.Request>(DAOmodel);
			if (result.Ok)
			{
				return result.Data;

			}
			return new() { bResult = false, vchMessage = $"[{(result.Ex.NotNull() ? result.Ex.Message : "")}] - [{result.Msg}]" };

		}


	}
}
