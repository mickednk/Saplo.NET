using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    internal class TextRelatedTextsRequest : RelatedTextBase
    {
        [DataMember(Name = "collection_id", IsRequired = true)]
        public int CollectionID { get; set; }

        [DataMember(Name = "text_id", IsRequired = true)]
        public int TextID { get; set; }

        [DataMember(Name = "related_by", EmitDefaultValue = false)]
        public RelationType? RelatedBy { get; set; }

        [DataMember(Name = "min_threshold", EmitDefaultValue = false)]
        public decimal? MinThreshold { get; set; }

        [DataMember(Name = "max_threshold", EmitDefaultValue = false)]
        public decimal? MaxThreshold { get; set; }

        [DataMember(Name = "ext_text_id", EmitDefaultValue = false)]
        public string ExternalTextID { get; set; }
    }
}