using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.Share.Entity.DataBase
{
	public class spGetConnectionString
	{
		public class Request : SpRequest
		{
			public string StoredProcedure { get => "spGetConnectionString"; }
			public int iIdUser { get; set; }
			public bool bActivo { get; set; }

		}

		public class Result : DbResult
		{
			public bool bResult { get; set; }
			public string vchMessage { get; set; }
			public int iIdConnectionString { get; set; }
			public int iIdUser { get; set; }
			public string vchConnectionString { get; set; }
			public string vchNota { get; set; }
			public bool bActivo { get; set; }
		}
	}

}
