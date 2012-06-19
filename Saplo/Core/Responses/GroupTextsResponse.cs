using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    public class GroupTextsResponse
    {
        [DataMember(Name = "texts")]
        public GroupTexts[] Texts { get; set; }
    }
}
