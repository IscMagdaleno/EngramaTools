using Engrama.Share.Objetos.DataBaseArea;

using EngramaCoreStandar.Extensions;

using System.Text;

namespace Engrama.API.EngramaLevels.Dominio.BusinessLogic
{
	public class FactoryCSharp
	{
		private readonly string vchName;
		private readonly List<Variable> VariablesEntrante;
		private readonly List<Variable> VariablesSalida;

		/// <summary>
		/// Constructor initializes the instance with name, input variables, and output variables.
		/// </summary>
		public FactoryCSharp(string nombre, List<Variable> variablesEntrante, List<Variable> variablesSalida)
		{
			vchName = nombre;
			VariablesEntrante = variablesEntrante;
			VariablesSalida = variablesSalida;
		}

		/// <summary>
		/// Generates Request and Response classes for database communication using Engrama.Helper
		/// </summary>
		/// <returns>A StringBuilder containing the C# class definition</returns>
		public StringBuilder GetEntityClass()
		{
			var Resultado = new StringBuilder();

			// Create main class
			Resultado.Append($"\tpublic class {vchName}\r\n{{\r\n");

			// Create Request class
			Resultado.Append("\t\tpublic class Request : SpRequest\r\n\t\t{\r\n");
			Resultado.AppendLine("\t\t\tpublic string StoredProcedure  {get=> \"" + vchName + "\";}");
			foreach (var item in VariablesEntrante)
			{
				Resultado.AppendLine($"\t\t\tpublic {item.vchCSharpType} {item.vchName} {{get; set;}}");
			}
			Resultado.Append("\t\t}\r\n");

			// Create Result class
			Resultado.Append("\t\tpublic class Result : DbResult\r\n\t\t{\r\n");
			foreach (var item in VariablesSalida)
			{
				Resultado.AppendLine($"\t\t\tpublic {item.vchCSharpType} {item.vchName} {{get; set;}}");
			}
			Resultado.Append("\t\t}\r\n");

			Resultado.Append("\t}\r\n");

			return Resultado;
		}

		/// <summary>
		/// Generates object classes to receive data from the entity class
		/// </summary>
		/// <returns>A StringBuilder containing the C# object class definitions</returns>
		public StringBuilder GetObjectClass()
		{
			var Resultado = new StringBuilder();
			var listaVariablesSalida = VariablesSalida.Where(e => e.vchName.NotAny("bResult", "vchMessage")).ToList();
			var listaVariablesEntrada = VariablesEntrante.Where(e => e.vchName.NotAny("bResult", "vchMessage")).ToList();
			// Create class for output variables
			Resultado.AppendLine("From Output Parameters");

			Resultado.AppendLine($"\tpublic class {GetNameWithoutPrefix()}  ");
			Resultado.AppendLine("\t{");
			foreach (var item in listaVariablesSalida)
			{

				Resultado.AppendLine($"\t\tpublic {item.vchCSharpType} {item.vchName} {{get; set;}}");

			}

			Resultado.AppendLine($"\t\tpublic {GetNameWithoutPrefix()}()");
			Resultado.AppendLine("\t\t{");
			foreach (var item in listaVariablesSalida)
			{
				if (item.vchCSharpType == "string")
				{

					Resultado.AppendLine($"\t\t\t{item.vchName} = string.Empty;");

				}
				else if (item.vchCSharpType.Contains("DateTime"))
				{
					Resultado.AppendLine($"\t\t\t{item.vchName} = Defaults.SqlMinDate();");

				}
			}
			Resultado.AppendLine("\t\t}");
			Resultado.AppendLine("");
			Resultado.AppendLine("\t} ");



			Resultado.AppendLine("--------------------------------------------------------------------------------------------------------");
			Resultado.AppendLine("From Input Parameters");

			// Create class for input variables
			Resultado.AppendLine($"\tpublic class {GetNameWithoutPrefix()} \t{{ ");
			foreach (var item in listaVariablesEntrada)
			{
				if (item.vchName.NotAny("bResult", "vchMessage"))
				{
					Resultado.AppendLine($"\t\tpublic {item.vchCSharpType} {item.vchName} {{get; set;}}");
				}
			}
			Resultado.AppendLine($"\t\t public {GetNameWithoutPrefix()}()");
			Resultado.AppendLine("\t\t{");
			foreach (var item in listaVariablesEntrada)
			{
				if (item.vchCSharpType == "string")
				{
					Resultado.AppendLine($"\t\t\t{item.vchName} = string.Empty;");
				}
				else if (item.vchCSharpType.Contains("DateTime"))
				{
					Resultado.AppendLine($"\t\t\t{item.vchName} = Defaults.SqlMinDate();");

				}
			}
			Resultado.AppendLine("\t\t}");
			Resultado.AppendLine("");

			Resultado.AppendLine("\t} ");

			return Resultado;
		}

