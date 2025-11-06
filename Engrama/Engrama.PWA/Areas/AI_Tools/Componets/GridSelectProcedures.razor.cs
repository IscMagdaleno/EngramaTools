using Engrama.PWA.Areas.DataBase.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using EngramaCoreStandar.Extensions;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.AI_Tools.Componets
{
	public partial class GridSelectProcedures : EngramaComponent
	{
		[Parameter] public MainDataBase DataBase { get; set; }
		[Parameter] public EventCallback<StoredProcedure> EC_StoredProcedureSelected { get; set; }
		[Parameter] public EventCallback OnConnectionSelected { get; set; }


		public bool bConnectionStringTable { get; set; }
		public bool Expand { get; set; }
		protected override async Task OnInitializedAsync()
		{
			bConnectionStringTable = true;
			Expand = true;

			if (DataBase.ConnectionStringSelected.vchConnectionString.NotEmpty())
			{
				bConnectionStringTable = false;
			}
		}


		private async Task OnConnectionStringSelected(ConnectionString connectionString)
		{
			Loading.Show();

			DataBase.SetConnectionString(connectionString);
			bConnectionStringTable = false;
			await DataBase.PostGetAllStoredProcedures();

			await OnConnectionSelected.InvokeAsync();
			Loading.Hide();
		}

		private async Task OnStoredProcedureSelected(StoredProcedure storedProcedure)
		{
			Loading.Show();

			var result = await DataBase.SetStoredProceduresSelected(storedProcedure);
			ShowSnake(result);
			if (result.bResult)
			{
				await EC_StoredProcedureSelected.InvokeAsync(DataBase.StoredProcedureSelected);
			}
			Loading.Hide();
		}
	}
}
