using EngramaCoreStandar.Dapper.Interfaces;

namespace Engrama.API.EngramaLevels.Infrastructure.Entity.WorkFlow
{
	public class spGetWorkPlan
	{
		public class Request : SpRequest
		{
			public string StoredProcedure { get => "spGetWorkPlan"; }
			public int iIdWorkPlan { get; set; }
		}
		public class Result : DbResult
		{
			public bool bResult { get; set; }
			public string vchMessage { get; set; }

			// Campos de WorkPlan
			public int iIdWorkPlan { get; set; }
			public int iIdUser { get; set; }
			public string vchInitialDescription { get; set; }
			public string vchImprovedDescription { get; set; }
			public DateTime dtCreationDate { get; set; }
			public bool bStatus { get; set; }

			// El resultado completo del Plan de Trabajo se devolverá como un solo JSON
			public string vchFullPlanJSON { get; set; }
		}
	}
}
