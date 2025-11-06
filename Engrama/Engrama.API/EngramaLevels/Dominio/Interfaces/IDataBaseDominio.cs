using Engrama.Share.Objetos.DataBaseArea;
using Engrama.Share.PostClass;
using Engrama.Share.PostClass.DataBase;

using EngramaCoreStandar.Results;

namespace Engrama.API.EngramaLevels.Dominio.Interfaces
{
	public interface IDataBaseDominio
	{
		Task<Response<IEnumerable<ConnectionString>>> GetConnectionString(PostGetConnectionString DAOmodel);
		Task<Response<IEnumerable<StoredProcedure>>> GetAllStoredProcedures(PostGetAllStoredProcedures DAOmodel);
		Task<Response<IEnumerable<Table>>> GetTables(PostGetTables DAOmodel);
		Task<Response<ConnectionString>> SaveConnectionString(PostSaveConnectionString DAOmodel);
		Task<Response<DetailsStoreProcedure>> GetStoreProcedure(PostGetStoreProcedure DAOmodel);
		Task<Response<IList<StoredProcedure>>> GetAllStoredProcedureDetails(PostGetAllStoredProcedureDetails DAOmodel);
		Task<Response<IEnumerable<StoredProcedure>>> GetProceduresByKey(PostGetItemByKey DAOmodel);
		Task<Response<IEnumerable<Table>>> GetTablesByKey(PostGetItemByKey DAOmodel);
		Task<Response<ConnectionString>> ProcesoConnectionString(PostSaveConnectionString DAOmodel);
	}
}
