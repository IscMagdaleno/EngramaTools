namespace Engrama.PWA.Helpers
{
	public class GenericResponse
	{
		public bool bResult { get; set; }
		public string vchMessage { get; set; }

		public GenericResponse()
		{

		}
		public GenericResponse(bool bResult, string vchMessage)
		{
			this.bResult = bResult;
			this.vchMessage = vchMessage;
		}
	}
}
