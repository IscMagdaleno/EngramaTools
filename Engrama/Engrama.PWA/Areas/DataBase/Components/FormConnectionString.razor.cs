using Engrama.PWA.Areas.DataBase.Utiles;
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.DataBaseArea;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.DataBase.Components
{
	public partial class FormConnectionString : EngramaComponent
	{


		[Parameter] public MainDataBase Data { get; set; }

		[Parameter] public EventCallback<ConnectionString> OnConnectionSaved { get; set; }
		protected override void OnInitialized()
		{

		}


		private async void OnValidSubmit()
		{
			Loading.Show();



			var result = await Data.PostSaveConnectionString();

			ShowSnake(result);
			if (result.bResult)
			{
				await OnConnectionSaved.InvokeAsync(Data.ConnectionStringSelected);
			}

			Loading.Hide();
		}


	}
}
