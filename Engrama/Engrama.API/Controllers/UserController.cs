using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.Share.PostClass;

using Microsoft.AspNetCore.Mvc;

namespace Engrama.API.Controllers
{
	/// <summary>
	/// Test controller to show how Engrama work
	/// </summary>
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{

		private readonly IUsersDominio usersDominio;

		public UserController(IUsersDominio usersDominio)
		{
			this.usersDominio = usersDominio;
		}



		/// <summary>
		/// Login at the APP using the email and the password
		/// </summary>
		/// <param name="postGuarda"></param>
		/// <returns></returns>
		[HttpPost("PostValidateCredentials")]
		public async Task<IActionResult> PostValidateCredentials(PostValidateCredentials postGuarda)
		{
			var result = await usersDominio.Login(postGuarda);
			if (result.IsSuccess)
			{

				var token = usersDominio.BuildToken(result.Data);

				return Ok(token);
			}
			return BadRequest(result);
		}



		/// <summary>
		/// Save the users in the database
		/// </summary>
		/// <param name="postModel"></param>
		/// <returns></returns>
		[HttpPost("PostSaveUsers")]
		public async Task<IActionResult> PostSaveUsers([FromBody] PostSaveUsers postModel)
		{
			var result = await usersDominio.SaveUsers(postModel);
			if (result.IsSuccess)
			{
				return Ok(result);
			}
			return BadRequest(result);
		}

	}
}
