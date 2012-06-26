using System;
using System.Net;
using Saplo.Core;
using Saplo.Core.Requests;
using Saplo.Core.Responses;
using Saplo.Managers;

namespace Saplo
{
	/// <summary>
	///   Manager used to execute operations against Saplo´s API.
	/// </summary>
	public partial class SaploManager : ManagerBase
	{
		/// <summary>
		///   API key used to authenticate request.
		/// </summary>
		private readonly string ApiKey;

		/// <summary>
		///   Secret key used to authenticate request.
		/// </summary>
		private readonly string SecretKey;

		/// <summary>
		///   The interval that the accesstoken should be refreshed in.
		/// </summary>
		private readonly TimeSpan TokenRefreshInterval = new TimeSpan(0, 30, 0);

		/// <summary>
		///   Creates new instance of SaploManager.
		/// </summary>
		/// <param name="apikey"> public api key </param>
		/// <param name="secretkey"> secret api key </param>
		public SaploManager(string apikey, string secretkey) : this(apikey, secretkey, null)
		{
		}

		/// <summary>
		///   Creates new instance of SaploManager.
		/// </summary>
		/// <param name="apikey"> public api key </param>
		/// <param name="secretkey"> secret api key </param>
		/// <param name="proxy"> proxy to use when communicating with the api </param>
		public SaploManager(string apikey, string secretkey, IWebProxy proxy) : base(proxy)
		{
			ApiKey = apikey;
			SecretKey = secretkey;

			AuthenticateIfNeeded();

			//init managers.
			Collections = new CollectionManager(AuthenticateIfNeeded, proxy);
			Groups = new GroupManager(AuthenticateIfNeeded);
			Texts = new TextManager(AuthenticateIfNeeded, proxy);
		}

		public CollectionManager Collections { get; set; }
		public GroupManager Groups { get; set; }
		public TextManager Texts { get; set; }

		/// <summary>
		///   Current accesstoken.
		/// </summary>
		private string AccessToken { get; set; }

		/// <summary>
		///   Last time accesstoken were refreshed.
		/// </summary>
		private DateTime LastTokenRefresh { get; set; }

		/// <summary>
		///   Authenticate for access token.
		/// </summary>
		/// <param name="api_key"> api key </param>
		/// <param name="secret_key"> secret key </param>
		/// <returns> </returns>
		public string Authenticate(string api_key, string secret_key)
		{
			var requestObj = new RequestBase<AuthenicateRequest>
			                 {
			                 	Method = "auth.accessToken",
			                 	Parameters = new AuthenicateRequest
			                 	             {
			                 	             	APIKey = api_key,
			                 	             	SecretKey = secret_key
			                 	             }
			                 };
			string resultString = QuerySaplo(Serialize(requestObj), null);
			var response = Deserialize<AuthenticateResponse>(resultString);

			return ValidateResult(response) ? response.Result.AccessToken : null;
		}

		/// <summary>
		///   Returns account information for authenticated account.
		/// </summary>
		/// <returns> </returns>
		public Account GetAccount()
		{
			var reqObj = new RequestBase<object>("account.get", new object());

			string result = QuerySaplo(Serialize(reqObj), AuthenticateIfNeeded());
			var responseObj = Deserialize<Account>(result);
			return ValidateResult(responseObj) ? responseObj.Result : null;
		}

		/// <summary>
		///   Invalidates token.
		/// </summary>
		/// <returns> </returns>
		public bool InvalidateToken()
		{
			var requestObj = new RequestBase<object>("auth.invalidateToken", new object());

			string result = QuerySaplo(Serialize(requestObj), AuthenticateIfNeeded());
			var responseObj = Deserialize<SuccessResponse>(result);

			return ValidateResult(responseObj) && responseObj.Result.Success;
		}

		/// <summary>
		///   Authenticates for a new accesstoken if needed.
		/// </summary>
		/// <returns> the resulting accesstoken </returns>
		private string AuthenticateIfNeeded()
		{
			if (LastTokenRefresh < DateTime.Now.Subtract(TokenRefreshInterval))
			{
				AccessToken = Authenticate(ApiKey, SecretKey);
				if (!string.IsNullOrEmpty(AccessToken))
					LastTokenRefresh = DateTime.Now;
			}

			return AccessToken;
		}
	}
}