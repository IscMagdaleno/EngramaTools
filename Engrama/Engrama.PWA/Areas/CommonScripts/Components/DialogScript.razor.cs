using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.CommonScripts;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Engrama.PWA.Areas.CommonScripts.Components
{
	public partial class DialogScript : EngramaComponent
	{
		[CascadingParameter] private IMudDialogInstance MudDialog { get; set; }

		[Parameter]
		public CommonScript Script { get; set; }

		private void Submit() => MudDialog.Close(DialogResult.Ok(true));

	}
}
