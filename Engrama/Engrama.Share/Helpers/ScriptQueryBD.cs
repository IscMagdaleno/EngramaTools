namespace Engrama.Share.Helpers
{
	public class ScriptQueryBD
	{
		public const string GetAllTables =
				"SELECT TA.object_id AS iIdTable, " +
				"TA.name AS vchName, " +
				"INX.rows AS iNoRows, " +
				"INX.name AS vchPrimaryKey, " +
				"CL.name AS vchColumnName , " +
				"TYPE_NAME(cl.user_type_id) AS vchDataType, " +
				"CL.max_length AS iMaxBites " +
				"FROM sys.tables TA " +
				"INNER JOIN sysindexes INX ON TA.object_id = INX.id AND INX.IndId < 2 " +
				"INNER JOIN sys.columns CL ON TA.object_id = CL.object_id " +
				"WHERE TA.name = @vchTabla OR @vchTabla = '' " +
				"ORDER BY iIdTable ";


		//public const string ConsultaRelacionesTabla =
		//	"SELECT	TA.object_id AS iIdTable, " +
		//	"TA.name AS vchName, " +
		//	"FK.name AS vchLlaveForanea, " +
		//	"COL_NAME(fc.parent_object_id, fc.parent_column_id) AS vchColumna ," +
		//	" OBJECT_NAME (fk.referenced_object_id) AS vchTablaForanea , " +
		//	"COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS vchColumnaForanea " +
		//	"FROM sys.tables TA	" +
		//	"INNER JOIN sys.foreign_keys FK ON TA.object_id = FK.parent_object_id " +
		//	"INNER JOIN sys.foreign_key_columns FC ON FK.OBJECT_ID = FC.constraint_object_id " +
		//	"WHERE TA.name = @vchTabla OR @vchTabla = '' " +
		//	"ORDER BY iIdTabla";



		public const string GetProceduresByKey = "SELECT   p.name AS vchName, " +
		"   Type_Desc   AS vchObjectType, " +
			"   create_date AS dtCreationDate " +
		"FROM  sys.procedures AS p  " +
		"JOIN  sys.sql_modules AS m ON p.object_id = m.object_id  " +
		" WHERE  m.definition LIKE '%@vchkey%';";


		public static string GetTablesbykey = "SELECT      t.name AS vchName   " +
		" FROM   " +
		"   sys.tables AS t JOIN      " +
		"sys.columns AS c ON t.object_id = c.object_id " +
		"WHERE      t.name LIKE '%@vchkey%'  " +
		"OR c.name LIKE '%@vchkey%' " +
		"group by t.name";

		//public const string ConsultaEjecutableQuery = "SELECT SO.OBJECT_ID as idEjecutableQuery,  " +
		//	"SO.name AS vchNombre    ," +
		//	"SO.Type_Desc AS vchTipoObjeto  ," +
		//	"SO.create_date AS dtFechaCreacion  ," +
		//	"P.parameter_id AS iOrden  ," +
		//	"P.name AS vchNombreParametro  ," +
		//	"P.max_length AS iMaxBite  ," +
		//	"P.is_output AS bIsOuput " +
		//	"FROM sys.objects AS SO " +
		//	"inner JOIN sys.parameters AS P ON SO.OBJECT_ID = P.OBJECT_ID " +
		//	"WHERE (SO.name   = @objname OR @objname ='' ) " +
		//	"ORDER BY  SO.name, P.parameter_id";

		public const string GetStoredProcedures = "SELECT SO.OBJECT_ID   AS idStoredProcedure," +
			"   SO.name        AS vchName, " +
			"   SO.Type_Desc   AS vchObjectType, " +
			"   SO.create_date AS dtCreationDate " +
			"	FROM sys.objects AS SO WHERE SO.Type_Desc = 'SQL_STORED_PROCEDURE' ORDER  BY SO.name ";


		public const string ConsultaCodigoCreacionTabla =
			"CREATE TABLE #tabla (Text nVARCHAR(MAX));" +
			"INSERT INTO #tabla Exec sp_ppinScriptTabla  @vchTabla ;" +
			"select * from #tabla ";
	}
}
