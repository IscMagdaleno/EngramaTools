using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.Share.Entity.DataBase
{
	public class sp_helptext
	{
		public class Request : SpRequest
		{
			public string StoredProcedure { get => "sp_helptext"; }
			public string objname { get; set; }
		}

		public class Result : DbResult
		{
			public bool bResult { get; set; } = false;
			public string vchMessage { get; set; } = "Sin Resultado";
			public string Text { get; set; }

		}
	}
}
