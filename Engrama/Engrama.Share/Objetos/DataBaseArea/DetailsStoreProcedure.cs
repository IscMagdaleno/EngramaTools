namespace Engrama.Share.Objetos.DataBaseArea
{
	public class DetailsStoreProcedure
	{


		public string Code { get; set; }
		public List<string> LstCodes { get; set; }

		public List<Variable> InputParameters { get; set; }
		public List<Variable> OutputParameters { get; set; }
		public List<BuildedCode> BuildedCodes { get; set; }

		public DetailsStoreProcedure()
		{
			Code = string.Empty;
			LstCodes = new List<string>();
			InputParameters = new List<Variable>();
			OutputParameters = new List<Variable>();
			BuildedCodes = new List<BuildedCode>();

		}

	}
}
