using Engrama.PWA.Areas.CodeAnalyzer.Utiles;
using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.AnalyzeCodeArea;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.CodeAnalyzer
{
	public partial class PageCodeAnalyzer : EngramaPage
	{

		[Inject] private UserSession UserSession { get; set; }


		public MainCodeAnalyze Data { get; set; }

		protected override void OnInitialized()
		{

			Data = new MainCodeAnalyze(httpService, validaServicioService, UserSession);
		}

		private void OnCodeAnalyzed(CodeAnalyze codeAnalyze)
		{
			Data.LstCodeAnalyzed.Add(codeAnalyze);
		}

		private void OnCodeAnalyzeAgain(CodeAnalyze codeAnalyze)
		{
			codeAnalyze.vchCodeAnalized = string.Empty;
			Data.CodeAnalyzed = (codeAnalyze);
		}

	}
}
