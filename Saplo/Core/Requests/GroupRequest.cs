using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    internal class GroupRequest
    {
        [DataMember(Name = "group_id")]
        public int GroupID { get; set; }
    }
}