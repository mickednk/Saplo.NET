using System.Runtime.Serialization;

namespace Saplo.Core.Responses
{
	[DataContract]
	internal class ListTextsResponse
	{
		[DataMember(Name = "texts")]
		public Text[] Texts { get; set; }
	}
}