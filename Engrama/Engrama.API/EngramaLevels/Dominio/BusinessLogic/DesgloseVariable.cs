using Engrama.Share.Objetos.DataBaseArea;

using EngramaCoreStandar;
using EngramaCoreStandar.Extensions;

using System.Text.RegularExpressions;

namespace Engrama.API.EngramaLevels.Dominio.BusinessLogic
{

	public static class DesgloseVariable
	{
		// Method to generate a list of SQL variables from the given input code
		public static List<Variable> GeneraListaVariables_SQLCode(string CodigoEntrada)
		{
			var Resultado = new List<Variable>();

			// Extract the section containing variables
			var seccionSQLCode = GetSeccionVariables(CodigoEntrada, false);

			// Trim the section and split by commas to get individual variables
			seccionSQLCode = seccionSQLCode.Trim();
			var separacionVaraiables = seccionSQLCode.Split(',');

			foreach (var item in separacionVaraiables)
			{
				if (item.NotEmpty()) // Check if the variable string is not empty
				{
					var tempSQlVariable = item.Trim();

					// Normalize spaces and remove tabs
					tempSQlVariable = NormalizeSpaces(tempSQlVariable);

					int tmpLenght = 0;

					// Check if the type is VARCHAR to get max length
					if (tempSQlVariable.Contains("VARCHAR"))
					{
						tmpLenght = GetMaxLenght(tempSQlVariable);
					}

					var listAtributos = tempSQlVariable.Split(" ");
					if (listAtributos.Length > 1) // Ensure there are at least two parts (name and type)
					{
						var nombre = Regex.Replace(listAtributos[0], @"[^a-zA-Z_]", "");
						var SQLTipo = Regex.Replace(listAtributos[1], @"[^a-zA-Z_]", "");

						// Create the Variable object with inferred types
						var variable = new Variable(nombre, GetTipoVariable(SQLTipo, false), SQLTipo);

						variable.iMaxLenght = tmpLenght;

						Resultado.Add(variable);
					}
				}
			}

			return Resultado;
		}

		// Method to extract the section containing variables, either in C# or SQL format
		private static string GetSeccionVariables(string codigoEntrada, bool bCodigoCSharp)
		{
			if (bCodigoCSharp)
			{
				// Extract section between the first and last curly braces
				var primerLlave = codigoEntrada.IndexOf("{") + 1;
				var subSegundoLlave = codigoEntrada.IndexOf("}");

				if (subSegundoLlave - primerLlave <= 20)
				{
					return codigoEntrada;
				}

				var UltimaLlave = codigoEntrada.LastIndexOf("}");
				codigoEntrada = codigoEntrada.Substring(primerLlave, UltimaLlave - primerLlave);
			}
			else
			{
				// Extract section between the first and last parentheses
				if (codigoEntrada.Contains('('))
				{
					var primerParentesis = codigoEntrada.IndexOf("(") + 1;
					var subSegundoParentesis = codigoEntrada.IndexOf(")");

					if (subSegundoParentesis - primerParentesis <= 5)
					{
						return codigoEntrada;
					}

					var UltimaParentesis = codigoEntrada.LastIndexOf(")");
					codigoEntrada = codigoEntrada.Substring(primerParentesis, UltimaParentesis - primerParentesis);
				}
			}

			return codigoEntrada;
		}

		// Method to get the variable type based on SQL or C# type name
		private static string GetTipoVariable(string nombreTipo, bool CSharpType)
		{
			var resultado = string.Empty;
			if (CSharpType)
			{
				// Map C# type to SQL type
				if (GetDataType.RelationTypesCSharpToSQL.ContainsKey(nombreTipo))
				{
					resultado = GetDataType.RelationTypesCSharpToSQL[nombreTipo];
				}
			}
			else
			{
				// Remove length specification in name if exists
				if (nombreTipo.Contains("("))
				{
					var primerParentesis = nombreTipo.IndexOf("(");
					nombreTipo = nombreTipo.Substring(0, primerParentesis);
				}

				// Map SQL type to C# type
				if (GetDataType.RelationTypesSQLToCSharp.ContainsKey(nombreTipo.ToUpper()))
				{
					resultado = GetDataType.RelationTypesSQLToCSharp[nombreTipo.ToUpper()];
				}
			}

			return string.IsNullOrEmpty(resultado) ? Defaults.TipoVariables : resultado;
		}

		// Method to get the maximum length of a VARCHAR type variable
		private static int GetMaxLenght(string nombreTipo)
		{
			int resultado = 0;

			if (nombreTipo.Contains("("))
			{
				var primerParentesis = nombreTipo.IndexOf("(");
				var lastParentesis = nombreTipo.IndexOf(")");

				var tmpValue = nombreTipo.Substring(primerParentesis + 1, lastParentesis - (primerParentesis + 1)).Trim();
				int.TryParse(tmpValue, out resultado);
			}

			return resultado == null ? 0 : resultado;
		}

		// Method to normalize spaces and tabs in a string
		private static string NormalizeSpaces(string input)
		{
			while (input.Contains("  "))
			{
				input = input.Replace("  ", " ");
			}

			while (input.Contains("\t"))
			{
				input = input.Replace("\t", " ");
			}

			return input;
		}
	}
}
