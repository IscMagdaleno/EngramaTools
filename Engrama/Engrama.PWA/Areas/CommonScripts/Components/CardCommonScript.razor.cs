using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.CommonScripts;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.CommonScripts.Components
{
	public partial class CardCommonScript : EngramaComponent
	{


		[Parameter] public CommonScript Script { get; set; }
		[Parameter] public bool bEnableEdit { get; set; }

		[Parameter] public EventCallback<CommonScript> OnCommonScriptSelected { get; set; }
		[Parameter] public EventCallback<CommonScript> OnEditSelected { get; set; }

		private async Task OnClickCommonScript()
		{
			await OnCommonScriptSelected.InvokeAsync(Script);
		}

		private async Task OnClicEditSelected()
		{
			await OnEditSelected.InvokeAsync(Script);
		}
	}
}
