namespace Engrama.Share.Objetos.AnalyzeCodeArea
{
	public class CodeAnalyze
	{
		public string vchCode { get; set; }
		public string vchCodeAnalized { get; set; }

		public CodeAfterAnalyzed CodeAnalizedDetails { get; set; }

		public CodeAnalyze()
		{
			vchCode = string.Empty;
			vchCodeAnalized = string.Empty;

			CodeAnalizedDetails = new CodeAfterAnalyzed();
		}

		public void FillDetails()
		{
			CodeAnalizedDetails.SetData(vchCode, vchCodeAnalized);
		}



	}
}
