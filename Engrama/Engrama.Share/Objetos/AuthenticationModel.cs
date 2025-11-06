namespace Engrama.Share.Objetos
{
	public class AuthenticationModel
	{
		public int iIdUsers { get; set; }
		public string vchToken { get; set; }
		public DateTime dtExpiration { get; set; }
	}
}
