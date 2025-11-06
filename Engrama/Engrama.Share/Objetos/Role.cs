namespace Engrama.Share.Objetos
{
	public class Role
	{
		public int iIdRoles { get; set; }
		public string vchName { get; set; }
		public bool bStatus { get; set; }

		public Role()
		{
			iIdRoles = -1;
			vchName = string.Empty;
			bStatus = false;
		}

		// Note: this is important so the MudSelect 
		public override bool Equals(object o)
		{
			var other = o as Role;
			return other?.vchName == vchName;
		}

		// Note: this is important too!
		public override int GetHashCode() => vchName?.GetHashCode() ?? 0;

		// Implement this for  display correctly in MudSelect
		public override string ToString() => vchName;

	}

}
