using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    public class GroupRelatedGroupsRequest
    {
        [DataMember(Name = "group_id")]
        public int GroupID { get; set; }

        [DataMember(Name = "group_scope", EmitDefaultValue = false)]
        public int[] GroupScope { get; set; }

        [DataMember(Name = "wait", EmitDefaultValue = false)]
        public int? Wait { get; set; }
    }
}