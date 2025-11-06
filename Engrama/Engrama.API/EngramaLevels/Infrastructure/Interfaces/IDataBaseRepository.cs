using Engrama.Share.Entity.DataBase;

namespace Engrama.API.EngramaLevels.Infrastructure.Interfaces
{
	public interface IDataBaseRepository
	{
		Task<IEnumerable<sqGetTables.Result>> sqGetTables(string Query, string connectionString);
		Task<IEnumerable<spGetConnectionString.Result>> spGetConnectionString(spGetConnectionString.Request DAOmodel);
		Task<spSaveConnectionString.Result> spSaveConnectionString(spSaveConnectionString.Request DAOmodel);
		Task<IEnumerable<sqGetAllStoredProcedures.Result>> sqGetAllStoredProcedures(string Query, string connectionString);
		Task<IEnumerable<sp_helptext.Result>> sqGetStoreProcedure(sp_helptext.Request DAOmodel, string CadenaConexion);
		Task<IEnumerable<sqGetProceduresByKey.Result>> sqGetProceduresByKey(string Query, string connectionString);
		Task<IEnumerable<sqGetProceduresByKey.Result>> sqGetTablesByKey(string Query, string connectionString);
	}
}
