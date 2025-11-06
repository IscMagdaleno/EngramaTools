using Engrama.PWA.Areas.DataBase.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.DataBase.Components
{
	public partial class TableConnectios : EngramaComponent
	{
		[Parameter] public MainDataBase Data { get; set; }

		[Parameter] public bool bDataBaseModule { get; set; }

		[Parameter] public EventCallback<ConnectionString> OnConnectionStringSelected { get; set; }


		protected override void OnInitialized()
		{

		}
		protected override async Task OnInitializedAsync()
		{
			Loading.Show();
			await Data.PostGetConnectionString(bDataBaseModule);
			Loading.Hide();
		}


		private async void OnSelectRow(ConnectionString connectionString)
		{
			await OnConnectionStringSelected.InvokeAsync(connectionString);
		}

	}
}
