namespace Engrama.Share.PostClass.AgenteEngrama
{
	// Engrama.Share/PostModels/AgenteEngrama/PostAskAgent.cs
	namespace Engrama.Share.PostModels.AgenteEngrama
	{
		public class PostAskAgent
		{
			public string vchPrompt { get; set; }
			public string nvchUserId { get; set; }

			public PostAskAgent()
			{
				vchPrompt = string.Empty;
				nvchUserId = string.Empty;
			}
		}
	}
}
