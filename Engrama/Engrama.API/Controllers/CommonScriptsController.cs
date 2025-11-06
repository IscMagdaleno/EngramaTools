using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.Share.PostClass;
using Engrama.Share.PostClass.CommonScritps;

using Microsoft.AspNetCore.Mvc;

namespace Engrama.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CommonScriptsController : ControllerBase
	{
		private readonly ICommonScriptsDominio commonScriptsDominio;

		public CommonScriptsController(ICommonScriptsDominio commonScriptsDominio)
		{
			this.commonScriptsDominio = commonScriptsDominio;
		}

		/// <summary>
		/// Save the new common script into the database
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostSaveCommonScripts")]
		public async Task<IActionResult> PostSaveCommonScripts([FromBody] PostSaveCommonScripts postModel)
		{
			var result = await commonScriptsDominio.SaveCommonScripts(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		/// <summary>
		/// Get all the registered common scripts from the database
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostGetCommonScripts")]
		public async Task<IActionResult> PostGetCommonScripts([FromBody] PostGetCommonScripts postModel)
		{
			var result = await commonScriptsDominio.GetCommonScripts(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}


		/// <summary>
		/// Get all the info of one table type cataloge
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostGetCatalogue")]
		public async Task<IActionResult> PostGetCatalogue([FromBody] PostGetCatalogue postModel)
		{
			var result = await commonScriptsDominio.GetCatalogue(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}



	}
}
