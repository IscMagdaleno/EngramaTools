using Engrama.PWA.Areas.DataBase.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.DataBase.Components
{
	public partial class GridTables : EngramaComponent
	{


		[Parameter] public MainDataBase Data { get; set; }
		public bool bShowDetails { get; set; }
		protected override void OnInitialized()
		{
			bShowDetails = false;
		}

		protected override async Task OnInitializedAsync()
		{
			Loading.Show();
			var result = await Data.PostGetTables();
			ShowSnake(result);
			Loading.Hide();
		}


		private void OnTablaSelected(Table table)
		{
			Data.TableSelected = table;
			bShowDetails = true;
		}

	}
}
