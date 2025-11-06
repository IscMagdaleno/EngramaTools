using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace Engrama.PWA.Areas.DataBase.Components
{
	public partial class ListTables : EngramaComponent
	{


		[Parameter] public IList<Table> LstTables { get; set; }


		[Parameter] public EventCallback<Table> OnTablaSelected { get; set; }

		private string searchStringFilto = "";




		private async Task TablaSelected(TableRowClickEventArgs<Table> tabla)
		{
			await OnTablaSelected.InvokeAsync(tabla.Item);
		}


		private bool FilterFunc1(Table element) => FilterFunc(element, searchStringFilto);

		private bool FilterFunc(Table element, string searchString)
		{
			if (string.IsNullOrWhiteSpace(searchString))
				return true;
			if (element.vchName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
				return true;


			return false;
		}




	}
}
