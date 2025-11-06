using System.ComponentModel.DataAnnotations;

namespace Engrama.Share.Objetos
{
	public class User
	{
		public int iIdUsers { get; set; }


		[Required(ErrorMessage = "The Name field is required.")]
		public string vchName { get; set; }

		[Required(ErrorMessage = "The Email field is required.")]
		[EmailAddress]
		public string vchEmail { get; set; }

		[Required(ErrorMessage = "The Password field is required.")]
		public string vchPass { get; set; }

		[Required(ErrorMessage = "The Password field is required.")]
		[Compare(nameof(vchPass), ErrorMessage = "The Passwords do not match")]
		public string Password2 { get; set; }


		public bool bStatus { get; set; }

		public string vchNickName { get; set; }

		public Role Rol { get; set; }

		public User()
		{
			iIdUsers = -1;
			vchName = string.Empty;
			vchEmail = string.Empty;
			vchPass = string.Empty;
			bStatus = false;

			Rol = new Role();

		}


		// Note: this is important so the MudSelect 
		public override bool Equals(object o)
		{
			var other = o as User;
			return other?.vchName == vchName;
		}

		// Note: this is important too!
		public override int GetHashCode() => vchName?.GetHashCode() ?? 0;

		// Implement this for  display correctly in MudSelect
		public override string ToString() => "[" + iIdUsers + "] - " + vchName;
	}

}
