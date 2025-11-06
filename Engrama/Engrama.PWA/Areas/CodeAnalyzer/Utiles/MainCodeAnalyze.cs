using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.Share.Objetos.AnalyzeCodeArea;
using Engrama.Share.PostClass.CodeAnalyze;

using EngramaCoreStandar.Dapper.Results;
using EngramaCoreStandar.Results;
using EngramaCoreStandar.Servicios;

namespace Engrama.PWA.Areas.CodeAnalyzer.Utiles
{
	public class MainCodeAnalyze
	{

		private string url = @"api/CodeAnalyze";

		private readonly IHttpService httpService;
		private readonly UserSession userSession;
		private readonly IValidaServicioService validaServicioService;



		public CodeAnalyze CodeAnalyzed { get; set; }
		public IList<CodeAnalyze> LstCodeAnalyzed { get; set; }

		public MainCodeAnalyze(IHttpService httpService, IValidaServicioService validaServicioService, UserSession userSession)
		{
			this.httpService = httpService;
			this.userSession = userSession;
			this.validaServicioService = validaServicioService;

			CodeAnalyzed = new CodeAnalyze();
			LstCodeAnalyzed = new List<CodeAnalyze>();
		}


		public async Task<SeverityMessage> PostAnalyzeCSharp()
		{
			var APIUrl = url + "/PostAnalyzeCSharp";

			var model = new PostAnalyzeCSharp()
			{
				vchCSharpCode = CodeAnalyzed.vchCode
			};

			var response = await httpService.Post<PostAnalyzeCSharp, Response<CodeAnalyze>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => CodeAnalyzed = data);


			return validacion;

		}


	}
}
