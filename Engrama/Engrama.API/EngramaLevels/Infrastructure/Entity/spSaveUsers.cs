using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.API.EngramaLevels.Infrastructure.Entity
{
	public class spSaveUsers
	{
		public class Request : SpRequest
		{
			public string StoredProcedure { get => "spSaveUsers"; }
			public int iIdUsers { get; set; }
			public int iIdRoles { get; set; }
			public string vchName { get; set; }
			public string vchEmail { get; set; }
			public string vchPass { get; set; }
			public string vchNickName { get; set; }
			public bool bStatus { get; set; }
		}
		public class Result : DbResult
		{
			public bool bResult { get; set; }
			public string vchMessage { get; set; }
			public int iIdUsers { get; set; }
		}
	}

}
