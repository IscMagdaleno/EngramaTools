using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.Share.Objetos.AnalyzeCodeArea;
using Engrama.Share.Objetos.DataBaseArea;
using Engrama.Share.PostClass;
using Engrama.Share.PostClass.DataBase;

using EngramaCoreStandar.Dapper.Results;
using EngramaCoreStandar.Results;
using EngramaCoreStandar.Servicios;

namespace Engrama.PWA.Areas.DataBase.Utiles
{
	public class MainDataBase
	{
		private string url = @"api/DataBase";

		private readonly IHttpService httpService;
		private readonly IValidaServicioService validaServicioService;

		private readonly UserSession userSession;

		public ConnectionString ConnectionStringSelected { get; set; }
		public IList<ConnectionString> LstConnectionString { get; set; }

		public IList<Table> LstTables { get; set; }
		public Table TableSelected { get; set; }

		public IList<StoredProcedure> LstStoredProcedures { get; set; }
		public StoredProcedure StoredProcedureSelected { get; set; }

		public string vchKeySerch { get; set; }
		public MainDataBase(IHttpService httpService, IValidaServicioService validaServicioService, UserSession userSession)
		{
			this.httpService = httpService;
			this.validaServicioService = validaServicioService;
			this.userSession = userSession;

			ConnectionStringSelected = new ConnectionString();
			LstConnectionString = new List<ConnectionString>();

			LstTables = new List<Table>();
			TableSelected = new Table();

			LstStoredProcedures = new List<StoredProcedure>();
			StoredProcedureSelected = new StoredProcedure();

			vchKeySerch = string.Empty;
		}

		public void SetConnectionString(ConnectionString connectionString)
		{
			ConnectionStringSelected = connectionString;
		}


		public async Task<SeverityMessage> PostGetConnectionString(bool bActivo = false)
		{
			var APIUrl = url + "/PostGetConnectionString";

			var model = new PostGetConnectionString()
			{
				iIdUser = userSession.iId,
				bActivo = bActivo
			};

			var response = await httpService.Post<PostGetConnectionString, Response<List<ConnectionString>>>(APIUrl, model);
			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => LstConnectionString = data);
			return validacion;


		}

		public async Task<SeverityMessage> PostSaveConnectionString()

		{
			var APIUrl = url + "/PostSaveConnectionString";

			ConnectionStringSelected.iIdUser = userSession.iId;
			var model = new PostSaveConnectionString
			{
				iIdConnectionString = ConnectionStringSelected.iIdConnectionString,
				vchConnectionString = ConnectionStringSelected.vchConnectionString,
				vchNota = ConnectionStringSelected.vchNota,
				bActivo = ConnectionStringSelected.bActivo,
				iIdUser = ConnectionStringSelected.iIdUser
			};
			var response = await httpService.Post<PostSaveConnectionString, Response<ConnectionString>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response, onSuccess: data => ConnectionStringSelected = data);
			return validacion;
			//			return new SeverityMessage(true, "");

		}


		public async Task<SeverityMessage> PostGetTables()
		{
			var APIUrl = url + "/PostGetTables";
			var model = new PostGetTables() { ConnectionString = ConnectionStringSelected.vchConnectionString };
			var response = await httpService.Post<PostGetTables, Response<List<Table>>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => LstTables = data);
			return validacion;


		}

		public async Task<SeverityMessage> PostGetAllStoredProcedures()
		{
			var APIUrl = url + "/PostGetAllStoredProcedures";
			var model = new PostGetAllStoredProcedures() { ConnectionString = ConnectionStringSelected.vchConnectionString };
			var response = await httpService.Post<PostGetAllStoredProcedures, Response<List<StoredProcedure>>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => LstStoredProcedures = data);
			return validacion;

		}


		public async Task<SeverityMessage> SetStoredProceduresSelected(StoredProcedure storedProcedure)
		{
			StoredProcedureSelected = storedProcedure;

			var APIUrl = url + "/PostGetStoreProcedure";
			var model = new PostGetStoreProcedure() { ConnectionString = ConnectionStringSelected.vchConnectionString, vchName = storedProcedure.vchName };
			var response = await httpService.Post<PostGetStoreProcedure, Response<DetailsStoreProcedure>>(APIUrl, model);


			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => StoredProcedureSelected.Details = data);
			return validacion;

		}


		public async Task<SeverityMessage> PostAnalizeSP()
		{
			var APIUrl = url + "/PostAnalizeSP";

			var model = new PostAnalyzeSP()
			{
				vchStoreProcedure = StoredProcedureSelected.Details.Code
			};

			var response = await httpService.Post<PostAnalyzeSP, Response<CodeAnalyze>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => StoredProcedureSelected.LstAnalizedIA.Add(data));

			return validacion;

		}


		public async Task<SeverityMessage> PostGetAllStoredProcedureDetails()
		{
			var APIUrl = url + "/PostGetAllStoredProcedureDetails";
			var model = new PostGetAllStoredProcedureDetails() { ConnectionString = ConnectionStringSelected.vchConnectionString };
			var response = await httpService.Post<PostGetAllStoredProcedureDetails, Response<List<StoredProcedure>>>(APIUrl, model);

			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => LstStoredProcedures = data);
			return validacion;

		}


		public async Task<SeverityMessage> PostGetProceduresByKey()
		{
			LstStoredProcedures = new List<StoredProcedure>();

			var APIUrl = url + "/PostGetProceduresByKey";
			var model = new PostGetItemByKey() { ConnectionString = ConnectionStringSelected.vchConnectionString, vchKey = vchKeySerch };
			var response = await httpService.Post<PostGetItemByKey, Response<List<StoredProcedure>>>(APIUrl, model);


			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => LstStoredProcedures = data);
			return validacion;


		}





		public async Task<SeverityMessage> GetTablesByKey()
		{
			LstTables = new List<Table>();
			var APIUrl = url + "/PostGetTablesByKey";
			var model = new PostGetItemByKey() { ConnectionString = ConnectionStringSelected.vchConnectionString, vchKey = vchKeySerch };
			var response = await httpService.Post<PostGetItemByKey, Response<List<Table>>>(APIUrl, model);


			var validacion = validaServicioService.ValidadionServicio(response,
			onSuccess: data => LstTables = data);
			return validacion;

		}

	}
}
