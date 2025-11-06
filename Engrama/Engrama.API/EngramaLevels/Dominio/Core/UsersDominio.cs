using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.API.EngramaLevels.Infrastructure.Interfaces;
using Engrama.Share.Entity;
using Engrama.Share.Objetos;
using Engrama.Share.PostClass;

using EngramaCoreStandar.Mapper;
using EngramaCoreStandar.Results;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Engrama.API.EngramaLevels.Dominio.Core
{
	public class UsersDominio : IUsersDominio
	{
		private readonly IUsersRepository usersRepository;
		private readonly MapperHelper mapper;
		private readonly IResponseHelper responseHelper;
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Initialize the fields receiving the interfaces on the builder
		/// </summary>
		public UsersDominio(IUsersRepository usersRepository,
			MapperHelper mapper,
			IResponseHelper responseHelper,
			IConfiguration configuration)
		{
			this.usersRepository = usersRepository;
			this.mapper = mapper;
			this.responseHelper = responseHelper;
			_configuration = configuration;
		}



		public async Task<Response<User>> Login(PostValidateCredentials DAOmodel)
		{
			try
			{
				// Map the input DAOmodel of type PostValidateCredentials to the spLogin.Request type
				var model = mapper.Get<PostValidateCredentials, spLogin.Request>(DAOmodel);
				var result = await usersRepository.spLogin(model);
				var validated = responseHelper.Validacion<spLogin.Result, User>(result);

				if (validated.IsSuccess)
				{
					var tmpUserRol = CreateRoleFromResult(result);
					validated.Data.Rol = tmpUserRol;
				}

				return validated;
			}
			catch (Exception ex)
			{
				return Response<User>.BadResult(ex.Message, new());
			}
		}

		// Method to create a Role object from the spLogin result
		private Role CreateRoleFromResult(spLogin.Result result)
		{
			// Initialize and return a Role object using the information from result
			return new Role()
			{
				iIdRoles = result.iIdRoles,
				vchName = result.vchNameRole,
				bStatus = true // Assuming 'true' implies the role is active or valid
			};
		}


		public async Task<Response<User>> SaveUsers(PostSaveUsers DAOmodel)
		{
			try
			{
				var model = mapper.Get<PostSaveUsers, spSaveUsers.Request>(DAOmodel);

				var result = await usersRepository.spSaveUsers(model);
				var validation = responseHelper.Validacion<spSaveUsers.Result, User>(result);
				if (validation.IsSuccess)
				{
					DAOmodel.iIdUsers = validation.Data.iIdUsers;
					validation.Data = mapper.Get<PostSaveUsers, User>(DAOmodel);
				}
				return validation;

			}
			catch (Exception ex)
			{
				return Response<User>.BadResult(ex.Message, new());
			}
		}



		public Response<AuthenticationModel> BuildToken(User users)
		{
			var claims = new List<Claim>()
			{
				new Claim("id",users.iIdUsers.ToString()),
				new Claim("email",users.vchEmail),
				new Claim("name",users.vchName),
				new Claim(ClaimTypes.Role,users.Rol.vchName),
				new Claim("iIdRol",users.Rol.iIdRoles.ToString()),
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["keyJWT"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


			var expiracion = DateTime.UtcNow.AddHours(8);

			var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims
				, expires: expiracion, signingCredentials: creds);

			var model = new AuthenticationModel
			{
				vchToken = new JwtSecurityTokenHandler().WriteToken(securityToken),
				dtExpiration = expiracion,
				iIdUsers = users.iIdUsers,

			};
			var response = new Response<AuthenticationModel>();
			response.IsSuccess = true;
			response.Message = string.Empty;
			response.Data = model;

			return response;
		}
	}
}
