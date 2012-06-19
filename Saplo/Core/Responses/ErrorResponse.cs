using System.Runtime.Serialization;
using Saplo.Core.Exceptions;

namespace Saplo.Core.Responses
{
    [DataContract]
    public class ErrorResponse
    {
        [DataMember(Name = "code")]
        public int Code { get; set; }

        [DataMember(Name = "msg")]
        public string Msg { get; set; }

        internal void ThrowAsException()
        {
            //TODO: Make more relatable exceptions than this generic.
        	throw new SaploException(Msg, Code);
        }
    }
}