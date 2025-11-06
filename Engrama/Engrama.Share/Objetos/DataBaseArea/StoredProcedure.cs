using Engrama.Share.Objetos.AnalyzeCodeArea;

namespace Engrama.Share.Objetos.DataBaseArea
{
	public class StoredProcedure
	{

		public int idStoredProcedure { get; set; }
		public string vchName { get; set; }
		public string vchObjectType { get; set; }
		public DateTime dtCreationDate { get; set; }

		public DetailsStoreProcedure Details { get; set; }

		public IList<CodeAnalyze> LstAnalizedIA { get; set; }

		public StoredProcedure()
		{
			Details = new DetailsStoreProcedure();
			vchName = string.Empty;
			vchObjectType = string.Empty;
			LstAnalizedIA = new List<CodeAnalyze>();
		}
	}
}
