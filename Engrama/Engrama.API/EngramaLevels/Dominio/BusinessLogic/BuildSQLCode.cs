using Engrama.Share.Objetos.DataBaseArea;

using System.Text;

namespace Engrama.API.EngramaLevels.Dominio.BusinessLogic
{
	public class BuildSQLCode
	{

		private readonly Table Table;

		public BuildSQLCode(Table table)
		{
			Table = table;
			//Variables = GeneraListVariables(tabla.Columns);
		}


		/// <summary>
		/// Get the Insert code in SQL based in the table selected
		/// </summary>
		/// <returns></returns>
		public StringBuilder GetInsertCode()
		{

			var Result = new StringBuilder();

			var NicName = GetUpperLetters(Table.vchName);
			string FirstColumn = Table.Columns.FirstOrDefault().vchName;


			Result = CreteInsert(Result);
			Result.AppendLine("\t,");

			Result.AppendLine("\t( \n\t ");
			int J = 1;

			var lstColumns = Table.Columns.Where(e => e.vchName != FirstColumn).ToList();

			foreach (var item in lstColumns)
			{
				Result.Append($"\t\t{DefaultValue(item.vchDataType)}, \t");
				if (J % 3 == 0)
				{
					Result.Append("\n");

				}
				J++;
			}
			Result = AvoidLastComa(Result);


			Result.AppendLine("");
			Result.AppendLine("\t)\n");
			Result.AppendLine("END\n");

			return Result;
		}


		/// <summary>
		/// Get back the update code in SQL based on the table selected
		/// </summary>
		/// <returns></returns>
		public StringBuilder GetUpdateCode()
		{
			var Result = new StringBuilder();
			string FirstColumn = Table.Columns.FirstOrDefault().vchName;

			Result = CreateUpdate(Result);
			return Result;
		}


		/// <summary>
		/// Get back the select code in SQL bases in table selected
		/// </summary>
		/// <returns></returns>
		public string GetSelectCode()
		{
			var Result = new StringBuilder();


			var NicName = GetUpperLetters(Table.vchName);
			string FirstColumn = Table.Columns.FirstOrDefault().vchName;
			Result.Append($"SELECT ");

			int i = 1;
			foreach (var item in Table.Columns)
			{
				Result.Append($"{NicName}.{item.vchName}, \t");
				if (i % 3 == 0)
				{
					Result.Append("\n");
					Result.Append("\t");

				}
				i++;
			}
			Result = AvoidLastComa(Result);
			Result.AppendLine($"FROM");
			Result.AppendLine($"\t dbo.{Table.vchName} {NicName}  WITH(NOLOCK) \t ");

			Result.AppendLine("");

			Result.AppendLine($"WHERE");
			Result.AppendLine($"\t {NicName}.{FirstColumn}  = @{FirstColumn};\n");
			Result.AppendLine("");

			return Result.ToString();
		}


