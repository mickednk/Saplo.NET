using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    internal class TextRelatedGroupsRequest
    {
        [DataMember(Name = "collection_id")]
        public int CollectionID { get; set; }

        [DataMember(Name = "text_id")]
        public int TextID { get; set; }

        [DataMember(Name = "group_scope", EmitDefaultValue = false)]
        public int[] GroupScope { get; set; }

        [DataMember(Name = "wait", EmitDefaultValue = false)]
        public int? Wait { get; set; }

        [DataMember(Name = "limit", EmitDefaultValue = false)]
        public int? Limit { get; set; }

        [DataMember(Name = "min_threshold", EmitDefaultValue = false)]
        public decimal? MinThreshold { get; set; }

        [DataMember(Name = "max_threshold", EmitDefaultValue = false)]
        public decimal? MaxThreshold { get; set; }

        [DataMember(Name = "ext_text_id", EmitDefaultValue = false)]
        public string ExternalTextID { get; set; }
    }
}