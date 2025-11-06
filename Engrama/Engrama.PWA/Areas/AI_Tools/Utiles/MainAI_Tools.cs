using Engrama.PWA.Areas.DataBase.Utiles;
using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.Share.Objetos.AI_Tools;
using Engrama.Share.Objetos.DataBaseArea;
using Engrama.Share.PostClass.AI_Tools;

using EngramaCoreStandar.Dapper.Results;
using EngramaCoreStandar.Extensions;
using EngramaCoreStandar.Mapper;
using EngramaCoreStandar.Results;
using EngramaCoreStandar.Servicios;

namespace Engrama.PWA.Areas.AI_Tools.Utiles
{
	public class MainAI_Tools
	{
		private string url = @"api/AI_Tools";
		private string urlDB = @"api/DataBase";

		private readonly IHttpService httpService;
		private readonly UserSession userSession;
		private readonly IValidaServicioService validaServicioService;
		private readonly MapperHelper mapperHelper;


		public MainDataBase MainDataBase { get; set; }

		public StoredProcedure StoredProcedureSelected { get; set; }
		public IList<Table> LstTablesSelected { get; set; }

		public IList<ChatMessage> LstCodeByIA { get; set; }

		public string Description { get; set; }

		public MainAI_Tools(IHttpService httpService, MapperHelper mapperHelper, IValidaServicioService validaServicioService, UserSession userSession)
		{
			this.httpService = httpService;
			this.userSession = userSession;
			this.validaServicioService = validaServicioService;

			MainDataBase = new MainDataBase(httpService, validaServicioService, userSession);

			LstTablesSelected = new List<Table>();
			LstCodeByIA = new List<ChatMessage>();
			Description = string.Empty;
			StoredProcedureSelected = new StoredProcedure();
		}



		public void AddTableAtList(Table table)
		{
			if (LstTablesSelected.NotAny(x => x.iIdTable == table.iIdTable))
			{
				LstTablesSelected.Add(table);
			}

		}

		public void RemoveTableAtList(Table table)
		{
			LstTablesSelected = LstTablesSelected.Where(x => x.iIdTable != table.iIdTable).ToList();

		}



		public async Task<SeverityMessage> PostCreateCodeByTables()
		{
			var chat = new ChatMessage("user", Description);
			LstCodeByIA.Add(chat);

			var APIUrl = url + "/PostCreateCodeByTables";
			var neuvaLista = new List<Table>();
			foreach (var item in LstTablesSelected.ToList())
			{
				item.BuildedCodes = new List<BuildedCode>();
				neuvaLista.Add(item);
			}

			var model = new PostCreateCodeByTables()
			{
				LstTablas = neuvaLista,
				Prompt = Description
			};

			var response = await httpService.Post<PostCreateCodeByTables, Response<ResultOpenAI>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => AfterGetCodigoIA(data));


			return validacion;

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

			var response = await httpService.Post<PostCreateNewTableIA, Response<ResultOpenAI>>(APIUrl, model);
			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => AfterGetCodigoIA(data));
			return validacion;


		}

		public async Task<SeverityMessage> PostImproveSPwithAI()
		{
			var chat = new ChatMessage("user", Description);
			LstCodeByIA.Add(chat);

			var APIUrl = url + "/PostImproveSPwithAI";

			var neuvaLista = new List<Table>();
			foreach (var item in LstTablesSelected.ToList())
			{
				item.BuildedCodes = new List<BuildedCode>();
				neuvaLista.Add(item);
			}

			var model = new PostImproveSPwithAI()
			{
				LstTablas = neuvaLista,
				StoredProcedure = StoredProcedureSelected,
				Prompt = Description
			};

			var response = await httpService.Post<PostImproveSPwithAI, Response<ResultOpenAI>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => AfterGetCodigoIA(data));


			return validacion;

		}



		public async Task<SeverityMessage> PostImproveCSharpCode(string CodigoCSharp)
		{
			var chat = new ChatMessage("user", Description);
			LstCodeByIA.Add(chat);

			var APIUrl = url + "/PostImproveCSharpCode";

			var model = new PostImproveCSharpCode()
			{
				CSharpCode = CodigoCSharp,
				Prompt = Description
			};

			var response = await httpService.Post<PostImproveCSharpCode, Response<ResultOpenAI>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => AfterGetCodigoIA(data));


			return validacion;

		}



		private void AfterGetCodigoIA(ResultOpenAI codeByIA)
		{

			var chat = new ChatMessage("IA", codeByIA.ResultaAI);
			LstCodeByIA.Add(chat);
			Description = string.Empty;
		}
	}
}
