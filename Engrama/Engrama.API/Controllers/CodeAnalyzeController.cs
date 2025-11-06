using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.Share.PostClass.CodeAnalyze;

using Microsoft.AspNetCore.Mvc;

namespace Engrama.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CodeAnalyzeController : ControllerBase
	{
		private readonly IAnalyzeCodeDominio analyzeCodeDominio;

		public CodeAnalyzeController(IAnalyzeCodeDominio analyzeCodeDominio)
		{
			this.analyzeCodeDominio = analyzeCodeDominio;
		}

		/// <summary>
		/// Send the SP code to analize in Chat GPT to get back the code with coments and the code improved
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostAnalyzeCSharp")]
		public async Task<IActionResult> PostAnalyzeCSharp([FromBody] PostAnalyzeCSharp postModel)
		{

			//var result = await analyzeCodeDominio.AnalyzeCSharp(postModel);
			//if (result.IsSuccess)
			//{
			//	return Ok(result);
			//}
			return BadRequest();
		}


	}
}

