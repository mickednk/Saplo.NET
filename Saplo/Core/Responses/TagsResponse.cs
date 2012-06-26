using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    internal class TagsResponse
    {
        [DataMember(Name = "tags")]
        public Tag[] Tags { get; set; }
    }
}