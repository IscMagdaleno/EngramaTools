using Engrama.PWA.Areas.CommonScripts.Utiles;
using Engrama.PWA.Helpers;

using EngramaCoreStandar.Extensions;
using EngramaCoreStandar.Logger;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Engrama.PWA.Areas.CommonScripts.Components
{
	public partial class FormCommonScript
	{


		[Inject] private ILoggerHelper LoggerHelper { get; set; }
		[Inject] private LoadingState Loading { get; set; }
		[Inject] private ISnackbar Snackbar { get; set; }

		[Parameter] public DataCommonScripts Data { get; set; }

		[Parameter] public EventCallback OnScriptSaved { get; set; }
		protected override void OnInitialized()
		{

			Data.CommonScriptsSelected.CatProyectType = Data.LstCatProyectType.Where(e => e.iId == Data.CommonScriptsSelected.iIdCatProyectType).FirstOrDefault();

		}

		private async void OnSubmit()
		{
			Loading.Show();
			var result = await Data.PostSaveCommonScripts();
			ShowSnake(result);

			if (result.bResult)
			{
				await OnScriptSaved.InvokeAsync();
			}
			Loading.Hide();

		}

		public void ShowSnake(SnackMessage snackMessage)
		{
			if (snackMessage.vchMessage.NotEmpty())
			{

				Snackbar.Clear();
				Snackbar.Configuration.PositionClass = snackMessage.Position;
				Snackbar.Add(snackMessage.vchMessage, snackMessage.Severity);
			}
		}


	}
}
