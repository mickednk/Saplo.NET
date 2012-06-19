using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    public class GroupCreateRequest
    {
        [DataMember(Name = "group_id", IsRequired = false, EmitDefaultValue = false)]
        public int? GroupID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}