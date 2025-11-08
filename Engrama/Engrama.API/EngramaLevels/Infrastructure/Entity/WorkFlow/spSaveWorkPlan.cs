using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.API.EngramaLevels.Infrastructure.Entity.WorkFlow
{
	public class spSaveWorkPlan
	{
		public class Request : SpRequest
		{
			public string StoredProcedure { get => "spSaveWorkPlan"; }
			public int iIdWorkPlan { get; set; }
			public int iIdUser { get; set; }
			public string vchInitialDescription { get; set; }
			public string vchImprovedDescription { get; set; }
			// El campo a continuación contendrá todo el objeto WorkPlan serializado a JSON para simplificar el SP
			public string vchFullPlanJSON { get; set; }
		}
		public class Result : DbResult
		{
			public bool bResult { get; set; }
			public string vchMessage { get; set; }
			public int iIdWorkPlan { get; set; }
		}
	}
}
