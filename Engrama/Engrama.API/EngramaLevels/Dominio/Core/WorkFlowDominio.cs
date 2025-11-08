using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.API.EngramaLevels.Infrastructure.Entity.WorkFlow;
using Engrama.API.EngramaLevels.Infrastructure.Interfaces;
using Engrama.Share.Objetos.Workflow;
using Engrama.Share.PostClass.AgenteEngrama.Engrama.Share.PostModels.AgenteEngrama;
using Engrama.Share.PostClass.WorkFlow;

using EngramaCoreStandar.Mapper;
using EngramaCoreStandar.Results;

using Newtonsoft.Json;

using System.Text;

namespace Engrama.API.EngramaLevels.Dominio.Core
{
	public class WorkFlowDominio : IWorkFlowDominio
	{

		private readonly IDataBaseRepository dataBaseRepository;
		private readonly IWorkFlowRepository workPlanRepository;
		private readonly IAgentOrchestrationService agentOrchestrationService; // INYECTADO
		private readonly MapperHelper mapper;
		private readonly IResponseHelper responseHelper;

		/// <summary>
		/// Inicializa las dependencias, incluyendo el orquestador de agentes.
		/// </summary>
		public WorkFlowDominio(IDataBaseRepository dataBaseRepository,
			MapperHelper mapper,
			IResponseHelper responseHelper,
			IWorkFlowRepository workPlanRepository,
			IAgentOrchestrationService agentOrchestrationService) // NUEVO PARÁMETRO
		{
			this.dataBaseRepository = dataBaseRepository;
			this.mapper = mapper;
			this.responseHelper = responseHelper;
			this.workPlanRepository = workPlanRepository;
			this.agentOrchestrationService = agentOrchestrationService; // ASIGNACIÓN
		}

		public async Task<Response<WorkPlan>> GetWorkPlanById(int iIdWorkPlan)
		{
			try
			{
				var model = new spGetWorkPlan.Request { iIdWorkPlan = iIdWorkPlan };
				var result = await workPlanRepository.spGetWorkPlan(model);
				var validation = responseHelper.Validacion<spGetWorkPlan.Result, WorkPlan>(result);

				return validation;
			}
			catch (Exception ex)
			{
				return Response<WorkPlan>.BadResult(ex.Message, new());
			}
		}

