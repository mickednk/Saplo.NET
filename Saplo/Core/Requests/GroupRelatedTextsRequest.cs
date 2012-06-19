using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    public class GroupRelatedTextsRequest : RelatedTextBase
    {
        [DataMember(Name = "group_id")]
        public int GroupID { get; set; }
    }
}