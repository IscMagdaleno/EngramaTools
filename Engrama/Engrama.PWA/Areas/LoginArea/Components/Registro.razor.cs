using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Helpers;
using Engrama.PWA.Shared.Common;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Engrama.PWA.Areas.LoginArea.Components
{
	public partial class Registro : EngramaComponent
	{
		[Inject] private NavigationManager NavigationManager { get; set; }

		[Inject] private ILogginService LogginService { get; set; }

		[Parameter] public DataLogin Data { get; set; }

		protected override void OnInitialized()
		{


		}

		private async Task OnSubmit()
		{
			Loading.Show();


			var resp = await Data.PostSaveUsers();
			ShowSnake(resp);

			if (resp.bResult)
			{

				await OnClickLogin();
			}

			Loading.Hide();
		}


		private async Task OnClickLogin()
		{
			Loading.Show();
			Data.Credentials.vchNickName = Data.UsersSelected.vchNickName;
			Data.Credentials.vchPassword = Data.UsersSelected.vchPass;
			var model = await Data.PostValidateCredentials();
			ShowSnake(model);
			if (model.bResult)
			{

				await LogginService.Login(Data.UserAuthenticated);
				NavigationManager.NavigateTo("/");
			}
			else
			{
				Snackbar.Add("Error", Severity.Error);
			}

			Loading.Hide();
		}




	}
}
