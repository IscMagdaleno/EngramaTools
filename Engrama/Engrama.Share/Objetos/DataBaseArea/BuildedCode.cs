namespace Engrama.Share.Objetos.DataBaseArea
{
	public class BuildedCode
	{

		public string vchName { get; set; }
		public List<ScriptGenerated> Scripts { get; set; }

		public BuildedCode()
		{

		}
		public BuildedCode(string name)
		{
			vchName = name;
			Scripts = new();

		}

		public void AddNewScript(ScriptGenerated scriptGenerated)
		{
			Scripts.Add(scriptGenerated);
		}
	}
}
