using Engrama.API.EngramaLevels.Infrastructure.Entity.DataBase;
using Engrama.Share.Objetos.DataBaseArea;

using EngramaCoreStandar.Extensions;
using EngramaCoreStandar.Mapper;

namespace Engrama.API.EngramaLevels.Dominio.BusinessLogic
{
	public class FactoryTable
	{
		private readonly MapperHelper _mapper;

		public FactoryTable(MapperHelper mapper)
		{
			_mapper = mapper;
		}


		public List<Table> TablasFromList(IEnumerable<sqGetTables.Result> Lsttablas_AB)
		{
			var lista = new List<Table>();
			try
			{

				if (Lsttablas_AB.NotEmpty())
				{
					var gruposTablas = Lsttablas_AB.GroupBy(e => e.iIdTable);

					foreach (var listaTabla in gruposTablas)
					{
						var datosPrincipales = listaTabla.FirstOrDefault();

						var table = _mapper.Get<sqGetTables.Result, Table>(datosPrincipales);


						foreach (var tablarow in listaTabla)
						{
							table.Columns.Add(new Column
							{
								vchName = tablarow.vchColumnName,
								vchDataType = tablarow.vchDataType.ToUpper(),
								iMaxBites = tablarow.iMaxBites
							});
						}

						table.BuildedCodes = GenerateCodesFromTable(table);
						lista.Add(table);
					}
				}

			}
			finally
			{

			}


			return lista;
		}

		private List<BuildedCode> GenerateCodesFromTable(Table table)
		{
			var BuildedCodes = new List<BuildedCode>();

			var Frabrica = new BuildSQLCode(table);
			var FrabricaCSharp = new BuildCSharpCode(table);

			var SqlCode = new BuildedCode("SQL Code");
			SqlCode.AddNewScript(new ScriptGenerated("TEMPORAL TABLE", Frabrica.GetTemporalTable().ToString()));
			SqlCode.AddNewScript(new ScriptGenerated("DATATYPE TABLE", Frabrica.GetDateTypeTable().ToString()));
			SqlCode.AddNewScript(new ScriptGenerated("INPUT SP PARAMS", Frabrica.GetInputParameters().ToString()));
			SqlCode.AddNewScript(new ScriptGenerated("ALTER TABLE", Frabrica.GetAlterTable().ToString()));
			BuildedCodes.Add(SqlCode);


			var storedProcedure = new BuildedCode("Stores Procedures");
			storedProcedure.AddNewScript(new ScriptGenerated("GET", Frabrica.GetSpGetData().ToString()));
			storedProcedure.AddNewScript(new ScriptGenerated("SAVE", Frabrica.GetSPSaveData().ToString()));
			BuildedCodes.Add(storedProcedure);


			var scripts = new BuildedCode("Scripts");
			scripts.AddNewScript(new ScriptGenerated("INSERT", Frabrica.GetInsertCode().ToString()));
			scripts.AddNewScript(new ScriptGenerated("UPDATE", Frabrica.GetUpdateCode().ToString()));
			scripts.AddNewScript(new ScriptGenerated("SELECT", Frabrica.GetSelectCode().ToString()));
			BuildedCodes.Add(scripts);

			var cSharp = new BuildedCode("C# Codes");
			cSharp.AddNewScript(new ScriptGenerated("CLASS", FrabricaCSharp.GetClase().ToString()));
			BuildedCodes.Add(cSharp);



			return BuildedCodes;
		}

	}
}
