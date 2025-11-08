using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.API.EngramaLevels.Infrastructure.Entity
{
	public class spGetCatalogue
	{
		public class Request : SpRequest
		{
			public string StoredProcedure { get => "spGetCatalogue"; }
			public string vchCatalogueName { get; set; }
		}
		public class Result : DbResult
		{
			public bool bResult { get; set; }
			public string vchMessage { get; set; }
			public int iId { get; set; }
			public string vchDescription { get; set; }
		}
	}

}
