using System.Globalization;
using System.Runtime.Serialization;
using Saplo.Utils;

namespace Saplo.Core.Requests
{
	/// <summary>
	///   Base class for all types of request against Saplo.
	/// </summary>
	/// <typeparam name="T"> Parameter class to use for request </typeparam>
	[DataContract]
	public class RequestBase<T>
	{
		public RequestBase()
		{
			ID = Helper.GetNextId().ToString(CultureInfo.InvariantCulture);
		}

		public RequestBase(string method, T @params) : this()
		{
			Method = method;
			Parameters = @params;
		}

		[DataMember(Name = "id")]
		public string ID { get; private set; }

		[DataMember(Name = "method")]
		public string Method { get; set; }

		[DataMember(Name = "params")]
		public T Parameters { get; set; }
	}
}