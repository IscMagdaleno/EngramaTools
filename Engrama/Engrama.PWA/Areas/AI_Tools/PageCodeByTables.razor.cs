using Engrama.PWA.Areas.AI_Tools.Utiles;
using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.AI_Tools
{
	public partial class PageCodeByTables : EngramaPage
	{
		[Inject] private UserSession UserSession { get; set; }


		public MainAI_Tools Data { get; set; }

		protected override void OnInitialized()
		{
			Data = new MainAI_Tools(httpService, mapperHelper, validaServicioService, UserSession);
		}


		private void EC_TablaSelected(Table table)
		{
			Data.AddTableAtList(table);
		}

		private void CloseChip(Table table)
		{
			Data.RemoveTableAtList(table);
		}


		public async Task OnClickCrearCodigo(string Promt)
		{
			Loading.Show();
			Data.Description = Promt;
			await Data.PostCreateCodeByTables();
			Loading.Hide();
		}
	}
}
