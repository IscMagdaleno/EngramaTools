namespace Engrama.Share.Objetos.AI_Tools
{
	public class ChatMessage
	{
		public string Role { get; set; } // "user" or "assistant"
		public string Content { get; set; }

		public ChatMessage(string role, string content)
		{
			Role = role;
			Content = content;
		}
	}
}
