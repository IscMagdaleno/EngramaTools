using Engrama.Share.Objetos.AnalyzeCodeArea;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Engrama.PWA.Areas.CodeAnalyzer.Components
{
	public partial class CardAnalyzeCode
	{

		[Inject] private IDialogService DialogService { get; set; }




		[Parameter] public CodeAnalyze Code { get; set; }
		[Parameter] public EventCallback<CodeAnalyze> OnCodeAnalyzeAgain { get; set; }

		private readonly DialogOptions _maxWidth = new() { MaxWidth = MaxWidth.Large, FullWidth = true };


		private async Task OnShowCode()
		{
			var parameters = new DialogParameters<DialogCodeAnalyze> { { x => x.Code, Code } };
			var dialog = await DialogService.ShowAsync<DialogCodeAnalyze>("Code Analyzed", parameters, _maxWidth);

		}

		private async Task OnClickAnalyzeAgain()
		{
			await OnCodeAnalyzeAgain.InvokeAsync(Code);
		}



	}
}
