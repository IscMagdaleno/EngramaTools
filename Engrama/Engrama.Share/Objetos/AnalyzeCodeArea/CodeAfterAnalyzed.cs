using EngramaCoreStandar.Extensions;

namespace Engrama.Share.Objetos.AnalyzeCodeArea
{
	public class CodeAfterAnalyzed
	{
		public string vchTitle { get; set; }
		public string vchDescriptinos { get; set; }
		public string vchCodeImproved { get; set; }
		public string vchImprovements { get; set; }

		public CodeAfterAnalyzed()
		{
			vchTitle = string.Empty;
			vchDescriptinos = string.Empty;
			vchCodeImproved = string.Empty;
			vchImprovements = string.Empty;
		}

		public void SetData(string vchCode, string vchCodeAnalized)
		{
			if (vchCode.NotEmpty() && vchCodeAnalized.NotEmpty())
			{
				GetDescriptions(vchCodeAnalized);
				GetTitle(vchCode);
				GetCodeImproved(vchCodeAnalized);
				GetImprovements(vchCodeAnalized);
			}
		}

		private void GetDescriptions(string vchCodeAnalized)
		{
			if (string.IsNullOrEmpty(vchCodeAnalized))
			{
				vchDescriptinos = string.Empty;
				return;
			}

			var separatorIndex = vchCodeAnalized.IndexOf("```");
			if (separatorIndex <= 0)
			{
				vchDescriptinos = vchCodeAnalized.Trim();
				return;
			}

			try
			{
				vchDescriptinos = vchCodeAnalized.Substring(0, separatorIndex).Trim();
			}
			catch
			{
				vchDescriptinos = string.Empty;
			}
		}

		private void GetTitle(string vchCode)
		{
			if (string.IsNullOrEmpty(vchCode))
			{
				vchTitle = string.Empty;
				return;
			}

			var separatorIndex = vchCode.IndexOf("\n");
			if (separatorIndex < 0)
			{
				vchTitle = vchCode.Trim();
				return;
			}

			try
			{
				vchTitle = vchCode.Substring(0, separatorIndex).Trim();
			}
			catch
			{
				vchTitle = string.Empty;
			}
		}


		private void GetCodeImproved(string vchCodeAnalized)
		{
			if (string.IsNullOrEmpty(vchCodeAnalized))
			{
				vchCodeImproved = string.Empty;
				return;
			}

			var firstSeparatorIndex = vchCodeAnalized.IndexOf("```");
			var lastSeparatorIndex = vchCodeAnalized.LastIndexOf("```");

			if (firstSeparatorIndex < 0 || lastSeparatorIndex <= firstSeparatorIndex)
			{
				vchCodeImproved = string.Empty;
				return;
			}

			try
			{
				var startIndex = firstSeparatorIndex + 3; // Saltar "```"
				var contentAfterFirstSeparator = vchCodeAnalized.Substring(startIndex, lastSeparatorIndex - startIndex);

				// Buscar la primera palabra después de "```"
				var firstWordEndIndex = contentAfterFirstSeparator.IndexOfAny(new[] { ' ', '\n', '\r' });
				if (firstWordEndIndex >= 0)
				{
					// Saltar la primera palabra y cualquier espacio o salto de línea posterior
					startIndex += firstWordEndIndex;
					while (startIndex < vchCodeAnalized.Length &&
						   (vchCodeAnalized[startIndex] == ' ' ||
							vchCodeAnalized[startIndex] == '\n' ||
							vchCodeAnalized[startIndex] == '\r'))
					{
						startIndex++;
					}
					var length = lastSeparatorIndex - startIndex;
					if (length > 0)
					{
						vchCodeImproved = vchCodeAnalized.Substring(startIndex, length).Trim();
					}
					else
					{
						vchCodeImproved = string.Empty;
					}
				}
				else
				{
					// Si no hay una palabra distinta (solo espacios o nada), tomar el contenido hasta el último separador
					vchCodeImproved = contentAfterFirstSeparator.Trim();
				}
			}
			catch
			{
				vchCodeImproved = string.Empty;
			}
		}

		private void GetImprovements(string vchCodeAnalized)
		{
			if (string.IsNullOrEmpty(vchCodeAnalized))
			{
				vchImprovements = string.Empty;
				return;
			}

			var lastSeparatorIndex = vchCodeAnalized.LastIndexOf("```");
			if (lastSeparatorIndex < 0 || lastSeparatorIndex + 3 >= vchCodeAnalized.Length)
			{
				vchImprovements = string.Empty;
				return;
			}

			try
			{
				vchImprovements = vchCodeAnalized.Substring(lastSeparatorIndex + 3).Trim();
			}
			catch
			{
				vchImprovements = string.Empty;
			}
		}
	}
}