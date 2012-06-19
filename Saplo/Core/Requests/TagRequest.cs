using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    public class TagRequest
    {
        [DataMember(Name = "collection_id")]
        public int CollectionID { get; set; }

        [DataMember(Name = "text_id")]
        public int TextID { get; set; }

        [DataMember(Name = "wait", EmitDefaultValue = false)]
        public int? Wait { get; set; }

        [DataMember(Name = "skip_categorization", EmitDefaultValue = false)]
        public bool? SkipCategorization { get; set; }

        [DataMember(Name = "ext_text_id", EmitDefaultValue = false)]
        public string ExternalTextID { get; set; }
    }
}