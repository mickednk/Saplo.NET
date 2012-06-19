using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    /// <summary>
    ///   Parameter container for authentication request.
    /// </summary>
    [DataContract]
    public class AuthenicateRequest
    {
        [DataMember(Name = "api_key", IsRequired = true)]
        public string APIKey { get; set; }

        [DataMember(Name = "secret_key", IsRequired = true)]
        public string SecretKey { get; set; }
    }
}