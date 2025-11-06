namespace Engrama.Share.Objetos.CommonScripts
{
	public class CommonScript
	{
		public int iIdCommonScripts { get; set; }
		public int iIdCatProyectType { get; set; }
		public string vchName { get; set; }
		public string vchDescription { get; set; }
		public string vchCode { get; set; }

		public Catalogue CatProyectType { get; set; }

		public CommonScript()
		{
			vchName = string.Empty;
			vchDescription = string.Empty;
			vchCode = string.Empty;

			CatProyectType = new Catalogue();
		}
	}
}
