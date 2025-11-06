using Engrama.PWA.Areas.CommonScripts.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.CommonScripts;

namespace Engrama.PWA.Areas.CommonScripts
{
	public partial class PageAdminCommonScript : EngramaPage
	{

		public DataCommonScripts Data { get; set; }

		public bool bShowForm { get; set; }
		protected override void OnInitialized()
		{
			Data = new DataCommonScripts(httpService, mapperHelper, validaServicioService);
			bShowForm = false;
		}



		private void OnEditSelected(CommonScript commonScripts)
		{
			Data.CommonScriptsSelected = commonScripts;
			bShowForm = true;


		}

		private void OnClickAdd()
		{
			OnEditSelected(new CommonScript());
		}


		private void OnScriptSaved()
		{
			bShowForm = false;
			Data.CommonScriptsSelected = new CommonScript();
		}

		private void OnClickVolver()
		{
			bShowForm = false;
		}
	}
}
