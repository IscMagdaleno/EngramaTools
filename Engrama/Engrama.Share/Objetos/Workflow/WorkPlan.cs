namespace Engrama.Share.Objetos.Workflow
{
	/// <summary>
	/// Representa el plan de trabajo completo generado por la IA.
	/// </summary>
	public class WorkPlan
	{
		public int iIdWorkPlan { get; set; } // PK para la DB
		public int iIdUser { get; set; }

		public string vchInitialDescription { get; set; }
		public string vchImprovedDescription { get; set; } // Descripción mejorada por la IA
		public DateTime dtCreationDate { get; set; }
		public bool bStatus { get; set; }

		public List<ModulePlan> lstModulePlan { get; set; } // Lista de módulos de la aplicación

		public WorkPlan()
		{
			vchInitialDescription = string.Empty;
			vchImprovedDescription = string.Empty;
			lstModulePlan = new List<ModulePlan>();
			dtCreationDate = DateTime.MinValue;
		}
	}

	/// <summary>
	/// Representa un módulo funcional dentro de la aplicación (Ej: Módulo Usuarios).
	/// </summary>
	public class ModulePlan
	{
		public int iIdModulePlan { get; set; } // PK para la DB
		public int iIdWorkPlan { get; set; } // FK a WorkPlan

		public string vchModuleName { get; set; } // Ej: Usuarios
		public string vchModulePurpose { get; set; }
		public List<Phase> lstPhase { get; set; } // Fases o funcionalidades dentro del módulo

		public ModulePlan()
		{
			vchModuleName = string.Empty;
			vchModulePurpose = string.Empty;
			lstPhase = new List<Phase>();
		}
	}

	/// <summary>
	/// Representa una fase o funcionalidad específica (Ej: CRUD de Usuarios).
	/// </summary>
	public class Phase
	{
		public int iIdPhase { get; set; } // PK para la DB
		public int iIdModulePlan { get; set; } // FK a ModulePlan

		public string vchPhaseName { get; set; }
		public string vchPhaseObjective { get; set; }
		public List<Step> lstStep { get; set; } // Pasos de desarrollo

		public Phase()
		{
			vchPhaseName = string.Empty;
			vchPhaseObjective = string.Empty;
			lstStep = new List<Step>();
		}
	}

	/// <summary>
	/// Representa un paso atómico de desarrollo.
	/// </summary>
	public class Step
	{
		public int iIdStep { get; set; } // PK para la DB
		public int iIdPhase { get; set; } // FK a Phase

		public string vchStepDescription { get; set; } // Ej: Crear spSaveUsuarios en la DB.
		public string vchLayer { get; set; } // Capa Engrama (Infraestructura, Dominio, Servicio, Cliente)
		public bool bIsCompleted { get; set; } = false;

		public Step()
		{
			vchStepDescription = string.Empty;
			vchLayer = string.Empty;
		}
	}
}
