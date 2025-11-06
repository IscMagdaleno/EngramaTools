using Engrama.Share.Objetos;

namespace Engrama.PWA.Helpers
{
	public interface ILogginService
	{
		Task Login(AuthenticationModel token);

		Task Logout();
	}
}
