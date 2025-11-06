using Engrama.Share.Entity.DataBase;
using Engrama.Share.Objetos.DataBaseArea;

using System.Text;

namespace Engrama.API.EngramaLevels.Dominio.BusinessLogic
{
	public class FactoryStoredProcedure
	{
		public DetailsStoreProcedure GetDetailsStoredProcedure(IEnumerable<sp_helptext.Result> request, string name)
		{

			var Code = new StringBuilder();
			var lstCode = new List<string>();

			foreach (var item in request)
			{
				Code.Append(item.Text);
				lstCode.Add(item.Text);
			}

			//Obtengo sus parametros de salida ytentrada
			var inputParameter = GetInputParameters(Code.ToString());
			var outputParameter = GetOutputParameters(Code.ToString());

			//obtengo todas las lista de codigops generados
			var listaCodes = GetCSharpClass(name, inputParameter, outputParameter);

			//Creo el objeto que voy a devolver al cliente
			var nuevoSP = new DetailsStoreProcedure()
			{
				Code = Code.ToString(),
				LstCodes = lstCode,
				InputParameters = inputParameter,
				OutputParameters = outputParameter,
				BuildedCodes = listaCodes,
			};

			return nuevoSP;

		}

		private List<Variable> GetInputParameters(string codigo)
		{

			var result = new List<Variable>();

			var inicioVariables = codigo.IndexOf("(") + 1;
			var FinVariables = codigo.IndexOf("AS");

			if (inicioVariables > 0 && FinVariables > inicioVariables)
			{

				var subCadenaVariabels = codigo.Substring(inicioVariables, FinVariables - inicioVariables);
				result = DesgloseVariable.GeneraListaVariables_SQLCode(subCadenaVariabels);
			}



			return result;

		}

		private List<Variable> GetOutputParameters(string codigo)
		{



			var PosRespuesta = codigo.IndexOf("Result");
			if (PosRespuesta == -1) { return new List<Variable>(); }

			var inicioVariables = codigo.IndexOf("(", PosRespuesta) + 1;

			var FinVariables = codigo.IndexOf(");", PosRespuesta);
			if (FinVariables == -1) FinVariables = codigo.IndexOf(")", PosRespuesta);


			var subCadenaVariabels = codigo.Substring(inicioVariables, FinVariables - inicioVariables);
			var list = DesgloseVariable.GeneraListaVariables_SQLCode(subCadenaVariabels);


			return list;

		}

		private List<BuildedCode> GetCSharpClass(string name, List<Variable> variablesEntrante, List<Variable> variablesSalida)
		{
			var BuildedCodes = new List<BuildedCode>();

			var generaVariables = new FactoryCSharp(name, variablesEntrante, variablesSalida);

			/*CSharpClass*/

			var csharpClass = new BuildedCode("C# Class");
			csharpClass.AddNewScript(new ScriptGenerated("Entity", generaVariables.GetEntityClass().ToString()));
			csharpClass.AddNewScript(new ScriptGenerated("Object", generaVariables.GetObjectClass().ToString()));
			csharpClass.AddNewScript(new ScriptGenerated("Post", generaVariables.GetPostClass().ToString()));
			BuildedCodes.Add(csharpClass);


			var repositoryMethod = new BuildedCode("Repository");
			repositoryMethod.AddNewScript(new ScriptGenerated("Get", generaVariables.GetRepositoryMethod().ToString()));
			repositoryMethod.AddNewScript(new ScriptGenerated("Get All", generaVariables.GetAllRepositoryMethod().ToString()));
			BuildedCodes.Add(repositoryMethod);

			var dominioMethod = new BuildedCode("Dominio");
			dominioMethod.AddNewScript(new ScriptGenerated("Get", generaVariables.GetDominioMethod().ToString()));
			dominioMethod.AddNewScript(new ScriptGenerated("Get All", generaVariables.GetAllDominioMethod().ToString()));
			BuildedCodes.Add(dominioMethod);


			var servidioMethod = new BuildedCode("Service");
			servidioMethod.AddNewScript(new ScriptGenerated("EndPoint", generaVariables.GetServicioMethod().ToString()));
			BuildedCodes.Add(servidioMethod);

			var clientSide = new BuildedCode("Client Side");
			clientSide.AddNewScript(new ScriptGenerated("Call API GET", generaVariables.BuildCallToTheAPIGet().ToString()));
			clientSide.AddNewScript(new ScriptGenerated("Call API GET ALL", generaVariables.BuildCallToTheAPIGetAll().ToString()));
			BuildedCodes.Add(clientSide);

			var mudBlazor = new BuildedCode("MudBlazor");
			mudBlazor.AddNewScript(new ScriptGenerated("Generic Table", generaVariables.GetGenericTableMudBlazor().ToString()));
			mudBlazor.AddNewScript(new ScriptGenerated("Form", generaVariables.GetFormMudBlazor().ToString()));
			mudBlazor.AddNewScript(new ScriptGenerated("DataList", generaVariables.GetDataListMudBlazor().ToString()));
			BuildedCodes.Add(mudBlazor);

			var configurations = new BuildedCode("Config");
			configurations.AddNewScript(new ScriptGenerated("Program.cs", generaVariables.BuildProgramRows().ToString()));
			BuildedCodes.Add(configurations);




			return BuildedCodes;
		}
	}
}

