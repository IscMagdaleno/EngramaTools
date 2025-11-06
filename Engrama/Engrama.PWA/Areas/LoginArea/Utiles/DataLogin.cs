using Engrama.Share.Objetos;
using Engrama.Share.PostClass;

using EngramaCoreStandar.Dapper.Results;
using EngramaCoreStandar.Mapper;
using EngramaCoreStandar.Results;
using EngramaCoreStandar.Servicios;

namespace Engrama.PWA.Areas.LoginArea.Utiles
{
	public class DataLogin
	{

		private string url = @"api/User";

		private readonly IHttpService _httpService;
		private readonly MapperHelper _mapperHelper;
		private readonly IValidaServicioService validaServicioService;

		public Credentials Credentials { get; set; }

		public AuthenticationModel UserAuthenticated { get; set; }

		public User UsersSelected { get; set; }
		public DataLogin(IHttpService httpService, MapperHelper mapperHelper, IValidaServicioService validaServicioService)
		{
			_httpService = httpService;
			_mapperHelper = mapperHelper;
			this.validaServicioService = validaServicioService;
			Credentials = new Credentials();

			/*Credential*/
			//Credentials.vchEmail = "iscmagdaleno@gmail.com";

			UserAuthenticated = new AuthenticationModel();
			UsersSelected = new User();
		}

		public async Task<SeverityMessage> PostValidateCredentials()
		{


			var APIUrl = url + "/PostValidateCredentials";
			var model = new PostValidateCredentials
			{
				vchNickName = Credentials.vchNickName,
				vchPassword = Credentials.vchPassword
			};

			var response = await _httpService.Post<PostValidateCredentials, Response<AuthenticationModel>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => UserAuthenticated = data,
			ContinueWarning: false);
			return validacion;

		}


		public async Task<SeverityMessage> PostSaveUsers()

		{
			var APIUrl = url + "/PostSaveUsers";

			var model = new PostSaveUsers
			{
				iIdRoles = UsersSelected.Rol.iIdRoles,
				vchName = UsersSelected.vchName,
				vchEmail = UsersSelected.vchEmail,
				vchPass = UsersSelected.vchPass,
				bStatus = UsersSelected.bStatus,
				vchNickName = UsersSelected.vchNickName
			};

			var response = await _httpService.Post<PostSaveUsers, Response<User>>(APIUrl, model);
			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => UsersSelected = data);
			return validacion;

		}

	}

}