		/// <summary>
		/// Get the code to alter the table selected
		/// </summary>
		/// <returns></returns>
		public string GetAlterTable()
		{
			var Result = new StringBuilder();


			var NicName = GetUpperLetters(Table.vchName);

			Result.AppendLine($"IF NOT EXISTS ( SELECT *");
			Result.AppendLine($"\t\t\t\tFROM");
			Result.AppendLine($"\t\t\t\tSYSOBJECTS O,");
			Result.AppendLine($"\t\t\t\tSYSCOLUMNS C");
			Result.AppendLine($"\t\t\tWHERE");
			Result.AppendLine($"\t\t\t\tO.id     = C.id");
			Result.AppendLine($"\t\t\t\tAND O.NAME   = '{Table.vchName}'");
			Result.AppendLine($"\t\t\t\tAND C.NAME   = '_CampoModificar_'");
			Result.AppendLine(")");
			Result.AppendLine("BEGIN");
			Result.AppendLine("");
			Result.AppendLine("\t\t\t-- Modificar tamaño de campo");
			Result.AppendLine($"\t\tALTER TABLE {Table.vchName}");
			Result.AppendLine("\t\tALTER COLUMN _CampoModificar_ VARCHAR(200) NOT NULL;");
			Result.AppendLine("");

			Result.AppendLine("END");
			Result.AppendLine("");
			Result.AppendLine("");


			Result.AppendLine($"IF EXISTS ( SELECT *");
			Result.AppendLine($"\t\t\t\tFROM");
			Result.AppendLine($"\t\t\t\tSYSOBJECTS O,");
			Result.AppendLine($"\t\t\t\tSYSCOLUMNS C");
			Result.AppendLine($"\t\t\tWHERE");
			Result.AppendLine($"\t\t\t\tO.id     = C.id");
			Result.AppendLine($"\t\t\t\tAND O.NAME   = '{Table.vchName}'");
			Result.AppendLine($"\t\t\t\tAND C.NAME   = '_CampoModificar_'");
			Result.AppendLine(")");
			Result.AppendLine("BEGIN");
			Result.AppendLine("");
			Result.AppendLine("\t\t\t--Eliminbar llave foranea en caso de");
			Result.AppendLine($"\t\tALTER TABLE {Table.vchName}");
			Result.AppendLine($"\t\tDROP CONSTRAINT FK_{Table.vchName}__CampoModificar_;");
			Result.AppendLine("");
			Result.AppendLine($"\t\tALTER TABLE {Table.vchName}");
			Result.AppendLine($"\t\tDROP COLUMN iIdUser;");

			Result.AppendLine("END");
			Result.AppendLine("");
			Result.AppendLine("");


			Result.AppendLine($"IF NOT EXISTS ( SELECT *");
			Result.AppendLine($"\t\t\t\tFROM");
			Result.AppendLine($"\t\t\t\tSYSOBJECTS O,");
			Result.AppendLine($"\t\t\t\tSYSCOLUMNS C");
			Result.AppendLine($"\t\t\tWHERE");
			Result.AppendLine($"\t\t\t\tO.id     = C.id");
			Result.AppendLine($"\t\t\t\tAND O.NAME   = '{Table.vchName}'");
			Result.AppendLine($"\t\t\t\tAND C.NAME   = '_CampoModificar_'");
			Result.AppendLine(")");
			Result.AppendLine("BEGIN");
			Result.AppendLine("");
			Result.AppendLine("\t\t\t--Eliminbar llave foranea en caso de");
			Result.AppendLine($"\t\tALTER TABLE {Table.vchName}");
			Result.AppendLine("\t\t ADD _CampoModificar_ INT;");
			Result.AppendLine("");
			Result.AppendLine("\t\t\t --En caso de agregar llave foranea");
			Result.AppendLine($"\t\tADD CONSTRAINT FK_ {Table.vchName}__CampoModificar_ FOREIGN KEY (_CampoModificar_)");
			Result.AppendLine($"\t\tREFERENCES OtherTable(Id);");

			Result.AppendLine("END");
			Result.AppendLine("");
			Result.AppendLine("");

			return Result.ToString();
		}


		/// <summary>
		/// Get the columns of the table selected in a list as a temporal table
		/// </summary>
		/// <returns></returns>
		public string GetTemporalTable()
		{
			var Result = new StringBuilder();


			var NicName = GetUpperLetters(Table.vchName);
			Result.AppendLine($"DECLARE @{Table.vchName} AS TABLE");
			Result.AppendLine("(");
			foreach (var item in Table.Columns)
			{
				Result.AppendLine($"\t\t{item.vchName} {CompeltarTipoVariable(item)} DEFAULT ({DefaultValue(item.vchDataType)}),");
			}
			Result = AvoidLastComa(Result);

			Result.AppendLine(")");


			return Result.ToString();
		}


		public string GetDateTypeTable()
		{
			var Result = new StringBuilder();


			var NicName = GetUpperLetters(Table.vchName);
			Result.AppendLine($"IF NOT EXISTS (SELECT 1 FROM sys.types WHERE is_table_type = 1 AND name = ' DT{Table.vchName}')\r\n");
			Result.AppendLine("BEGIN");
			Result.AppendLine($"CREATE TYPE DT{Table.vchName} AS TABLE");
			Result.AppendLine("(");
			foreach (var item in Table.Columns)
			{
				Result.AppendLine($"\t\t{item.vchName} {CompeltarTipoVariable(item)} DEFAULT ({DefaultValue(item.vchDataType)}),");
			}
			Result = AvoidLastComa(Result);

			Result.AppendLine(")");
			Result.AppendLine("END");


			return Result.ToString();
		}


