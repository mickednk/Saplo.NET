namespace Saplo.Managers
{
	/// <summary>
	///   Delegate used to get accesstoken from top manager.
	/// </summary>
	/// <returns> </returns>
	public delegate string PerformAuthentication();

	/// <summary>
	/// Base class used by managers that need help with authentication.
	/// </summary>
	public class SectionManagerBase : ManagerBase
	{
		/// <summary>
		///   Delegate to authentication method.
		/// </summary>
		protected PerformAuthentication authenticate;

		public SectionManagerBase(PerformAuthentication authenticateDelegate)
		{
			authenticate = authenticateDelegate;
		}

		/// <summary>
		///   Performs serialization/deserialization and query against Saplo API.
		/// </summary>
		/// <typeparam name="T"> the responsetype we are expecting </typeparam>
		/// <param name="requestObject"> the request object </param>
		/// <returns> </returns>
		protected T CallSaploApi<T>(object requestObject) where T : class
		{
			var result = QuerySaplo(Serialize(requestObject), authenticate());
			var responseObj = Deserialize<T>(result);
			return ValidateResult(responseObj) ? responseObj.Result : null;
		}
	}
}