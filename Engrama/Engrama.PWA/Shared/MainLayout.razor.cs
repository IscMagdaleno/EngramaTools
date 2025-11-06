using Engrama.PWA.Helpers;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Shared
{
	public partial class MainLayout
	{
		[Inject] private NavigationManager NavigationManager { get; set; }

		[Inject] private ILogginService LogginService { get; set; }

		private bool _drawerOpen = false;

		protected override void OnInitialized()
		{
		}


		private void DrawerToggle()
		{
			_drawerOpen = !_drawerOpen;
		}

		private async Task OnclickLogOut()
		{
			NavigationManager.NavigateTo("/PageLogOut");


		}

	}
}
