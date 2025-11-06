using Engrama.PWA.Areas.CommonScripts.Utiles;
using Engrama.PWA.Shared.Common;

namespace Engrama.PWA.Areas.CommonScripts
{
	public partial class PageCommonScripts : EngramaPage
	{

		public DataCommonScripts Data { get; set; }
		protected override void OnInitialized()
		{
			Data = new DataCommonScripts(httpService, mapperHelper, validaServicioService);

		}
	}
}
