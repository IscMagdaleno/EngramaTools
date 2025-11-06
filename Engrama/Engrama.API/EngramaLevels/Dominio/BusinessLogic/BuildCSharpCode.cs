using Engrama.Share.Objetos.DataBaseArea;

using System.Text;

namespace Engrama.API.EngramaLevels.Dominio.BusinessLogic
{
	public class BuildCSharpCode
	{
		private readonly Table Table;

		public BuildCSharpCode(Table table)
		{
			Table = table;

		}

		public StringBuilder GetClase()
		{
			var Result = new StringBuilder();

			var listaVariables = GeteLstVariables(Table.Columns);

			Result.Append($"\tpublic class {Table.vchName}\r\n");
			Result.Append("\t{\r\n");

			foreach (var item in listaVariables)
			{
				Result.AppendLine($"\t\t\tpublic {item.vchCSharpType} {item.vchName} " + "{get; set;}");
			}
			Result.Append("\t}\r\n");


			return Result;
		}


		/// <summary>
		/// Genera la lista de variables conlos tipo de datos en CShary y SQL a partir de los campos de una table
		/// </summary>
		/// <param name="Columns"></param>
		/// <returns></returns>
		private List<Variable> GeteLstVariables(List<Column> Columns)
		{
			var response = new List<Variable>();

			foreach (var item in Columns)
			{
				if (GetDataType.RelationTypesSQLToCSharp.ContainsKey(item.vchDataType))
				{
					var tipoCharp = GetDataType.RelationTypesSQLToCSharp[item.vchDataType];
					var variable = new Variable(item.vchName, tipoCharp, item.vchDataType);
					response.Add(variable);
				}
				else
				{
					var variable = new Variable(item.vchName, "????", item.vchDataType);
					response.Add(variable);


				}
			}

			return response;
		}
	}
}

