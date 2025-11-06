using Engrama.Share.Objetos.AI_Tools;
using Engrama.Share.PostClass.AI_Tools;

using EngramaCoreStandar.Results;

namespace Engrama.API.EngramaLevels.Dominio.Interfaces
{
	public interface IAI_ToolsDominio
	{
		Task<Response<ResultOpenAI>> CreateCodeByTables(PostCreateCodeByTables postModel);
		Task<Response<ResultOpenAI>> CreateNewTable(PostCreateNewTableIA postModel);
		Task<Response<ResultOpenAI>> ImproveCSharpCodeAI(PostImproveCSharpCode postModel);
		Task<Response<ResultOpenAI>> ImproveSPwithAI(PostImproveSPwithAI postModel);
	}
}
