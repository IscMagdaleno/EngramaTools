
using Engrama.API.EngramaLevels.Infrastructure.Entity;
namespace Engrama.API.EngramaLevels.Infrastructure.Interfaces
{
	public interface IUsersRepository
	{
		Task<spLogin.Result> spLogin(spLogin.Request DAOmodel);
		Task<spSaveUsers.Result> spSaveUsers(spSaveUsers.Request DAOmodel);
	}
}