		/// <summary>
		/// Generates a class for making web service calls
		/// </summary>
		/// <returns>A StringBuilder containing the C# post class definition</returns>
		public StringBuilder GetPostClass()
		{
			var Resultado = new StringBuilder();

			// Create Post class for web service
			Resultado.Append($"\tpublic class Post{GetNameWithoutsp()}\r\n\t{{\r\n");
			foreach (var item in VariablesEntrante)
			{
				if (item.vchName.NotAny("StoredProcedure", "iIdUsuario"))
				{
					Resultado.AppendLine($"\t\tpublic {item.vchCSharpType} {item.vchName} {{get; set;}}");
				}
			}
			Resultado.Append("\t}\r\n");

			return Resultado;
		}

		/// <summary>
		/// Generates a repository method to get one object
		/// </summary>
		/// <returns>A StringBuilder containing the C# method for repository layer</returns>
		public StringBuilder GetRepositoryMethod()
		{
			var Resultado = new StringBuilder();

			// Repository method for single object retrieval
			Resultado.Append($"public async Task<{vchName}.Result> {vchName}({vchName}.Request PostModel)\r\n{{\r\n");
			Resultado.Append($"\tvar result = await _managerHelper.GetAsync<{vchName}.Result, {vchName}.Request>(PostModel);\r\n");
			Resultado.Append("\tif (result.Ok)\r\n\t{\r\n\t\treturn result.Data;\r\n\t}\r\n");
			Resultado.Append("\treturn new() { bResult = false, vchMessage = $\"[{(result.Ex.NotNull() ? result.Ex.Message : \"\")}] - [{result.Msg}]\" };\r\n");
			Resultado.Append("}\r\n");

			return Resultado;
		}

		/// <summary>
		/// Generates a repository method to get a list of objects
		/// </summary>
		/// <returns>A StringBuilder containing the C# method for retrieving all objects</returns>
		public StringBuilder GetAllRepositoryMethod()
		{
			var Resultado = new StringBuilder();

			// Repository method for list retrieval
			Resultado.Append($"public async Task<IEnumerable<{vchName}.Result>> {vchName}({vchName}.Request PostModel)\r\n{{\r\n");
			Resultado.Append($"\tvar result = await _managerHelper.GetAllAsync<{vchName}.Result, {vchName}.Request>(PostModel);\r\n");
			Resultado.Append("\tif (result.Ok)\r\n\t{\r\n\t\treturn result.Data;\r\n\t}\r\n");
			Resultado.Append("\treturn new List<" + vchName + ".Result>() { new() { bResult = false, vchMessage = $\"[{(result.Ex.NotNull() ? result.Ex.Message : \"\")}] - [{result.Msg}]\" } };\r\n");
			Resultado.Append("}\r\n");

			return Resultado;
		}

		/// <summary>
		/// Generates a method used in domain layer to get a single value
		/// </summary>
		/// <returns>A StringBuilder containing the C# method for domain logic</returns>
		public StringBuilder GetDominioMethod()
		{
			// Get the first column for mapping purposes
			var firstColum = VariablesEntrante.FirstOrDefault() ?? new Variable("", "", "");

			var Resultado = new StringBuilder();

			// Domain method for single value retrieval
			Resultado.AppendLine($"public async Task<Response<{GetNameWithoutPrefix()}>> {GetNameWithoutsp()}({GetPostName()} PostModel) ");
			Resultado.AppendLine("{ ");
			Resultado.AppendLine("\ttry ");
			Resultado.AppendLine("\t{ ");
			Resultado.AppendLine($"\t\tvar model = mapperHelper.Get<{GetPostName()}, {vchName}.Request>(PostModel); ");
			Resultado.AppendLine($"\t\tvar result = await _CapaRepository.{vchName}(model); ");
			Resultado.AppendLine($"\t\tvar validation = responseHelper.Validacion<{vchName}.Result, {GetNameWithoutPrefix()}>(result); ");
			Resultado.AppendLine($"\t\tif (validation.IsSuccess)");
			Resultado.AppendLine("\t\t{ ");
			Resultado.AppendLine($"\t\t PostModel.{firstColum.vchName} = validation.Data.{firstColum.vchName};");
			Resultado.AppendLine($"\t\t\tvalidation.Data = mapperHelper.Get<{GetPostName()}, {GetNameWithoutPrefix()}>(PostModel);");
			Resultado.AppendLine("\t\t}");
			Resultado.AppendLine("\t\treturn validation;");
			Resultado.AppendLine("\t} ");
			Resultado.AppendLine("\tcatch (Exception ex) ");
			Resultado.AppendLine("\t{ ");
			Resultado.AppendLine($"\t\treturn Response<{GetNameWithoutPrefix()}>.BadResult(ex.Message, new ()); ");
			Resultado.AppendLine("\t} ");
			Resultado.AppendLine("} ");

			return Resultado;
		}

