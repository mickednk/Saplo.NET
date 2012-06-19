using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
    [DataContract]
    public class ResponseBase<T>
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "result", IsRequired = false)]
        public T Result { get; set; }

        [DataMember(Name = "error", IsRequired = false)]
        public ErrorResponse Error { get; set; }
    }
}