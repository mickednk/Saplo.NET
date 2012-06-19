using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Saplo.Core.Responses;
using Saplo.Parsers;

namespace Saplo.Managers
{
	/// <summary>
	/// Base class used by all manager classes and implements methods for serialize/deserialize, query Saplo and validate response messages.
	/// </summary>
	public class ManagerBase
	{
		/// <summary>
		///   Deserialize the json string into supplied type.
		/// </summary>
		/// <typeparam name="T"> type to deserialize to </typeparam>
		/// <param name="jsonString"> string to deserialize </param>
		/// <returns> </returns>
		protected ResponseBase<T> Deserialize<T>(string jsonString)
		{
			ResponseBase<T> responseObj;

			var serializer = new DataContractJsonSerializer(typeof (ResponseBase<T>));
			byte[] buffer = Encoding.UTF8.GetBytes(jsonString);
			using (var memStream = new MemoryStream(buffer))
			{
				responseObj = (ResponseBase<T>) serializer.ReadObject(memStream);
			}

			return responseObj;
		}

		/// <summary>
		///   Query Saplo service with json data.
		/// </summary>
		/// <param name="json"> query to send to Saplo </param>
		/// <param name="token"> access token used for authentication </param>
		/// <returns> </returns>
		protected string QuerySaplo(string json, string token)
		{
			string outputString;
			byte[] messageBuffer = Encoding.UTF8.GetBytes(json);

			var request = (HttpWebRequest) WebRequest.Create("https://api.saplo.com/rpc/json?access_token=" + token);
			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = messageBuffer.Length;

			using (var requestStream = request.GetRequestStream())
			{
				requestStream.Write(messageBuffer, 0, messageBuffer.Length);
				requestStream.Flush();
				requestStream.Close();
			}

			using (var response = request.GetResponse() as HttpWebResponse)
			{
				using (var reader = new StreamReader(response.GetResponseStream()))
				{
					outputString = reader.ReadToEnd();
					reader.Close();
				}
			}
			return outputString;
		}

		/// <summary>
		///   Serializes supplied object into an JSON string representation.
		/// </summary>
		/// <param name="obj"> </param>
		/// <returns> </returns>
		protected string Serialize(object obj)
		{
			string resultString;
			using (var memStream = new MemoryStream())
			{
				new DataContractJsonSerializer(obj.GetType(), null, Int32.MaxValue, true, new EnumSurrogateSerializer(), false).WriteObject(memStream, obj);
				resultString = Encoding.UTF8.GetString(memStream.ToArray());
			}

			return resultString;
		}

		/// <summary>
		///   Validates response object and throws an exception if error message were received.
		/// </summary>
		/// <param name="responseObj"> object to validate </param>
		/// <returns> </returns>
		protected bool ValidateResult<T>(ResponseBase<T> responseObj)
		{
			if (responseObj.Error != null)
				responseObj.Error.ThrowAsException();
			return true;
		}
	}
}