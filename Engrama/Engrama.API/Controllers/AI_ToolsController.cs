using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.Share.PostClass.AI_Tools;

using Microsoft.AspNetCore.Mvc;

namespace Engrama.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AI_ToolsController : ControllerBase
	{
		private readonly IAI_ToolsDominio aI_ToolsDominio;

		public AI_ToolsController(IAI_ToolsDominio aI_ToolsDominio)
		{
			this.aI_ToolsDominio = aI_ToolsDominio;
		}


		/// <summary>
		/// Send the tables and the promt to crate sql code to ejecute in the database
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostCreateCodeByTables")]
		public async Task<IActionResult> PostCreateCodeByTables([FromBody] PostCreateCodeByTables postModel)
		{

			var result = await aI_ToolsDominio.CreateCodeByTables(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}



		/// <summary>
		/// Sent the promt to create a new table using AI
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostCreateNewTableIA")]
		public async Task<IActionResult> PostCreateNewTableIA([FromBody] PostCreateNewTableIA postModel)
		{
			var result = await aI_ToolsDominio.CreateNewTable(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}


		/// <summary>
		/// Sent the exisiting Procedure and impreve it.
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostImproveSPwithAI")]
		public async Task<IActionResult> PostImproveSPwithAI([FromBody] PostImproveSPwithAI postModel)
		{
			var result = await aI_ToolsDominio.ImproveSPwithAI(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}


		/// <summary>
		/// Improve the C# codo and improve it.
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostImproveCSharpCode")]
		public async Task<IActionResult> PostImproveCSharpCode([FromBody] PostImproveCSharpCode postModel)
		{
			var result = await aI_ToolsDominio.ImproveCSharpCodeAI(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}
	}
}
