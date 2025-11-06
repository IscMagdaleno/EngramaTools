using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.Share.Entity.CommonScripts
{
	public class spGetCommonScripts
	{
		public class Request : SpRequest
		{
			public string StoredProcedure { get => "spGetCommonScripts"; }
		}
		public class Result : DbResult
		{
			public bool bResult { get; set; }
			public string vchMessage { get; set; }
			public int iIdCommonScripts { get; set; }
			public int iIdCatProyectType { get; set; }
			public string vchName { get; set; }
			public string vchDescription { get; set; }
			public string vchCode { get; set; }
		}
	}
}
