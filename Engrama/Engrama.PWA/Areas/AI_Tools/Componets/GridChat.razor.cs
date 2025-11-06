using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.AI_Tools;

using EngramaCoreStandar.Extensions;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.AI_Tools.Componets
{
	public partial class GridChat : EngramaComponent
	{

		[Parameter] public IList<ChatMessage> LstChatMessage { get; set; }
		[Parameter] public string Lenguaje { get; set; } = "sql";
		[Parameter] public EventCallback<string> OnSendMessage { get; set; }
		public string UserMessage { get; set; }

		private async Task OnClickSendMessage()
		{
			if (UserMessage.NotEmpty())
			{
				await OnSendMessage.InvokeAsync(UserMessage);
				UserMessage = string.Empty;

			}
		}
	}
}
