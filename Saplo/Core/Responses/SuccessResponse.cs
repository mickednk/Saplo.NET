using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    internal class SuccessResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}
