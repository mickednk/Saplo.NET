using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    internal class CollectionsResponse
    {
        [DataMember(Name = "collections")]
        public Collection[] Collections { get; set; }
    }
}
