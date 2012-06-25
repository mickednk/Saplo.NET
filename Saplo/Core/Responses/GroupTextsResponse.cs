using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    public class GroupTextsResponse
    {
        [DataMember(Name = "texts")]
        public GroupText[] Texts { get; set; }
    }
}
