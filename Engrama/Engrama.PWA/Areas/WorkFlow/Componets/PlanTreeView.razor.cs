// Engrama.PWA/Areas/WorkFlow/Componets/PlanTreeView.razor.cs
using Engrama.PWA.Shared.Common;
using Engrama.Share.Objetos.Workflow;

using Microsoft.AspNetCore.Components;

namespace Engrama.PWA.Areas.WorkFlow.Componets
{
	public partial class PlanTreeView : EngramaComponent
	{
		[Parameter] public List<ModulePlan> Modules { get; set; }

		[Parameter] public EventCallback OnPlanChanged { get; set; }

		private MudBlazor.Color GetLayerColor(string layer)
		{
			return layer?.ToLower() switch
			{
				"infraestructura" => MudBlazor.Color.Info,
				"dominio" => MudBlazor.Color.Primary,
				"servicio" => MudBlazor.Color.Warning,
				"cliente" => MudBlazor.Color.Success,
				_ => MudBlazor.Color.Secondary
			};
		}

		private async Task OnStepCompletedChange(Step step, object isChecked)
		{
			if (isChecked is bool completed)
			{
				step.bIsCompleted = completed;

				if (OnPlanChanged.HasDelegate)
				{
					await OnPlanChanged.InvokeAsync();
				}
			}
		}
	}
}