using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.API.EngramaLevels.Infrastructure.Entity.DataBase
{
	public class sqGetTables
	{
		public class Result : DbResult
		{
			public bool bResult { get; set; } = true;
			public string vchMessage { get; set; } = string.Empty;

			public int iIdTable { get; set; }
			public string vchName { get; set; }
			public int iNoRows { get; set; }
			public string vchPrimaryKey { get; set; }

			public string vchColumnName { get; set; }
			public string vchDataType { get; set; }
			public int iMaxBites { get; set; }
		}
	}
}
