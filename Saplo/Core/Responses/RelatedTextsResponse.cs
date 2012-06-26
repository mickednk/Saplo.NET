using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    internal class RelatedTextsResponse
    {
        [DataMember(Name = "related_texts")]
        public RelatedText[] RelatedTexts { get; set; }
    }
}