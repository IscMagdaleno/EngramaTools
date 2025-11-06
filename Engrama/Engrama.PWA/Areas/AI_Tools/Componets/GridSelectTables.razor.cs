using Engrama.PWA.Areas.DataBase.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using EngramaCoreStandar.Extensions;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.AI_Tools.Componets
{
	public partial class GridSelectTables : EngramaComponent
	{
		[Parameter] public MainDataBase DataBase { get; set; }
		[Parameter] public EventCallback<Table> EC_TablaSelected { get; set; }

		public bool bConnectionStringTable { get; set; }
		public bool Expand { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Loading.Show();
			bConnectionStringTable = true;

			if (DataBase.ConnectionStringSelected.vchConnectionString.NotEmpty())
			{
				bConnectionStringTable = false;

				await DataBase.PostGetTables();
			}
			Loading.Hide();
			Expand = true;


		}


		private async Task OnConnectionStringSelected(ConnectionString connectionString)
		{
			Loading.Show();

			DataBase.SetConnectionString(connectionString);
			bConnectionStringTable = false;
			await DataBase.PostGetTables();
			Loading.Hide();
		}

		private async Task OnTablaSelected(Table table)
		{
			await EC_TablaSelected.InvokeAsync(table);
		}
	}
}
