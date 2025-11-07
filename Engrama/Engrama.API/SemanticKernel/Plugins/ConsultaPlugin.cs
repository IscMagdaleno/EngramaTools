// Engrama.API/SemanticKernel/Plugins/ConsultaPlugin.cs
using Engrama.API.EngramaLevels.Dominio.Interfaces;
using Engrama.Share.PostClass.DataBase;

using Microsoft.SemanticKernel;

using System.ComponentModel;
using System.Text.Json;

namespace Engrama.API.SemanticKernel.Plugins
{
	/// <summary>
	/// Funciones nativas para consultar la estructura de la base de datos (DB) a través de la Capa de Dominio de Engrama.
	/// </summary>
	[Description("Funciones para consultar la estructura de la base de datos de EngramaTools.")]
	public class ConsultaPlugin
	{
		private readonly IDataBaseDominio _dataBaseDominio;

		/// <summary>
		/// Inicia el plugin con la dependencia a la capa de Dominio para consultar la DB.
		/// </summary>
		public ConsultaPlugin(IDataBaseDominio dataBaseDominio)
		{
			_dataBaseDominio = dataBaseDominio;
		}

		[KernelFunction]
		[Description("Obtiene una lista de nombres de todas las tablas en la base de datos de Engrama. Devuelve un string en formato JSON con el nombre de la tabla y el número de filas (iNoRows).")]
		public async Task<string> GetTables(
			//[Description("La cadena de conexión (ConnectionString) de la base de datos a consultar. Este campo es obligatorio.")] string connectionString
			)
		{
			var postModel = new PostGetTables { ConnectionString = "Data Source=Engrama.mssql.somee.com;Initial Catalog=Engrama;User ID=MMartinez_SQLLogin_1;Password=95xodkhgxa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" };
			var result = await _dataBaseDominio.GetTables(postModel);

			if (result.IsSuccess && result.Data != null)
			{
				// Selecciona solo los campos relevantes para el LLM y serializa.
				var tableData = result.Data.ToList();
				return JsonSerializer.Serialize(tableData);
			}

			return $"Error: No se pudieron obtener las tablas. Mensaje: {result.Message}";
		}

		[KernelFunction]
		[Description("Obtiene una lista de nombres de todos los procedimientos almacenados (SPs) en la base de datos de Engrama. Devuelve un string en formato JSON con el nombre del SP (vchName) y la fecha de creación.")]
		public async Task<string> GetStoredProcedures()
		//[Description("La cadena de conexión (ConnectionString) de la base de datos a consultar. Este campo es obligatorio.")] string connectionString)
		{
			var postModel = new PostGetAllStoredProcedures { ConnectionString = "Data Source=Engrama.mssql.somee.com;Initial Catalog=Engrama;User ID=MMartinez_SQLLogin_1;Password=95xodkhgxa;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" };

			var result = await _dataBaseDominio.GetAllStoredProcedures(postModel);

			if (result.IsSuccess && result.Data != null)
			{
				// Selecciona solo los campos relevantes para el LLM y serializa.
				var spData = result.Data.Select(sp => new { sp.vchName, sp.dtCreationDate }).ToList();
				return JsonSerializer.Serialize(spData);
			}

			return $"Error: No se pudieron obtener los procedimientos almacenados. Mensaje: {result.Message}";
		}
	}
}