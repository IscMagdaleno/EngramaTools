using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.AnalyzeCodeArea;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Engrama.PWA.Areas.CodeAnalyzer.Components
{
	public partial class DialogCodeAnalyze : EngramaComponent
	{
		[CascadingParameter] private IMudDialogInstance MudDialog { get; set; }

		[Parameter]
		public CodeAnalyze Code { get; set; }

		private void Submit() => MudDialog.Close(DialogResult.Ok(true));
	}
}
