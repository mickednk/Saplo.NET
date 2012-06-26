using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    internal class CollectionRequest
    {
        [DataMember(Name = "collection_id")]
        public int CollectionID { get; set; }
    }
}