		/// <summary>
		/// Get back the input parameters that the SP will receive 
		/// </summary>
		/// <returns></returns>
		public string GetInputParameters()
		{
			var Result = new StringBuilder();


			var NicName = GetUpperLetters(Table.vchName);
			Result.AppendLine("(");
			foreach (var item in Table.Columns)
			{
				Result.AppendLine($"\t\t@{item.vchName} {CompeltarTipoVariable(item)},");
			}
			Result = AvoidLastComa(Result);

			Result.AppendLine(")");


			return Result.ToString();
		}

		/// <summary>
		/// Get the StoreProcedure to get all the data from the database
		/// </summary>
		/// <returns></returns>
		public string GetSpGetData()
		{
			var spName = $"spGet{Table.vchName}";
			var FirstColumn = Table.Columns.FirstOrDefault();
			var NicName = GetUpperLetters(Table.vchName);


			var Result = new StringBuilder();
			Result = GetFirstPartSP(Result, spName);


			Result.AppendLine($"");
			Result.AppendLine($"CREATE TABLE #Result (");
			Result.AppendLine($"\tbResult BIT DEFAULT (1),");
			Result.AppendLine($"\tvchMessage VARCHAR(500) DEFAULT(''),");

			foreach (var item in Table.Columns)
			{
				Result.AppendLine($"\t {item.vchName} {CompeltarTipoVariable(item)} DEFAULT({DefaultValue(item.vchDataType)}),");
			}

			Result.AppendLine($");");
			Result.AppendLine($"");
			Result.AppendLine($"SET NOCOUNT ON");
			Result.AppendLine($"");

			Result.AppendLine($"\tBEGIN TRY");
			Result.AppendLine($"");

			Result.AppendLine($"\tINSERT INTO  #Result");
			Result.AppendLine($"\t ( \n");
			var lstColumns = Table.Columns.ToList();

			int i = 1;
			foreach (var item in lstColumns)
			{
				Result.Append($"\t\t{item.vchName}, \t");
				if (i % 3 == 0)
				{
					Result.Append("\n");

				}
				i++;
			}
			Result = AvoidLastComa(Result);
			Result.AppendLine($"\t\t)");
			Result.AppendLine($"\t\tSELECT ");
			i = 1;
			foreach (var item in lstColumns)
			{
				Result.Append($"\t\t {NicName}.{item.vchName}, \t");
				if (i % 3 == 0)
				{
					Result.Append("\n");
					Result.Append("\t\t");

				}
				i++;
			}
			Result = AvoidLastComa(Result);
			Result.AppendLine($"FROM");
			Result.AppendLine($"\t\t dbo.{Table.vchName} {NicName}  WITH(NOLOCK)  ");

			Result.AppendLine($"");
			Result.AppendLine($"");

			Result.AppendLine($"\t\tIF NOT EXISTS (SELECT 1 FROM #Result)");
			Result.AppendLine($"\t\t\tBEGIN");
			Result.AppendLine($"\t\t\t\tINSERT INTO #Result (bResult, vchMessage)");
			Result.AppendLine($"\t\t\t\tSELECT 0, 'No register found.';");
			Result.AppendLine($"\t\t\tEND");
			Result.AppendLine($"\tEND TRY");
			Result.AppendLine($"\tBEGIN CATCH");
			Result.AppendLine($"\t\tINSERT INTO #Result (bResult, vchMessage)");
			Result.AppendLine($"\t\tSELECT 0, CONCAT(ERROR_PROCEDURE(), ' : ', ERROR_MESSAGE(), ' - ', ERROR_LINE());");
			Result.AppendLine($"\t\tPRINT CONCAT(ERROR_PROCEDURE(), ' : ', ERROR_MESSAGE(), ' - ', ERROR_LINE());");
			Result.AppendLine($"\tEND CATCH");
			Result.AppendLine($"\tSELECT * FROM #Result;");
			Result.AppendLine($"\tDROP TABLE #Result;");
			Result.AppendLine($"\tEND");

			Result.AppendLine($"GO");

			return Result.ToString();


		}

