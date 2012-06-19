using System;
using System.Runtime.Serialization;

namespace Saplo.Core.Exceptions
{
	/// <summary>
	/// Exception specific to errors sent from Saplo.
	/// </summary>
    [DataContract]
    public class SaploException : Exception
    {
        public SaploException(string message, int code)
            : base(message)
        {
            Code = code;
        }

    	/// <summary>
    	/// Errorcode provided by Saplo API-
    	/// <see>
		///   <cref>http://developer.saplo.com/error_codes</cref>
    	/// </see>
    	/// </summary>
    	public int Code { get; set; }
    }
}