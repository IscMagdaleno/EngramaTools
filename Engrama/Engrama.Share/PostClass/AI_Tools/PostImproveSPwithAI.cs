using Engrama.Share.Objetos.DataBaseArea;

namespace Engrama.Share.PostClass.AI_Tools
{
	public class PostImproveSPwithAI
	{
		public StoredProcedure StoredProcedure { get; set; }
		public List<Table> LstTablas { get; set; }

		public string Prompt { get; set; }
	}
}