		/// <summary>
		/// Generates a method used in domain layer to get a list of values
		/// </summary>
		/// <returns>A StringBuilder containing the C# method for domain logic</returns>
		public StringBuilder GetAllDominioMethod()
		{
			var Resultado = new StringBuilder();

			// Domain method for list retrieval
			Resultado.AppendLine($"public async Task<Response<IEnumerable<{GetNameWithoutPrefix()}>>> {GetNameWithoutsp()}({GetPostName()} PostModel) ");
			Resultado.AppendLine("{ ");
			Resultado.AppendLine("\ttry ");
			Resultado.AppendLine("\t{ ");
			Resultado.AppendLine($"\t\tvar model = mapperHelper.Get<{GetPostName()}, {vchName}.Request>(PostModel); ");
			Resultado.AppendLine($"\t\tvar result = await _CapaRepository.{vchName}(model); ");
			Resultado.AppendLine($"\t\tvar validation = responseHelper.Validacion<{vchName}.Result, {GetNameWithoutPrefix()}>(result); ");
			Resultado.AppendLine($"\t\tif (validation.IsSuccess)");
			Resultado.AppendLine("\t\t{ ");
			Resultado.AppendLine($"\t\t\tvalidation.Data = validation.Data;");
			Resultado.AppendLine("\t\t}");
			Resultado.AppendLine("\t\treturn validation;");
			Resultado.AppendLine("\t} ");
			Resultado.AppendLine("\tcatch (Exception ex) ");
			Resultado.AppendLine("\t{ ");
			Resultado.AppendLine($"\t\treturn Response<IEnumerable<{GetNameWithoutPrefix()}>>.BadResult(ex.Message, new List<{GetNameWithoutPrefix()}>()); ");
			Resultado.AppendLine("\t} ");
			Resultado.AppendLine("} ");

			return Resultado;
		}

		/// <summary>
		/// Generates a method to create the endpoint in the service layer
		/// </summary>
		/// <returns>A StringBuilder containing the C# method for service layer</returns>
		public StringBuilder GetServicioMethod()
		{
			var Resultado = new StringBuilder();

			// API endpoint method for HTTP Post
			Resultado.Append($"[HttpPost(\"{GetPostName()}\")]\r\n");
			Resultado.Append($"public async Task<IActionResult> {GetPostName()}([FromBody] {GetPostName()} postModel)\r\n{{\r\n");
			Resultado.Append($"\tvar result = await _CapaDominio.{GetNameWithoutsp()}(postModel);\r\n");
			Resultado.Append("\tif (result.IsSuccess)\r\n\t{\r\n\t\treturn Ok(result);\r\n\t}\r\n");
			Resultado.Append("\treturn BadRequest(result);\r\n");
			Resultado.Append("}\r\n");

			return Resultado;
		}

