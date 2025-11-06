using MudBlazor;

namespace Engrama.PWA.Helpers
{
	public class SnackMessage : GenericResponse
	{
		public string Position { get; }
		public Severity Severity { get; }

		public SnackMessage(bool bResult,
							string vchMessage,
							Severity severity,
							string position = Defaults.Classes.Position.TopCenter
							)
		: base(bResult, vchMessage)
		{

			Position = position;
			Severity = severity;
		}
	}
}
