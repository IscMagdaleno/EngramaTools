using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.API.EngramaLevels.Infrastructure.Entity.DataBase
{
	public class sqGetProceduresByKey
	{
		public class Result : DbResult
		{
			public bool bResult { get; set; } = true;
			public string vchMessage { get; set; } = string.Empty;

			public string vchName { get; set; }
		}
	}
}