		/// <summary>
		/// Gestiona la generación inicial o la iteración de la descripción con la IA y la persiste.
		/// </summary>
		public async Task<Response<WorkPlan>> GenerateWorkPlan(PostGenerateWorkPlan postModel)
		{
			var validation = new Response<WorkPlan>();
			WorkPlan existingPlan = null;

			// 1. Cargar plan existente si estamos iterando (iIdWorkPlan > 0)
			if (postModel.iIdWorkPlan > 0)
			{
				var existingResponse = await GetWorkPlanById(postModel.iIdWorkPlan);
				if (existingResponse.IsSuccess)
				{
					existingPlan = existingResponse.Data;
				}
			}

			bool isIterative = existingPlan != null;
			string existingDescription = isIterative ? existingPlan.vchImprovedDescription : "";

			// 2. LLAMADA AL AGENTE DE ORQUESTACIÓN
			var agentPostModel = new PostAskAgent
			{
				nvchUserId = postModel.iIdUser.ToString(),
				vchPrompt = CreatePromptWorkPlan(postModel, isIterative, existingDescription)
			};

			var agentResponse = await agentOrchestrationService.ProcessUserQueryAsync(agentPostModel);
			string Response = agentResponse.vchResponse;

			try
			{
				// 3. Deserialización y re-estructuración
				// ... (lógica de limpieza de JSON)
				//int start = jsonResponse.IndexOf('{');
				//int end = jsonResponse.LastIndexOf('}') + 1;
				//string cleanJson = (start >= 0 && end > start)
				//					? jsonResponse.Substring(start, end - start)
				//					: jsonResponse;

				//// Aquí la IA SOLO devuelve el campo vchImprovedDescription
				//var partialPlan = JsonConvert.DeserializeObject<WorkPlan>(cleanJson);

				//if (partialPlan == null)
				//	return Response<WorkPlan>.BadResult("La IA devolvió un formato de plan inválido.", new());

				// 4. Consolidación de datos
				WorkPlan workPlanToSave = existingPlan ?? new WorkPlan();

				workPlanToSave.iIdWorkPlan = postModel.iIdWorkPlan;
				workPlanToSave.iIdUser = postModel.iIdUser;
				workPlanToSave.vchInitialDescription = postModel.Description; // Lo que el usuario escribió AHORA
				workPlanToSave.vchImprovedDescription = Response; // Lo que la IA MEJORÓ

				// 5. Persistencia
				var saveResult = await SavePlanToDatabase(workPlanToSave, postModel.Description, postModel.iIdUser);

				if (saveResult.IsSuccess)
				{
					validation.Data = saveResult.Data;
					validation.IsSuccess = true;
				}
				else
				{
					return Response<WorkPlan>.BadResult(saveResult.Message, new());
				}
			}
			catch (Exception ex)
			{
				return Response<WorkPlan>.BadResult($"Error de procesamiento de IA o persistencia: {ex.Message}", new());
			}

			return validation;
		}
		/// <summary>
		/// Lógica para guardar o actualizar el WorkPlan en la base de datos.
		/// </summary>
		private async Task<Response<WorkPlan>> SavePlanToDatabase(WorkPlan workPlan, string initialDescription, int userId)
		{
			var response = new Response<WorkPlan>();

			string fullPlanJson = JsonConvert.SerializeObject(workPlan);

			var saveModel = new spSaveWorkPlan.Request
			{
				iIdWorkPlan = workPlan.iIdWorkPlan,
				iIdUser = userId,
				vchInitialDescription = initialDescription,
				vchImprovedDescription = workPlan.vchImprovedDescription,
				vchFullPlanJSON = fullPlanJson
			};

			var saveResult = await workPlanRepository.spSaveWorkPlan(saveModel);
			var validation = responseHelper.Validacion<spSaveWorkPlan.Result, WorkPlan>(saveResult);

			if (validation.IsSuccess)
			{
				workPlan.iIdWorkPlan = validation.Data.iIdWorkPlan;
				response.IsSuccess = true;
				response.Data = workPlan;
			}
			else
			{
				response.IsSuccess = false;
				response.Message = validation.Message;
				response.Data = workPlan;
			}

			return response;
		}

		public async Task<Response<WorkPlan>> UpdateWorkPlanDetails(PostUpdateWorkPlanDetails postModel)
		{
			// Reutiliza la lógica de guardado
			return await SavePlanToDatabase(postModel.workPlan, postModel.workPlan.vchInitialDescription, postModel.workPlan.iIdUser);
		}


		// Prompt para la llamada a Gemini (Refinado para la iteración de descripción)
		private string CreatePromptWorkPlan(PostGenerateWorkPlan postModel, bool isIterative = false, string existingDescription = "")
		{
			var prompt = new StringBuilder();
			prompt.AppendLine("Eres un asistente experto en arquitectura de software.");
			prompt.AppendLine("Tu ÚNICA TAREA es analizar la descripción y mejorarla, rellenando huecos como seguridad, pruebas, y convenciones de Engrama.");

			if (isIterative)
			{
				prompt.AppendLine("El usuario está editando la descripción por ITERACIÓN. Considera la siguiente descripción mejorada ANTERIOR: [" + existingDescription + "]. La nueva solicitud del usuario para modificarla es:");
			}

			prompt.AppendLine("\nNueva descripción o cambios del usuario: '" + postModel.Description + "'");
			prompt.AppendLine("Solo dame la nueva descripción sin comentarios ni nada, la pura descripción con el máximo detalle posible.");

			return prompt.ToString();
		}

	}
}
