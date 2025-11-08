namespace Engrama.Share.PostClass.WorkFlow
{
	public class PostGenerateWorkPlan
	{
		public int iIdWorkPlan { get; set; }
		public string Description { get; set; } // Descripción inicial del usuario
		public int iIdUser { get; set; }

		public PostGenerateWorkPlan()
		{
			Description = string.Empty;
		}
	}
}
