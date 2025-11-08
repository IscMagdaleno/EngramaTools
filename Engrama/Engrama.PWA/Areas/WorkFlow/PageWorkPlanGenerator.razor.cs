using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Areas.WorkFlow.Utiles;
using Engrama.PWA.Shared.Common;

using EngramaCoreStandar.Dapper.Results;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.WorkFlow
{
	public partial class PageWorkPlanGenerator : EngramaPage
	{
		[Inject] private UserSession UserSession { get; set; }

		public MainWorkFlow Data { get; set; }

		protected override void OnInitialized()
		{
			Data = new MainWorkFlow(httpService, validaServicioService, UserSession);
		}

		private async Task OnClickGeneratePlan()
		{
			if (string.IsNullOrWhiteSpace(Data.WorkPlanDescription))
			{
				ShowSnake(new SeverityMessage(false, "Debe ingresar una descripción detallada.", SeverityTag.Warning));
				return;
			}

			Loading.Show();

			// La API maneja la lógica iterativa usando el ID en el modelo.
			var result = await Data.PostGenerateWorkPlan();
			ShowSnake(result);

			// Después de la generación, el campo de entrada debe mostrar la descripción mejorada
			// para que el usuario pueda editar esa versión mejorada en la siguiente iteración.
			if (result.bResult)
			{
				Data.WorkPlanDescription = Data.CurrentWorkPlan.vchImprovedDescription;
			}

			Loading.Hide();
		}

		private async Task OnClickSavePlan()
		{
			Loading.Show();

			// El usuario está confirmando la descripción que está en el campo de texto (Data.WorkPlanDescription)
			// Debemos asegurarnos de que la versión en el objeto del plan sea la misma que la editada por el usuario.
			Data.CurrentWorkPlan.vchImprovedDescription = Data.WorkPlanDescription;

			var result = await Data.PostUpdateWorkPlanDetails();
			ShowSnake(result);

			Loading.Hide();
		}
	}
}
