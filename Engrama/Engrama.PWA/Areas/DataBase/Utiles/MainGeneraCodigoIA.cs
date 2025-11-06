using Engrama.Share.Objetos.AI_Tools;
using Engrama.Share.PostClass.AI_Tools;
using Engrama.Share.PostClass.DataBase;

using EngramaCoreStandar.Dapper.Results;
using EngramaCoreStandar.Mapper;
using EngramaCoreStandar.Results;
using EngramaCoreStandar.Servicios;

namespace Engrama.PWA.Areas.DataBase.Utiles
{
	public class MainGeneraCodigoIA
	{
		private string url = @"api/DataBase";

		private readonly IHttpService httpService;
		private readonly MapperHelper mapperHelper;
		private readonly IValidaServicioService validaServicioService;

		public string Description { get; set; }
		public IList<ChatMessage> LstCodeByIA { get; set; }

		public MainGeneraCodigoIA(IHttpService httpService, MapperHelper mapperHelper, IValidaServicioService validaServicioService)
		{
			this.httpService = httpService;
			this.mapperHelper = mapperHelper;
			this.validaServicioService = validaServicioService;
			LstCodeByIA = new List<ChatMessage>();
			Description = string.Empty;
		}

		public async Task<SeverityMessage> PostCreateNewTableIA()
		{
			var chat = new ChatMessage("user", Description);
			LstCodeByIA.Add(chat);

			var APIUrl = url + "/PostCreateNewTableIA";

			var model = new PostCreateNewTableIA()
			{
				PromptCreation = Description
			};

			var response = await httpService.Post<PostCreateNewTableIA, Response<CodeByIA>>(APIUrl, model);
			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => AfterGetCodigoIA(data));
			return validacion;


		}

		private void AfterGetCodigoIA(CodeByIA codeByIA)
		{

			var chat = new ChatMessage("IA", codeByIA.Code);
			LstCodeByIA.Add(chat);
			Description = string.Empty;
		}



	}
}
