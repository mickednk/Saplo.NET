using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    public class TagsResponse
    {
        [DataMember(Name = "tags")]
        public Tag[] Tags { get; set; }
    }
}