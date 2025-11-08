using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.API.EngramaLevels.Infrastructure.Entity.DataBase
{
	public class sqGetAllStoredProcedures
	{
		public class Result : DbResult
		{

			public bool bResult { get; set; } = true;
			public string vchMessage { get; set; } = string.Empty;

			public int idStoredProcedure { get; set; }
			public string vchName { get; set; }
			public string vchObjectType { get; set; }
			public DateTime dtCreationDate { get; set; }
			public int iOrden { get; set; }
			public string vchParameterName { get; set; }
			public int iMaxBite { get; set; }
			public bool bIsOuput { get; set; }
		}
	}
}
