using System.Runtime.Serialization;

namespace Saplo.Core.Requests
{
    [DataContract]
    internal class CollectionCreateRequest
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }
    }
}