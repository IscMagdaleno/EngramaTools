namespace Engrama.Share.Objetos.DataBaseArea
{
	public class Variable
	{
		public string vchName { get; set; }
		public string vchCSharpType { get; set; }
		public string vchSQLType { get; set; }
		public int iMaxLenght { get; set; }

		public Variable()
		{

		}
		public Variable(string nombre, string tipoCSharp, string tipoSQL)
		{
			vchName = nombre;
			vchCSharpType = tipoCSharp;
			vchSQLType = tipoSQL;
		}

		public string GetNameWithoutPrefix()
		{
			var name = vchName;
			name = name.Substring(0, 1) == "i" ? name.Replace("i", "") : name;
			name = name.Substring(0, 3) == "vch" ? name.Replace("vch", "") : name;
			name = name.Substring(0, 4) == "nvch" ? name.Replace("nvch", "") : name;
			name = name.Substring(0, 2) == "dt" ? name.Replace("dt", "") : name;
			name = name.Substring(0, 1) == "m" ? name.Replace("m", "") : name;
			name = name.Substring(0, 1) == "b" ? name.Replace("b", "") : name;
			return name;
		}
	}
}
