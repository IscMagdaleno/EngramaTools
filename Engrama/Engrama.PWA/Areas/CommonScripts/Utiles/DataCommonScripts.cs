using Engrama.Share.Objetos;
using Engrama.Share.Objetos.CommonScripts;
using Engrama.Share.PostClass;
using Engrama.Share.PostClass.CommonScritps;

using EngramaCoreStandar.Dapper.Results;
using EngramaCoreStandar.Mapper;
using EngramaCoreStandar.Results;
using EngramaCoreStandar.Servicios;

namespace Engrama.PWA.Areas.CommonScripts.Utiles
{
	public class DataCommonScripts
	{

		private string url = @"api/CommonScripts";

		private readonly IHttpService httpService;
		private readonly MapperHelper mapperHelper;
		private readonly IValidaServicioService validaServicioService;

		public CommonScript CommonScriptsSelected { get; set; }
		public IList<CommonScript> LstCommonScripts { get; set; }
		public IList<Catalogue> LstCatProyectType { get; set; }

		public DataCommonScripts(IHttpService httpService, MapperHelper mapperHelper, IValidaServicioService validaServicioService)
		{
			this.httpService = httpService;
			this.mapperHelper = mapperHelper;
			this.validaServicioService = validaServicioService;
			LstCommonScripts = new List<CommonScript>();
			LstCatProyectType = new List<Catalogue>();

			CommonScriptsSelected = new CommonScript();
		}


		public async Task<SeverityMessage> PostGetCommonScripts()
		{
			var APIUrl = url + "/PostGetCommonScripts";

			var model = new PostGetCommonScripts();

			var response = await httpService.Post<PostGetCommonScripts, Response<List<CommonScript>>>(APIUrl, model);
			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => LstCommonScripts = data);


			return validacion;

		}

		public async Task<SeverityMessage> PostGetCatalogue()
		{
			var APIUrl = url + "/PostGetCatalogue";

			var model = new PostGetCatalogue()
			{
				vchCatalogueName = "CatProyectType"
			};

			var response = await httpService.Post<PostGetCatalogue, Response<List<Catalogue>>>(APIUrl, model);
			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => LstCatProyectType = data);
			return validacion;



		}
		public async Task<SeverityMessage> PostSaveCommonScripts()
		{
			var APIUrl = url + "/PostSaveCommonScripts";

			CommonScriptsSelected.iIdCatProyectType = CommonScriptsSelected.CatProyectType.iId;

			var model = mapperHelper.Get<CommonScript, PostSaveCommonScripts>(CommonScriptsSelected);
			var response = await httpService.Post<PostSaveCommonScripts, Response<CommonScript>>(APIUrl, model);
			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => AfterSaveCommonScript(data));

			return validacion;

		}


		private void AfterSaveCommonScript(CommonScript data)
		{
			CommonScriptsSelected = data;

			LstCommonScripts.Add(data);
		}

	}
}
