// Engrama.PWA/Areas/WorkFlow/Utiles/MainWorkFlow.cs

using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.Share.Objetos.Workflow;
using Engrama.Share.PostClass.WorkFlow;

using EngramaCoreStandar.Dapper.Results;
using EngramaCoreStandar.Results;
using EngramaCoreStandar.Servicios;

namespace Engrama.PWA.Areas.WorkFlow.Utiles
{
	public class MainWorkFlow
	{
		private string url = @"api/WorkFlow"; // El API endpoint sigue en AI_ToolsController

		private readonly IHttpService httpService;
		private readonly UserSession userSession;
		private readonly IValidaServicioService validaServicioService;

		public WorkPlan CurrentWorkPlan { get; set; }
		public string WorkPlanDescription { get; set; }

		public MainWorkFlow(IHttpService httpService, IValidaServicioService validaServicioService, UserSession userSession)
		{
			this.httpService = httpService;
			this.userSession = userSession;
			this.validaServicioService = validaServicioService;

			CurrentWorkPlan = new WorkPlan();
			WorkPlanDescription = string.Empty;
		}

		/// <summary>
		/// Envia la descripción a la IA para generar el plan de trabajo estructurado.
		/// </summary>
		public async Task<SeverityMessage> PostGenerateWorkPlan()
		{
			var APIUrl = url + "/PostGenerateWorkPlan";

			var model = new PostGenerateWorkPlan()
			{
				Description = WorkPlanDescription,
				iIdUser = userSession.iId,
				iIdWorkPlan = CurrentWorkPlan.iIdWorkPlan
			};

			var response = await httpService.Post<PostGenerateWorkPlan, Response<WorkPlan>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => CurrentWorkPlan = data);

			return validacion;
		}

		/// <summary>
		/// Obtiene un plan de trabajo existente por ID.
		/// </summary>
		public async Task<SeverityMessage> PostGetWorkPlanById(int iIdWorkPlan)
		{
			var APIUrl = url + "/PostGetWorkPlanById";

			var model = new PostGetWorkPlanById { iIdWorkPlan = iIdWorkPlan };

			var response = await httpService.Post<PostGetWorkPlanById, Response<WorkPlan>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => CurrentWorkPlan = data);

			return validacion;
		}

		/// <summary>
		/// Persiste los cambios realizados por el usuario en el plan de trabajo.
		/// </summary>
		public async Task<SeverityMessage> PostUpdateWorkPlanDetails()
		{
			var APIUrl = url + "/PostUpdateWorkPlanDetails";

			var response = await httpService.Post<WorkPlan, Response<WorkPlan>>(APIUrl, CurrentWorkPlan);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => CurrentWorkPlan = data);

			return validacion;
		}
	}
}