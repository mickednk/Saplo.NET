using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    public class SuccessResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}
