using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using MudBlazor;

using System.Text;

namespace Engrama.PWA.Areas.DataBase.Components
{
	public partial class TableStoredProcedures : EngramaComponent
	{
		[Parameter] public IList<StoredProcedure> LstStoredProcedures { get; set; }

		[Parameter] public EventCallback<StoredProcedure> OnStoredProcedureSelected { get; set; }

		private string searchStringFilto = "";

		private async void OnStoredProcedure(TableRowClickEventArgs<StoredProcedure> tableRow)
		{
			await OnStoredProcedureSelected.InvokeAsync(tableRow.Item);
		}



		private bool FilterFunc1(StoredProcedure element) => FilterFunc(element, searchStringFilto);

		private bool FilterFunc(StoredProcedure element, string searchString)
		{
			if (string.IsNullOrWhiteSpace(searchString))
				return true;
			if (element.vchName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
				return true;


			return false;
		}


	}
}
