namespace Engrama.API.EngramaLevels.Dominio.BusinessLogic
{
	public class GetDataType
	{
		public static Dictionary<string, string> RelationTypesCSharpToSQL { get => ConfiguracionesCSharpToSQL(); }


		public static Dictionary<string, string> RelationTypesSQLToCSharp { get => ConfiguracionesSQLToCSharp(); }

		private static Dictionary<string, string> ConfiguracionesCSharpToSQL()
		{
			var n = new Dictionary<string, string>();


			n.Add("string", "VARCHAR(150)");
			n.Add("String", "VARCHAR(150)");
			n.Add("int", "INT");
			n.Add("int", "SMALLINT");
			n.Add("double", "BIGINT");
			n.Add("long", "BIGINT");
			n.Add("decimal", "FLOAT");
			n.Add("DateTime", "DATETIME");
			n.Add("bool", "BIT");
			n.Add("byte", "TINYINT");

			return n;
		}


		private static Dictionary<string, string> ConfiguracionesSQLToCSharp()
		{
			var n = new Dictionary<string, string>();


			n.Add("VARCHAR", "string");
			n.Add("NVARCHAR", "string");
			n.Add("NCHAR", "string");
			n.Add("INT", "int");
			n.Add("SMALLINT", "int");
			n.Add("BIGINT", "long");
			n.Add("MONEY", "decimal");
			n.Add("FLOAT", "decimal");
			n.Add("TINYINT", "byte");
			n.Add("DATETIME", "DateTime?");
			n.Add("DATE", "DateTime");
			n.Add("BIT", "bool");
			n.Add("VARBINARY", "byte[]");

			return n;
		}

	}
}
