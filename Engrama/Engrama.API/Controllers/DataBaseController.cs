using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.Share.PostClass;
using Engrama.Share.PostClass.DataBase;

using Microsoft.AspNetCore.Mvc;

namespace Engrama.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DataBaseController : ControllerBase
	{
		private readonly IDataBaseDominio dataBaseDominio;

		public DataBaseController(IDataBaseDominio dataBaseDominio)
		{
			this.dataBaseDominio = dataBaseDominio;
		}


		/// <summary>
		/// Save and update the connection string from the database
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostSaveConnectionString")]
		public async Task<IActionResult> PostSaveConnectionString([FromBody] PostSaveConnectionString postModel)
		{
			var result = await dataBaseDominio.ProcesoConnectionString(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}


		/// <summary>
		/// Get all the connection string from the database
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostGetConnectionString")]
		public async Task<IActionResult> PostGetConnectionString([FromBody] PostGetConnectionString postModel)
		{
			var result = await dataBaseDominio.GetConnectionString(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

		/// <summary>
		/// Get all the tables how the database work
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		/// <remarks>
		/// {
		///		"ConnectionString":"Data Source =Engrama.mssql.somee.com;Initial Catalog=Engrama;User ID=MMartinez_SQLLogin_1;Password=95xodkhgxa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
		/// }
		/// </remarks>
		[HttpPost("PostGetTables")]
		public async Task<IActionResult> PostGetTables([FromBody] PostGetTables postModel)
		{
			var result = await dataBaseDominio.GetTables(postModel);

			if (result.IsSuccess)
			{
				return Ok(result);
			}

			return BadRequest(result);
		}


		/// <summary>
		/// Get all the Stored Procedures saved on the database
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		/// <remarks>
		/// {
		///		"ConnectionString ":"Data Source =Engrama.mssql.somee.com;Initial Catalog=Engrama;User ID=MMartinez_SQLLogin_1;Password=95xodkhgxa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
		/// }
		/// </remarks>
		[HttpPost("PostGetAllStoredProcedures")]
		public async Task<IActionResult> PostGetAllStoredProcedures([FromBody] PostGetAllStoredProcedures postModel)
		{
			var result = await dataBaseDominio.GetAllStoredProcedures(postModel);

			if (result.IsSuccess)
			{
				return Ok(result);
			}

			return BadRequest(result);
		}



		/// <summary>
		/// Get all the Stored Procedures saved on the database and his details
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		/// <remarks>
		/// {
		///		"ConnectionString ":"Data Source =Engrama.mssql.somee.com;Initial Catalog=Engrama;User ID=MMartinez_SQLLogin_1;Password=95xodkhgxa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
		/// }
		/// </remarks>
		[HttpPost("PostGetAllStoredProcedureDetails")]
		public async Task<IActionResult> PostGetAllStoredProcedureDetails([FromBody] PostGetAllStoredProcedureDetails postModel)
		{
			var result = await dataBaseDominio.GetAllStoredProcedureDetails(postModel);

			if (result.IsSuccess)
			{
				return Ok(result);
			}

			return BadRequest(result);
		}


		/// <summary>
		/// Get the details of the Stored procedure selected
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		/// <remarks>
		/// {
		///		"ConnectionString":"Data Source =Engrama.mssql.somee.com;Initial Catalog=Engrama;User ID=MMartinez_SQLLogin_1;Password=95xodkhgxa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
		///		"vchName":"spLogin"
		///
		/// }
		/// </remarks>
		[HttpPost("PostGetStoreProcedure")]
		public async Task<IActionResult> PostGetStoreProcedure([FromBody] PostGetStoreProcedure postModel)
		{
			var result = await dataBaseDominio.GetStoreProcedure(postModel);

			if (result.IsSuccess)
			{
				return Ok(result);
			}

			return BadRequest(result);
		}


		///// <summary>
		///// Send the SP code to analize in Chat GPT to get back the code with coments and the code improved
		///// </summary>
		///// <param name="postModel"></param>
		///// <returns></returns>
		//[HttpPost("PostAnalizeSP")]
		//public async Task<IActionResult> PostAnalizeSP([FromBody] PostAnalyzeSP postModel)
		//{
		//	var result = await dataBaseDominio.AnalyzeSP(postModel);
		//	if (result.IsSuccess)
		//	{
		//		return Ok(result);
		//	}
		//	return BadRequest(result);
		//}


		///// <summary>
		///// Sent the promt to create a new table using AI
		///// </summary>
		///// <param name="postModel"></param>
		///// <returns></returns>
		//[HttpPost("PostCreateNewTableIA")]
		//public async Task<IActionResult> PostCreateNewTableIA([FromBody] PostCreateNewTableIA postModel)
		//{
		//	var result = await dataBaseDominio.CreateNewTable(postModel);
		//	if (result.IsSuccess)
		//	{
		//		return Ok(result);
		//	}
		//	return BadRequest(result);
		//}


		/// <summary>
		/// Retunr al the stored procedures searched by work key
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostGetProceduresByKey")]
		public async Task<IActionResult> PostGetProceduresByKey([FromBody] PostGetItemByKey postModel)
		{
			var result = await dataBaseDominio.GetProceduresByKey(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}


		/// <summary>
		/// Retunr al the tables searched by work key
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostGetTablesByKey")]
		public async Task<IActionResult> PostGetTablesByKey([FromBody] PostGetItemByKey postModel)
		{
			var result = await dataBaseDominio.GetTablesByKey(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}
	}
}
