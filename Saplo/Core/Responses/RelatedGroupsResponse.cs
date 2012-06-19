using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    public class RelatedGroupsResponse
    {
        [DataMember(Name = "related_groups")]
        public RelatedGroup[] RelatedGroups { get; set; }
    }
}
