using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    internal class GroupsResponse
    {
        [DataMember(Name="groups")]
        public Group[] Groups { get; set; }
    }
}
