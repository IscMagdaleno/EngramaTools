using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.API.EngramaLevels.Infrastructure.Entity
{
	public class spLogin
	{
		public class Request : SpRequest
		{
			public string StoredProcedure { get => "spLogin"; }
			public string vchNickName { get; set; }
			public string vchPassword { get; set; }
		}
		public class Result : DbResult
		{
			public bool bResult { get; set; }
			public string vchMessage { get; set; }
			public int iIdUsers { get; set; }
			public int iIdRoles { get; set; }
			public string vchName { get; set; }
			public string vchNameRole { get; set; }
			public string vchEmail { get; set; }
			public bool bStatus { get; set; }
		}
	}
}
