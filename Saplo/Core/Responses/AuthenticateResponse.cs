using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    internal class AuthenticateResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}