		/// <summary>
		/// Get the Store procedure to save the data on the database based on the table selected
		/// </summary>
		/// <returns></returns>
		public string GetSPSaveData()
		{

			var Result = new StringBuilder();

			var spName = $"spSave{Table.vchName}";
			var FirstColumn = Table.Columns.FirstOrDefault();
			var NicName = GetUpperLetters(Table.vchName);

			Result = GetFirstPartSP(Result, spName, true);

			Result.AppendLine($"DECLARE @vchError VARCHAR(200) = '';");
			Result.AppendLine($"");
			Result.AppendLine($"DECLARE @Result AS TABLE (");
			Result.AppendLine($"\tbResult BIT DEFAULT(1),");
			Result.AppendLine($"\tvchMessage VARCHAR(500) DEFAULT(''),");
			Result.AppendLine($"\t{FirstColumn.vchName} {FirstColumn.vchDataType} DEFAULT({DefaultValue(FirstColumn.vchDataType)}) ");

			Result.AppendLine($"\t);");
			Result.AppendLine($"");
			Result.AppendLine($"SET NOCOUNT ON");
			Result.AppendLine($"SET XACT_ABORT ON;");
			Result.AppendLine($"");
			Result.AppendLine($"BEGIN TRY");
			Result.AppendLine($"");

			Result.AppendLine($"BEGIN TRANSACTION");
			Result.AppendLine($"");



			Result = CreteInsert(Result);
			Result.AppendLine($"\t\t SET @{FirstColumn.vchName} = @@IDENTITY");
			Result.AppendLine($"END");
			Result.AppendLine($"ELSE");
			Result.AppendLine($"BEGIN");
			Result = CreateUpdate(Result);
			Result.AppendLine($"END");

			Result.AppendLine($"\t\tCOMMIT TRANSACTION ;");
			Result.AppendLine($"\tEND TRY");
			Result.AppendLine($"\tBEGIN CATCH");
			Result.AppendLine($"\t\tROLLBACK TRANSACTION ;");
			Result.AppendLine($"");
			Result.AppendLine($"\t\tSELECT @vchError = CONCAT( '{spName}: ', ERROR_MESSAGE( ), ' ', ERROR_PROCEDURE( ), ' ', ERROR_LINE( ) );");
			Result.AppendLine($"\t\tPRINT @vchError;");
			Result.AppendLine($"\tEND CATCH");
			Result.AppendLine($"");
			Result.AppendLine($"\tIF LEN( @vchError ) > 0");
			Result.AppendLine($"\tBEGIN");
			Result.AppendLine($"\t\tINSERT INTO @Result");
			Result.AppendLine($"\t\t(");
			Result.AppendLine($"\t\t\tbResult,vchMessage");
			Result.AppendLine($"\t\t)");
			Result.AppendLine($"\t\tVALUES");
			Result.AppendLine($"\t\t(");
			Result.AppendLine($"\t\t\t0,@vchError");
			Result.AppendLine($"\t\t);");
			Result.AppendLine($"\tEND");
			Result.AppendLine($"\tELSE");
			Result.AppendLine($"\tBEGIN");
			Result.AppendLine($"\t\tINSERT INTO @Result");
			Result.AppendLine($"\t\t(");
			Result.AppendLine($"\t\t\tbResult,vchMessage,{FirstColumn.vchName}");
			Result.AppendLine($"\t\t)");
			Result.AppendLine($"\t\tVALUES");
			Result.AppendLine($"\t\t(");
			Result.AppendLine($"\t\t\t1,'',@{FirstColumn.vchName}");
			Result.AppendLine($"\t\t);");
			Result.AppendLine($"\tEND;");
			Result.AppendLine($"");
			Result.AppendLine($"\tSELECT *");
			Result.AppendLine($"\tFROM");
			Result.AppendLine($"\t\t@Result;");
			Result.AppendLine($"\tSET NOCOUNT OFF;");
			Result.AppendLine($"END;");
			Result.AppendLine($"GO ");
			Result.AppendLine($"");
			Result.AppendLine($"");



			return Result.ToString();


		}

		private string GetUpperLetters(string texto)
		{
			string resultado = "";

			foreach (char caracter in texto)
			{
				if (char.IsUpper(caracter))
				{
					resultado += caracter;
				}
			}

			return resultado;
		}

