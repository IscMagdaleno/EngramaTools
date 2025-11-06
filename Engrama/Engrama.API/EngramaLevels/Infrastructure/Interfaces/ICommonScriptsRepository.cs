using Engrama.Share.Entity;
using Engrama.Share.Entity.CommonScripts;

namespace Engrama.API.EngramaLevels.Infrastructure.Interfaces
{
	public interface ICommonScriptsRepository
	{
		Task<IEnumerable<spGetCatalogue.Result>> spGetCatalogue(spGetCatalogue.Request DAOmodel);
		Task<IEnumerable<spGetCommonScripts.Result>> spGetCommonScripts(spGetCommonScripts.Request DAOmodel);
		Task<spSaveCommonScripts.Result> spSaveCommonScripts(spSaveCommonScripts.Request DAOmodel);
	}
}
