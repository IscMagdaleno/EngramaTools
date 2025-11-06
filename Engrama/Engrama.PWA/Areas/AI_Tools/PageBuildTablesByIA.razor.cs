using Engrama.PWA.Areas.AI_Tools.Utiles;
using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Shared.Common;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.AI_Tools
{
	public partial class PageBuildTablesByIA : EngramaPage
	{
		[Inject] private UserSession UserSession { get; set; }


		public MainAI_Tools Data { get; set; }

		protected override void OnInitialized()
		{
			Data = new MainAI_Tools(httpService, mapperHelper, validaServicioService, UserSession);
		}

		private async Task OnClickCrearCodigo(string prompt)
		{
			Loading.Show();
			Data.Description = prompt;
			await Data.PostCreateNewTableIA();
			Loading.Hide();
		}

	}
}
