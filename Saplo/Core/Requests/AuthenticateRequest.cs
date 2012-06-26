using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    /// <summary>
    ///   Parameter container for authentication request.
    /// </summary>
    [DataContract]
    internal class AuthenicateRequest
    {
        [DataMember(Name = "api_key", IsRequired = true)]
        public string APIKey { get; set; }

        [DataMember(Name = "secret_key", IsRequired = true)]
        public string SecretKey { get; set; }
    }
}