using Engrama.PWA.Areas.CommonScripts.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos;
using Engrama.Share.Objetos.CommonScripts;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Engrama.PWA.Areas.CommonScripts.Components
{
	public partial class GridCommonScripts : EngramaComponent
	{


		[Inject] private IDialogService DialogService { get; set; }

		[Parameter] public EventCallback<CommonScript> OnEditSelected { get; set; }


		[Parameter] public DataCommonScripts Data { get; set; }
		[Parameter] public bool bEnableEdit { get; set; }


		private IList<CommonScript> tmpLstCommonScripts { get; set; }


		private readonly DialogOptions _maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };

		protected override void OnInitialized()
		{
			tmpLstCommonScripts = new List<CommonScript>();
		}

		protected override async Task OnInitializedAsync()
		{
			Loading.Show();

			await Data.PostGetCommonScripts();
			tmpLstCommonScripts = Data.LstCommonScripts;

			await Data.PostGetCatalogue();

			Loading.Hide();
		}

		private async Task OnCommonScriptSelected(CommonScript commonScript)
		{
			var parameters = new DialogParameters<DialogScript> { { x => x.Script, commonScript } };
			var dialog = await DialogService.ShowAsync<DialogScript>("Script", parameters, _maxWidth);

		}

		private void OnClickFilter(Catalogue catProyect)
		{
			if (catProyect.iId > 0)
			{

				tmpLstCommonScripts = Data.LstCommonScripts.Where(e => e.iIdCatProyectType == catProyect.iId).ToList();
			}
			else
			{
				tmpLstCommonScripts = Data.LstCommonScripts;
			}
		}


	}
}
