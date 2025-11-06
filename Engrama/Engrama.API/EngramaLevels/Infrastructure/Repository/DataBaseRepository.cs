using Engrama.API.EngramaLevels.Infrastructure.Interfaces;
using Engrama.Share.Entity.DataBase;

using EngramaCoreStandar.Dapper;

namespace Engrama.API.EngramaLevels.Infrastructure.Repository
{
	public class DataBaseRepository : IDataBaseRepository
	{

		private readonly IDapperManagerHelper managerHelper;

		public DataBaseRepository(IDapperManagerHelper managerHelper)
		{
			this.managerHelper = managerHelper;
		}


		public async Task<spSaveConnectionString.Result> spSaveConnectionString(spSaveConnectionString.Request DAOmodel)
		{
			var result = await managerHelper.GetAsync<spSaveConnectionString.Result, spSaveConnectionString.Request>(DAOmodel);
			if (result.Ok)
			{
				return result.Data;

			}
			return new();

		}

		public async Task<IEnumerable<spGetConnectionString.Result>> spGetConnectionString(spGetConnectionString.Request DAOmodel)
		{
			var result = await managerHelper.GetAllAsync<spGetConnectionString.Result, spGetConnectionString.Request>(DAOmodel, "");
			if (result.Ok)
			{
				return result.Data;

			}
			return new List<spGetConnectionString.Result>() { new() { bResult = false, vchMessage = result.Msg } };

		}

		public async Task<IEnumerable<sqGetTables.Result>> sqGetTables(string Query, string connectionString)
		{
			var respuesta = await managerHelper.GetAllAsync<sqGetTables.Result>(Query, connectionString);
			if (respuesta.Ok)
			{
				return respuesta.Data;
			}
			else
			{
				return new List<sqGetTables.Result>() { new() { bResult = false, vchMessage = respuesta.Msg } };
			}
		}

		public async Task<IEnumerable<sqGetAllStoredProcedures.Result>> sqGetAllStoredProcedures(string Query, string connectionString)
		{
			var respuesta = await managerHelper.GetAllAsync<sqGetAllStoredProcedures.Result>(Query, connectionString);
			if (respuesta.Ok)
			{
				return respuesta.Data;
			}
			else
			{
				return new List<sqGetAllStoredProcedures.Result>() { new() { bResult = false, vchMessage = respuesta.Msg } };
			}
		}


		public async Task<IEnumerable<sp_helptext.Result>> sqGetStoreProcedure(sp_helptext.Request DAOmodel, string CadenaConexion)
		{
			var respuestaCodigo = await managerHelper.GetAllAsync<sp_helptext.Result, sp_helptext.Request>(DAOmodel, CadenaConexion);
			if (respuestaCodigo.Ok)
			{
				return respuestaCodigo.Data;
			}


			return new List<sp_helptext.Result>() { new() { vchMessage = respuestaCodigo.Msg } };
		}


		public async Task<IEnumerable<sqGetProceduresByKey.Result>> sqGetProceduresByKey(string Query, string connectionString)
		{
			var respuesta = await managerHelper.GetAllAsync<sqGetProceduresByKey.Result>(Query, connectionString);
			if (respuesta.Ok)
			{
				return respuesta.Data;
			}
			else
			{
				return new List<sqGetProceduresByKey.Result>() { new() { bResult = false, vchMessage = respuesta.Msg } };
			}
		}

		public async Task<IEnumerable<sqGetProceduresByKey.Result>> sqGetTablesByKey(string Query, string connectionString)
		{
			var respuesta = await managerHelper.GetAllAsync<sqGetProceduresByKey.Result>(Query, connectionString);
			if (respuesta.Ok)
			{
				return respuesta.Data;
			}
			else
			{
				return new List<sqGetProceduresByKey.Result>() { new() { bResult = false, vchMessage = respuesta.Msg } };
			}
		}

	}
}
