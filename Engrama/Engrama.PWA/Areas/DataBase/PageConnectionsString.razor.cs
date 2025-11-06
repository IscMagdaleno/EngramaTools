using Engrama.PWA.Areas.DataBase.Utiles;
using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using Microsoft.AspNetCore.Components;


namespace Engrama.PWA.Areas.DataBase
{
	public partial class PageConnectionsString : EngramaPage
	{

		[Inject] private UserSession UserSession { get; set; }

		public MainDataBase Data { get; set; }


		protected override void OnInitialized()
		{

			Data = new MainDataBase(httpService, validaServicioService, UserSession);
		}

		protected override async Task OnInitializedAsync()
		{

		}

		private void OnConnectionSaved(ConnectionString connection)
		{

			Data.LstConnectionString.Add(connection);
			Data.ConnectionStringSelected = new ConnectionString();
		}

		public void OnConnectionStringSelected(ConnectionString connectionString)
		{

			Data.ConnectionStringSelected = connectionString;
		}

	}
}
