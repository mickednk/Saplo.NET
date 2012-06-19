using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    public class AuthenticateResponse
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}