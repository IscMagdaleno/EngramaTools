namespace Engrama.Share.Objetos
{
	public class Catalogue
	{
		public int iId { get; set; }
		public string vchDescription { get; set; }
		public Catalogue()
		{
			iId = -1;
			vchDescription = string.Empty;
		}


		public override string ToString()
		{
			return vchDescription;
		}
	}
}
