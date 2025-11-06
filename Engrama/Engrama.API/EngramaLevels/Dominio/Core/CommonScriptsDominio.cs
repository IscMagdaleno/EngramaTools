using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.API.EngramaLevels.Infrastructure.Interfaces;
using Engrama.Share.Entity;
using Engrama.Share.Entity.CommonScripts;
using Engrama.Share.Objetos;
using Engrama.Share.Objetos.CommonScripts;
using Engrama.Share.PostClass;
using Engrama.Share.PostClass.CommonScritps;

using EngramaCoreStandar.Mapper;
using EngramaCoreStandar.Results;

namespace Engrama.API.EngramaLevels.Dominio.Core
{
	public class CommonScriptsDominio : ICommonScriptsDominio
	{

		private readonly MapperHelper mapper;
		private readonly IResponseHelper responseHelper;
		private readonly ICommonScriptsRepository commonScriptsRepository;

		/// <summary>
		/// Initialize the fields receiving the interfaces on the builder
		/// </summary>
		public CommonScriptsDominio(
			MapperHelper mapper,
			IResponseHelper responseHelper,
			ICommonScriptsRepository commonScriptsRepository)
		{
			this.mapper = mapper;
			this.responseHelper = responseHelper;
			this.commonScriptsRepository = commonScriptsRepository;
		}


		public async Task<Response<CommonScript>> SaveCommonScripts(PostSaveCommonScripts DAOmodel)
		{
			try
			{
				var model = mapper.Get<PostSaveCommonScripts, spSaveCommonScripts.Request>(DAOmodel);

				var result = await commonScriptsRepository.spSaveCommonScripts(model);
				var validation = responseHelper.Validacion<spSaveCommonScripts.Result, CommonScript>(result);
				if (validation.IsSuccess)
				{
					DAOmodel.iIdCommonScripts = validation.Data.iIdCommonScripts;
					validation.Data = mapper.Get<PostSaveCommonScripts, CommonScript>(DAOmodel); ;
				}
				return validation;

			}
			catch (Exception ex)
			{
				return Response<CommonScript>.BadResult(ex.Message, new());
			}
		}


		public async Task<Response<IEnumerable<CommonScript>>> GetCommonScripts(PostGetCommonScripts DAOmodel)
		{
			try
			{
				var model = mapper.Get<PostGetCommonScripts, spGetCommonScripts.Request>(DAOmodel);

				var result = await commonScriptsRepository.spGetCommonScripts(model);
				var validation = responseHelper.Validacion<spGetCommonScripts.Result, CommonScript>(result);
				if (validation.IsSuccess)
				{
					validation.Data = validation.Data;
				}
				return validation;

			}
			catch (Exception ex)
			{
				return Response<IEnumerable<CommonScript>>.BadResult(ex.Message, new List<CommonScript>());
			}
		}

		public async Task<Response<IEnumerable<Catalogue>>> GetCatalogue(PostGetCatalogue DAOmodel)
		{
			try
			{
				var model = mapper.Get<PostGetCatalogue, spGetCatalogue.Request>(DAOmodel);

				var result = await commonScriptsRepository.spGetCatalogue(model);
				var validation = responseHelper.Validacion<spGetCatalogue.Result, Catalogue>(result);
				if (validation.IsSuccess)
				{
					validation.Data = validation.Data;
				}
				return validation;

			}
			catch (Exception ex)
			{
				return Response<IEnumerable<Catalogue>>.BadResult(ex.Message, new List<Catalogue>());
			}
		}

	}
}
