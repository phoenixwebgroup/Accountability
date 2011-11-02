namespace Accountability.Authentication
{
	public interface IOpenIdUserService
	{
		void CreatePendingUserIfDoesNotExist(UserInformation userInformation);
	}
}