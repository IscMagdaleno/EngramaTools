using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Helpers;
using Engrama.PWA.Shared.Common;

using EngramaCoreStandar.Logger;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Engrama.PWA.Areas.LoginArea.Components
{
	public partial class Login : EngramaComponent
	{
		[Inject] private ILogginService LogginService { get; set; }
		[Inject] private NavigationManager NavigationManager { get; set; }
		[Inject] private ISnackbar Snackbar { get; set; }

		[Inject] private ILoggerHelper LoggerHelper { get; set; }
		[Inject] private LoadingState Loading { get; set; }

		[Parameter] public DataLogin Data { get; set; }

		protected override void OnInitialized()
		{
		}


		protected override async Task OnInitializedAsync()
		{

		}

		private async void OnClickLogin()
		{
			Loading.Show();
			var model = await Data.PostValidateCredentials();
			ShowSnake(model);
			if (model.bResult)
			{
				await LogginService.Login(Data.UserAuthenticated);
				NavigationManager.NavigateTo("/");
			}


			Loading.Hide();
		}



	}
}
