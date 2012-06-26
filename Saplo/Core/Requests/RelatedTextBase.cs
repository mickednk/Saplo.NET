using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    internal class RelatedTextBase
    {
        [DataMember(Name = "collection_scope", EmitDefaultValue = false)]
        public int[] CollectionScope { get; set; }

        [DataMember(Name = "wait", EmitDefaultValue = false)]
        public int? Wait { get; set; }

        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public int? Limit { get; set; }
    }
}