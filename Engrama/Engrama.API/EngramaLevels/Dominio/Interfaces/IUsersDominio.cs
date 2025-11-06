using Engrama.Share.Objetos;
using Engrama.Share.PostClass;

using EngramaCoreStandar.Results;

namespace Engrama.API.EngramaLevels.Dominio.Interfaces
{
	public interface IUsersDominio
	{
		Response<AuthenticationModel> BuildToken(User users);
		Task<Response<User>> Login(PostValidateCredentials DAOmodel);
		Task<Response<User>> SaveUsers(PostSaveUsers DAOmodel);
	}
}