		/// <summary>
		/// Generates a generic table in MudBlazor based on stored variable list
		/// </summary>
		/// <returns>A StringBuilder containing the MudBlazor table definition</returns>
		public StringBuilder GetGenericTableMudBlazor()
		{
			var Resultado = new StringBuilder();

			// MudBlazor table component definition
			Resultado.AppendLine($"<MudTable Items=\"@Data.Lst{GetNameWithoutPrefix()}\"");
			Resultado.AppendLine($"\t\t  T=\"@{GetNameWithoutPrefix()}\" ");
			Resultado.AppendLine($"\t\t  Dense=\"@true\"");
			Resultado.AppendLine($"\t\t  Hover=\"true\"");
			Resultado.AppendLine($"\t\t  Filter=\"new Func<{GetNameWithoutPrefix()},bool>(FilterFunc1)\">");
			Resultado.AppendLine($"\t<ToolBarContent>");
			Resultado.AppendLine($"\t <MudText Typo=\"Typo.h6\">Tabla de {GetNameWithoutPrefix()}</MudText>");
			Resultado.AppendLine($"\t<MudSpacer />");
			Resultado.AppendLine($"\t <MudTextField @bind-Value=\"searchStringFiltro\" Placeholder=\"Search\" Adornment=\"Adornment.Start\" AdornmentIcon=\"@Icons.Material.Filled.Search\" IconSize=\"Size.Medium\" Class=\"mt-0\"></MudTextField>");
			Resultado.AppendLine($"\t</ToolBarContent>");
			Resultado.AppendLine($"\t<HeaderContent>");

			foreach (var variable in VariablesSalida)
			{
				if (variable.vchName.NotAny("bResult", "vchMessage"))
				{
					Resultado.AppendLine($"\t<MudTh>{variable.GetNameWithoutPrefix()}</MudTh>");
				}
			}

			Resultado.AppendLine($"\t</HeaderContent>");
			Resultado.AppendLine($"\t<RowTemplate>");
			foreach (var variable in VariablesSalida)
			{
				if (variable.vchName.NotAny("bResult", "vchMessage"))
				{
					Resultado.AppendLine($"\t<MudTd DataLabel=\"{variable.vchName}\">@context.{variable.vchName}</MudTd>");
				}
			}

			Resultado.AppendLine($"\t</RowTemplate>");
			Resultado.AppendLine($"\t<PagerContent>");
			Resultado.AppendLine($"\t\t<MudTablePager />");
			Resultado.AppendLine($"\t</PagerContent>");
			Resultado.AppendLine($"\t</MudTable>");

			Resultado.AppendLine("@code {");
			Resultado.AppendLine($"\t private string searchStringFiltro = \"\";");
			Resultado.AppendLine($"\t private bool FilterFunc1({GetNameWithoutPrefix()} element) => FilterFunc(element, searchStringFiltro);");
			Resultado.AppendLine($"\t private bool FilterFunc({GetNameWithoutPrefix()} element, string searchString)");
			Resultado.AppendLine("\t\t{");
			Resultado.AppendLine("\t\t if (string.IsNullOrWhiteSpace(searchString))");
			Resultado.AppendLine("\t\t\t return true;");
			Resultado.AppendLine("\t\t if (element.vchName.Contains(searchString, StringComparison.OrdinalIgnoreCase)) // Agrega el campo por el caul realizar el filtro");
			Resultado.AppendLine("\t\t\t return true;");
			Resultado.AppendLine("\t\treturn false;");
			Resultado.AppendLine("\t\t}");
			Resultado.AppendLine("}");

			return Resultado;
		}

		/// <summary>
		/// Generates a generic form in MudBlazor based on stored variable list
		/// </summary>
		/// <returns>A StringBuilder containing the MudBlazor form definition</returns>
		public StringBuilder GetFormMudBlazor()
		{
			var Resultado = new StringBuilder();

			// Calculate middle index for layout purposes
			var iMitad = VariablesEntrante.Count() / 2;

			// MudBlazor form component definition
			Resultado.AppendLine($"<EditForm Model=\"@Data.{GetNameWithoutPrefix()}Selected\"");
			Resultado.AppendLine("OnValidSubmit=\"OnSubmint\">");
			Resultado.AppendLine("\t<DataAnnotationsValidator />\r\n");
			Resultado.AppendLine("\t<MudFocusTrap>\r\n");
			Resultado.AppendLine("<MudGrid>");
			Resultado.AppendLine("\t<MudItem sm=\"6\">");

			var i = 0;
			foreach (var item in VariablesEntrante)
			{
				Resultado.AppendLine(GeneraTextFiel(item));

				// Split into two columns
				if (i == iMitad)
				{
					Resultado.AppendLine("\t</MudItem>");
					Resultado.AppendLine("\t<MudItem sm=\"6\">");
				}
				i++;
			}

			Resultado.AppendLine("\t</MudItem>");
			Resultado.AppendLine("\t<MudItem sm=\"12\">");
			Resultado.AppendLine("\t\t<MudButton Variant=\"Variant.Filled\"");
			Resultado.AppendLine("\t\t\t\t ButtonType=\"ButtonType.Submit\"");
			Resultado.AppendLine($"\t\t\t\t  Color=\"Color.Secondary\">Save {GetNameWithoutPrefix()}</MudButton>");
			Resultado.AppendLine("\t</MudItem>");
			Resultado.AppendLine("</MudGrid>");
			Resultado.AppendLine("</MudFocusTrap>");
			Resultado.AppendLine("</EditForm>");
			Resultado.AppendLine("");
			Resultado.AppendLine("@code {");
			Resultado.AppendLine("\t ");
			Resultado.AppendLine("\t [Parameter] public MainClass Data { get; set; }");
			Resultado.AppendLine("\t [Parameter] public EventCallback<" + GetNameWithoutPrefix() + "> On" + GetNameWithoutPrefix() + "Saved {get; set; }\r\n");

			Resultado.AppendLine("\tprivate void OnSubmint()");
			Resultado.AppendLine("\t{");
			Resultado.AppendLine("\t Loading.Show();");
			Resultado.AppendLine("\t ");
			Resultado.AppendLine($"\t var result = await Data.Post{GetNameWithoutPrefix()};");
			Resultado.AppendLine("\t ShowSnake(result);");
			Resultado.AppendLine("\t if (result.bResult)");
			Resultado.AppendLine("\t {");
			Resultado.AppendLine($"\t\t await On{GetNameWithoutPrefix()}Saved.InvokeAsync(Data.{GetNameWithoutPrefix()}Selected);");
			Resultado.AppendLine("\t }");
			Resultado.AppendLine("\t Loading.Hide();");
			Resultado.AppendLine("\t ");

			Resultado.AppendLine("\t}");
			Resultado.AppendLine("}");

			return Resultado;
		}

