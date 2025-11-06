using Engrama.PWA.Areas.DataBase.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using EngramaCoreStandar.Extensions;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Text;

namespace Engrama.PWA.Areas.DataBase.Components
{
	public partial class GridStoredProcedure : EngramaComponent
	{

		[Inject] protected IJSRuntime JS { get; set; }

		[Parameter] public MainDataBase Data { get; set; }
		public bool bShowDetails { get; set; }


		protected override void OnInitialized()
		{
			bShowDetails = false;
		}

		protected override async Task OnInitializedAsync()
		{

			Loading.Show();

			var result = await Data.PostGetAllStoredProcedures();
			ShowSnake(result);
			Loading.Hide();
		}

		private async Task OnStoredProcedureSelected(StoredProcedure storedProcedure)
		{
			Loading.Show();
			bShowDetails = false;

			var result = await Data.SetStoredProceduresSelected(storedProcedure);
			ShowSnake(result);
			bShowDetails = true;
			Loading.Hide();
		}




		private async Task OnClickDescargar()
		{
			Loading.Show();

			var procedures = new StringBuilder();

			await Data.PostGetAllStoredProcedureDetails();

			if (Data.LstStoredProcedures.NotEmpty())
			{

				foreach (var item in Data.LstStoredProcedures)
				{

					var code = item.Details.Code;
					code = code.Replace("CREATE PROCEDURE", $"IF OBJECT_ID( '{item.vchName}' ) IS NULL\r\n\r\n\tEXEC ('CREATE PROCEDURE {item.vchName} AS SET NOCOUNT ON;') \r\n\r\nGO\r\nALTER PROCEDURE ");
					procedures.Append(code);
					procedures.AppendLine("");
				}


				var bytes = System.Text.Encoding.UTF8.GetBytes(procedures.ToString());
				var base64 = Convert.ToBase64String(bytes);
				var nombreArchivo = "StoredProcedures.sql";

				await JS.InvokeVoidAsync("descargarArchivo", base64, nombreArchivo, "text/sql");
			}

			Loading.Hide();

		}

		private async Task OnClickDescargarIndividual()
		{
			Loading.Show();



			if (Data.StoredProcedureSelected.Details.Code.NotEmpty())
			{

				var code = Data.StoredProcedureSelected.Details.Code;
				code = code.Replace("CREATE PROCEDURE", $"IF OBJECT_ID( '{Data.StoredProcedureSelected.vchName}' ) IS NULL\r\n\r\n\tEXEC ('CREATE PROCEDURE {Data.StoredProcedureSelected.vchName} AS SET NOCOUNT ON;') \r\n\r\nGO\r\nALTER PROCEDURE ");

				var bytes = System.Text.Encoding.UTF8.GetBytes(code);
				var base64 = Convert.ToBase64String(bytes);
				var nombreArchivo = $"{Data.StoredProcedureSelected.vchName}.sql";

				await JS.InvokeVoidAsync("descargarArchivo", base64, nombreArchivo, "text/sql");
			}

			Loading.Hide();

		}



	}
}