		private string DefaultValue(string tipoCampo)
		{
			var resultado = "";
			switch (tipoCampo)
			{
				case "VARCHAR":
				case "NVARCHAR":
					resultado = $" '' ";
					break;

				case "INT":
				case "BIGINT":
					resultado = $" -1 ";
					break;

				case "MONEY":
				case "BIT":
					resultado = $" 0 ";
					break;

				case "DATETIME":
				case "DATE":
					resultado = $" '1900-01-01' ";
					break;
				default:
					resultado = $" -1 ";
					break;
			}

			return resultado;
		}

		private string CompeltarTipoVariable(Column campo)
		{
			var resultado = "";

			var maxBites = (campo.iMaxBites > 0) ? $"{campo.iMaxBites}" : "MAX";
			switch (campo.vchDataType)
			{
				case "VARCHAR":
					resultado = $"VARCHAR ({maxBites}) ";
					break;
				case "NVARCHAR":
					resultado = $"NVARCHAR ({maxBites}) ";
					break;
				default:
					resultado = $"{campo.vchDataType}";
					break;
			}

			return resultado;
		}

		private StringBuilder AvoidLastComa(StringBuilder codigoCompleto)
		{
			string completo = codigoCompleto.ToString();
			codigoCompleto = codigoCompleto.Remove(completo.LastIndexOf(","), 1);

			return codigoCompleto;
		}


		private StringBuilder GetFirstPartSP(StringBuilder codigoCompleto, string spName, bool Params = false)
		{
			var FirstColumn = Table.Columns.FirstOrDefault();

			codigoCompleto.AppendLine($"IF OBJECT_ID( '{spName}' ) IS NULL");
			codigoCompleto.AppendLine($"\tEXEC ('CREATE PROCEDURE {spName} AS SET NOCOUNT ON;') ");
			codigoCompleto.AppendLine($"GO ");
			codigoCompleto.AppendLine($"ALTER PROCEDURE {spName} (");
			if (Params)
			{
				foreach (var item in Table.Columns)
				{
					codigoCompleto.AppendLine($"@{item.vchName} {CompeltarTipoVariable(item)}, ");

				}

				codigoCompleto = AvoidLastComa(codigoCompleto);

			}
			else
			{

				codigoCompleto.AppendLine($"@{FirstColumn.vchName} {CompeltarTipoVariable(FirstColumn)} ");
			}

			codigoCompleto.AppendLine($") ");
			codigoCompleto.AppendLine($"AS ");
			codigoCompleto.AppendLine($"BEGIN ");
			codigoCompleto.AppendLine($"");
			return codigoCompleto;
		}



		private StringBuilder CreteInsert(StringBuilder Result)
		{
			string FirstColumn = Table.Columns.FirstOrDefault().vchName;

			Result.AppendLine($"IF  ( @{FirstColumn} <= 0) ");
			Result.AppendLine($"BEGIN ");
			Result.AppendLine($"\tINSERT INTO dbo.{Table.vchName}");
			Result.AppendLine($"\t ( \n");
			int i = 1;

			var lstColumns = Table.Columns.Where(e => e.vchName != FirstColumn).ToList();
			foreach (var item in lstColumns)
			{
				Result.Append($"\t\t{item.vchName}, \t");
				if (i % 3 == 0)
				{
					Result.Append("\n");

				}
				i++;
			}
			Result = AvoidLastComa(Result);

			Result.AppendLine("");
			Result.AppendLine("\t)");
			Result.AppendLine("\tVALUES ");
			Result.AppendLine("\t(");


			int k = 1;
			foreach (var item in lstColumns)
			{

				Result.Append($"\t\t@{item.vchName},");
				if (k % 3 == 0)
				{
					Result.Append("\n");

				}
				k++;
			}
			Result = AvoidLastComa(Result);

			Result.AppendLine("");
			Result.AppendLine("\t)");

			return Result;
		}

		private StringBuilder CreateUpdate(StringBuilder Result)
		{

			string FirstColumn = Table.Columns.FirstOrDefault().vchName;

			Result.AppendLine($"\tUPDATE  dbo.{Table.vchName} WITH(ROWLOCK) SET");
			var lstColumns = Table.Columns.Where(e => e.vchName != FirstColumn);

			foreach (var item in lstColumns)
			{
				Result.Append($"\t\t {item.vchName} = @{item.vchName}, \n");
			}
			Result = AvoidLastComa(Result);
			Result.AppendLine("");
			Result.AppendLine($"\t WHERE  {FirstColumn}  = @{FirstColumn};\n");


			return Result;

		}
	}
}

