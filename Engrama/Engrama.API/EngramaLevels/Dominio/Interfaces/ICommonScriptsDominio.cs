using Engrama.Share.Objetos;
using Engrama.Share.Objetos.CommonScripts;
using Engrama.Share.PostClass;
using Engrama.Share.PostClass.CommonScritps;

using EngramaCoreStandar.Results;

namespace Engrama.API.EngramaLevels.Dominio.Interfaces
{
	public interface ICommonScriptsDominio
	{
		Task<Response<IEnumerable<Catalogue>>> GetCatalogue(PostGetCatalogue DAOmodel);
		Task<Response<IEnumerable<CommonScript>>> GetCommonScripts(PostGetCommonScripts DAOmodel);
		Task<Response<CommonScript>> SaveCommonScripts(PostSaveCommonScripts DAOmodel);
	}
}
