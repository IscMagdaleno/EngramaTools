// Engrama.Share/Objects/AgenteEngrama/ChatMessage.cs
namespace Engrama.Share.Objetos.AgenteEngrama
{
	public class ChatMessage
	{
		public string Role { get; set; } // "user" or "assistant"
		public string Content { get; set; }

		public ChatMessage()
		{
			Role = string.Empty;
			Content = string.Empty;
		}
	}
}