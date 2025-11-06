namespace Engrama.Share.PostClass
{
	public class PostValidateCredentials
	{
		public string vchNickName { get; set; }
		public string vchPassword { get; set; }

		public PostValidateCredentials()
		{
			vchNickName = string.Empty;
			vchPassword = string.Empty;
		}
	}
}
