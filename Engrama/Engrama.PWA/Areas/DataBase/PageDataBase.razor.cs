using Engrama.PWA.Areas.DataBase.Utiles;
using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.DataBase
{
	public partial class PageDataBase : EngramaPage
	{

		[Inject] private UserSession UserSession { get; set; }

		public MainDataBase Data { get; set; }

		public bool bConnectionStringTable { get; set; }
		public string vchSearchString { get; set; }

		protected override void OnInitialized()
		{
			Data = new MainDataBase(httpService, validaServicioService, UserSession);

			bConnectionStringTable = true;
		}




		private void OnConnectionStringSelected(ConnectionString connectionString)
		{
			Data.SetConnectionString(connectionString);
			bConnectionStringTable = false;
		}


		private async Task OnClickBuscar()
		{
			Loading.Show();
			await Data.PostGetProceduresByKey();
			await Data.GetTablesByKey();

			bConnectionStringTable = false;
			Loading.Hide();

		}


		private void OnGoBackConeccionString()
		{
			bConnectionStringTable = true;
		}

	}
}