		/// <summary>
		/// Generates a list of disabled fields from the stored variable list
		/// </summary>
		/// <returns>A StringBuilder containing the MudBlazor list of disabled text fields</returns>
		public StringBuilder GetDataListMudBlazor()
		{
			var Resultado = new StringBuilder();

			var iMitad = VariablesSalida.Count() / 2;

			// MudBlazor data list component definition
			Resultado.Append("<MudGrid>\r\n");
			Resultado.Append("\t<MudItem sm=\"6\">\r\n");

			var i = 0;
			foreach (var item in VariablesSalida)
			{
				Resultado.Append($"<MudTextFieldDisable Title=\"{item.vchName}\" Value=\"@Model.{item.vchName}\"/>\r\n");

				if (i == iMitad)
				{
					Resultado.Append("\t</MudItem>\r\n");
					Resultado.Append("\t<MudItem sm=\"6\">\r\n");
				}
				i++;
			}

			Resultado.Append("\t</MudItem>\r\n");
			Resultado.Append("</MudGrid>\r\n");

			return Resultado;
		}

		/// <summary>
		/// Generates MudBlazor code for a generic text field
		/// </summary>
		/// <param name="variable">Variable for which the field is to be generated</param>
		/// <returns>The MudBlazor text field code</returns>
		private string GeneraTextFiel(Variable variable)
		{
			var txtField = new StringBuilder();
			switch (variable.vchCSharpType)
			{
				case "int":
				case "decimal":
				case "double":
				case "short":
					txtField.Append($"\t\t<MudNumericField @bind-Value=\"Data.{GetNameWithoutPrefix()}Selected.{variable.vchName}\" \r\n");
					txtField.Append($"\t\t\t\t\t  Label=\"{variable.GetNameWithoutPrefix()}\"\r\n");
					txtField.Append($"\t\t\t\t\t  Variant=\"Variant.Outlined\" />\r\n");
					break;
				case "bool":
					txtField.Append($"\t\t<MudSwitch   @bind-Checked=\"Data.{GetNameWithoutPrefix()}Selected.{variable.vchName}\" \r\n");
					txtField.Append($"\t\t\t\t\t  Label=\"{variable.GetNameWithoutPrefix()}\"\r\n");
					txtField.Append($"\t\t\t\t\t  Variant=\"Variant.Outlined\" />\r\n");
					break;
				case "DateTime":
				case "DateTime?":
					txtField.Append($"\t\t<MudDatePicker  @bind-date=\"Data.{GetNameWithoutPrefix()}Selected.{variable.vchName}\" \r\n");
					txtField.Append($"\t\t\t\t\t  Label=\"{variable.GetNameWithoutPrefix()}\"\r\n");
					txtField.Append($"\t\t\t\t\t  Variant=\"Variant.Outlined\" />\r\n");
					break;
				default:
					txtField.Append($"\t\t<MudTextField @bind-Value=\"@Data.{GetNameWithoutPrefix()}Selected.{variable.vchName}\"\r\n");
					txtField.Append($"\t\t\t\t\t  Label=\"{variable.GetNameWithoutPrefix()}\"\r\n");
					txtField.Append($"\t\t\t\t\t  MaxLength=\"{variable.iMaxLenght}\"\r\n");
					txtField.Append($"\t\t\t\t\t  Variant=\"Variant.Outlined\" />\r\n");
					break;
			}
			txtField.AppendLine("");

			return txtField.ToString();
		}

