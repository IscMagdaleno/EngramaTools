using System.ComponentModel.DataAnnotations;

namespace Engrama.Share.Objetos.DataBaseArea
{
	public class ConnectionString
	{
		public int iIdConnectionString { get; set; }
		public int iIdUser { get; set; }

		[Required(ErrorMessage = "The ConnectionString field is required.")]
		public string vchConnectionString { get; set; }

		[Required(ErrorMessage = "The Note field is required.")]
		public string vchNota { get; set; }
		public bool bActivo { get; set; }
	}
}
