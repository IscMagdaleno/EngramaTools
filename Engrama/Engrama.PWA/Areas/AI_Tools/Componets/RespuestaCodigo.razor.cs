using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.AI_Tools;
using Engrama.Share.Objetos.AnalyzeCodeArea;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.AI_Tools.Componets
{
	public partial class RespuestaCodigo : EngramaComponent
	{

		[Parameter] public ChatMessage Message { get; set; }
		[Parameter] public string Lenguaje { get; set; } = "sql";

		public CodeAfterAnalyzed codeAfterAnalyzed { get; set; }
		protected override void OnInitialized()
		{

			codeAfterAnalyzed = new CodeAfterAnalyzed();
			codeAfterAnalyzed.SetData(Message.Role, Message.Content);

		}

	}
}
