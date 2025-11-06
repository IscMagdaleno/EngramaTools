namespace Engrama.Share.Objetos.DataBaseArea
{
	public class Table
	{
		public int iIdTable { get; set; }
		public string vchName { get; set; }
		public string vchPrimaryKey { get; set; }
		public int iNoRows { get; set; }

		public List<Column> Columns { get; set; }
		public List<BuildedCode> BuildedCodes { get; set; }


		public Table()
		{
			Columns = new List<Column>();
			BuildedCodes = new List<BuildedCode>();
			vchName = string.Empty;
			vchPrimaryKey = string.Empty;

		}
	}
}
