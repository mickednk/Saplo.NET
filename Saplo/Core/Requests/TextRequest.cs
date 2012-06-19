using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    public class TextRequest
    {
        [DataMember(Name = "collection_id")]
        public int CollectionID { get; set; }

        [DataMember(Name = "text_id")]
        public int TextID { get; set; }

        [DataMember(Name = "ext_text_id")]
        public string ExternalTextID { get; set; }
    }
}