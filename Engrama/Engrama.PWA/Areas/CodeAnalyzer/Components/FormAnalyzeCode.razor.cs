using Engrama.PWA.Areas.CodeAnalyzer.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.AnalyzeCodeArea;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Engrama.PWA.Areas.CodeAnalyzer.Components
{
	public partial class FormAnalyzeCode : EngramaComponent
	{
		[Inject] private IDialogService DialogService { get; set; }

		[Parameter] public MainCodeAnalyze Data { get; set; }
		[Parameter] public EventCallback<CodeAnalyze> OnCodeAnalyzed { get; set; }

		private readonly DialogOptions _maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };


		protected override void OnInitialized()
		{

		}


		private async Task OnClickAnalyze()
		{
			Loading.Show();
			var result = await Data.PostAnalyzeCSharp();
			ShowSnake(result);
			if (result.bResult)
			{
				await OnShowCode();
				await OnCodeAnalyzed.InvokeAsync(Data.CodeAnalyzed);
			}


			Loading.Hide();
		}

		private async Task OnShowCode()
		{
			var parameters = new DialogParameters<DialogCodeAnalyze> { { x => x.Code, Data.CodeAnalyzed } };
			var dialog = await DialogService.ShowAsync<DialogCodeAnalyze>("Code Analyzed", parameters, _maxWidth);

		}
		private void OnClickLimpiar()
		{
			Data.CodeAnalyzed = new CodeAnalyze();
		}


	}
}
