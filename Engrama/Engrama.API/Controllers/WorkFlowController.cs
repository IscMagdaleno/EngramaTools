using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.Share.PostClass.WorkFlow;

using Microsoft.AspNetCore.Mvc;

namespace Engrama.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WorkFlowController : ControllerBase
	{
		private readonly IWorkFlowDominio workFlowDominio;

		public WorkFlowController(IWorkFlowDominio workFlowDominio)
		{
			this.workFlowDominio = workFlowDominio;
		}


		/// <summary>
		/// Se generan planes de trabajo basándose en una descripción
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostGenerateWorkPlan")]
		public async Task<IActionResult> PostGenerateWorkPlan([FromBody] PostGenerateWorkPlan postModel)
		{

			var result = await workFlowDominio.GenerateWorkPlan(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		/// <summary>
		/// Se actualizan los planes de trabajo
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostUpdateWorkPlanDetails")]
		public async Task<IActionResult> PostUpdateWorkPlanDetails([FromBody] PostUpdateWorkPlanDetails postModel)
		{

			var result = await workFlowDominio.UpdateWorkPlanDetails(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}


		/// <summary>
		/// Obtiene un plan de trabajo existente por ID para edición.
		/// </summary>
		/// <param name="postModel">Modelo con el ID del WorkPlan.</param>
		[HttpPost("PostGetWorkPlanById")]
		public async Task<IActionResult> PostGetWorkPlanById([FromBody] PostGetWorkPlanById postModel)
		{
			var result = await workFlowDominio.GetWorkPlanById(postModel.iIdWorkPlan);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

	}
}
