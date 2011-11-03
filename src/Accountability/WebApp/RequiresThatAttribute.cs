namespace Accountability.WebApp
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Authentication;
	using FubuCore.Reflection;
	using Users;

	public class RequiresThatAttribute : Attribute
	{
		private readonly Roles[] _LimitedToRoles = new Roles[0];

		public RequiresThatAttribute(params Roles[] limitedToRoles)
		{
			_LimitedToRoles = limitedToRoles;
		}

		public virtual bool UserHasSecurityAccess()
		{
			if (!_LimitedToRoles.Any())
			{
				return true;
			}
			return _LimitedToRoles.Any(UserPrincipal.Current.HasRole);
		}

		public static bool UserCanAccess(MethodInfo action)
		{
			var requiresThat = action.GetAttribute<RequiresThatAttribute>();
			if (requiresThat != null && !requiresThat.UserHasSecurityAccess())
			{
				return false;
			}
			requiresThat = action.DeclaringType.GetAttribute<RequiresThatAttribute>();
			if (requiresThat != null && !requiresThat.UserHasSecurityAccess())
			{
				return false;
			}
			return true;
		}
	}
}