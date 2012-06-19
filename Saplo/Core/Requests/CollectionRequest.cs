using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    public class CollectionRequest
    {
        [DataMember(Name = "collection_id")]
        public int CollectionID { get; set; }
    }
}