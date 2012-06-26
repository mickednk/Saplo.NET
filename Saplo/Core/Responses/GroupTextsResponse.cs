using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    internal class GroupTextsResponse
    {
        [DataMember(Name = "texts")]
        public GroupText[] Texts { get; set; }
    }
}
