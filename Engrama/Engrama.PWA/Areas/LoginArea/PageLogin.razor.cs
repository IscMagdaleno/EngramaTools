using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Helpers;
using Engrama.PWA.Shared.Common;

using EngramaCoreStandar.Extensions;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Engrama.PWA.Areas.LoginArea
{
	public partial class PageLogin : EngramaPage
	{
		[Inject] private ILogginService LogginService { get; set; }
		[Inject] private NavigationManager NavigationManager { get; set; }

		[Inject] private IJSRuntime _jSRuntime { get; set; }

		[Inject] private LoadingState Loading { get; set; }

		public DataLogin Data { get; set; }

		public bool bShowResgistro { get; set; }

		protected override void OnInitialized()
		{
			Data = new DataLogin(httpService, mapperHelper, validaServicioService);
			bShowResgistro = false;
		}


		private void OnClickRegistro()
		{
			bShowResgistro = bShowResgistro.False();
		}

	}

}