		/// <summary>
		/// Generate the rows to create the configuracion at the program.cs file
		/// </summary>
		/// <returns>A StringBuilder containing the mapping code</returns>
		public StringBuilder BuildProgramRows()
		{
			var Resultado = new StringBuilder();

			// Mapping model to stored variable objects
			Resultado.AppendLine($"builder.Services.AddScoped<InterfaceDominio,ImplemetacionDominio>();");
			Resultado.AppendLine($"builder.Services.AddScoped<InterfaceRepository,ImplementacionRepository>();");

			return Resultado;
		}

		/// <summary>
		/// Generates code to call the API for a single object request
		/// </summary>
		/// <returns>A StringBuilder containing the C# method for API call</returns>
		public StringBuilder BuildCallToTheAPIGet()
		{
			var Resultado = new StringBuilder();

			// API call method
			Resultado.AppendLine($"public async Task<SeverityMessage> {GetPostName()}()");
			Resultado.AppendLine("{");
			Resultado.AppendLine($"\tvar APIUrl = url + \"/{GetPostName()}\";");
			Resultado.AppendLine($"\tvar model = _mapper.Get<{GetNameWithoutPrefix()}, {GetPostName()}>({GetNameWithoutPrefix()}Selected);");
			Resultado.AppendLine($"\tvar response = await _httpService.Post<{GetPostName()}, Response<{GetNameWithoutPrefix()}>>(APIUrl, model);");

			Resultado.AppendLine("\tvar validacion = _validaServicioService.ValidadionServicio(response,");
			Resultado.AppendLine($"\tonSuccess: data => Lst{GetNameWithoutPrefix()}s.Add(data));");
			Resultado.AppendLine("\treturn validacion;");
			Resultado.AppendLine("");
			Resultado.AppendLine("}");

			return Resultado;
		}

		/// <summary>
		/// Generates code to call the API for retrieving a list of objects
		/// </summary>
		/// <returns>A StringBuilder containing the C# method for API call</returns>
		public StringBuilder BuildCallToTheAPIGetAll()
		{
			var Resultado = new StringBuilder();

			// API call method for list retrieval
			Resultado.AppendLine($"public async Task<SeverityMessage> {GetPostName()}()");
			Resultado.AppendLine("{");
			Resultado.AppendLine($"\tvar APIUrl = url + \"/{GetPostName()}\";\r\n");
			Resultado.AppendLine($"\tvar model = _mapper.Get<{GetNameWithoutPrefix()}, {GetPostName()}>({GetNameWithoutPrefix()}Selected);");
			Resultado.AppendLine($"\tvar response = await _httpService.Post<{GetPostName()}, Response<List<{GetNameWithoutPrefix()}>>>(APIUrl, model);");
			Resultado.AppendLine("\tvar validacion = _validaServicioService.ValidadionServicio(response,");
			Resultado.AppendLine($"\tonSuccess: data => Lst{GetNameWithoutPrefix()}s = data);");
			Resultado.AppendLine("\treturn validacion;");
			Resultado.AppendLine("}");

			return Resultado;
		}

		/// <summary>
		/// Generates the post name for methods
		/// </summary>
		/// <returns>A string representing the post method name</returns>
		private string GetPostName()
		{
			var name = GetNameWithoutsp();
			return "Post" + name;
		}

		/// <summary>
		/// Retrieves a name without certain prefixes
		/// </summary>
		/// <returns>A string with prefixes removed</returns>
		private string GetNameWithoutPrefix()
		{
			var name = vchName;
			name = name.Replace("sp", "");
			name = name.Replace("Save", "");
			name = name.Replace("Get", "");
			name = name.Replace("Consulta", "");
			name = name.Replace("Consult", "");

			return name;
		}

		/// <summary>
		/// Retrieves a name without the "sp" prefix
		/// </summary>
		/// <returns>A string with "sp" prefix removed</returns>
		private string GetNameWithoutsp()
		{
			var name = vchName;
			name = name.Replace("sp", "");

			return name;
		}
	}
}