using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Saplo.Core
{
	[DataContract]
	public class Account
	{
		[DataMember(Name = "api_calls")]
		public APICallInformation APICalls { get; set; }

		[DataMember(Name = "account_id")]
		public int AccountID { get; set; }

		[DataMember(Name = "expiration_date")]
		private string ExpiresString { get; set; }

		public DateTime? Expires
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(ExpiresString, null, DateTimeStyles.RoundtripKind, out date))
					return date;
				return null;
			}
		}

		[DataMember(Name = "collections")]
		public LimitLeft Collections { get; set; }

		[DataMember(Name = "groups")]
		public LimitLeft Groups { get; set; }
	}

	[DataContract]
	public class APICallInformation
	{
		[DataMember(Name = "left_month")]
		public int LeftMonth { get; set; }

		[DataMember(Name = "left_hour")]
		public int LeftHour { get; set; }

		[DataMember(Name = "limit_month")]
		public int LimitMonth { get; set; }

		[DataMember(Name = "limit_hour")]
		public int LimitHour { get; set; }

		[DataMember(Name = "reset_month")]
		private string ResetMonthString { get; set; }

		public DateTime? ResetMonth
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(ResetMonthString, null, DateTimeStyles.RoundtripKind, out date))
					return date;
				return null;
			}
		}

		[DataMember(Name = "reset_hour")]
		private string ResetHourString { get; set; }

		public DateTime? ResetHour
		{
			get
			{
				DateTime date;
				if (DateTime.TryParse(ResetHourString, null, DateTimeStyles.RoundtripKind, out date))
					return date;
				return null;
			}
		}

		[DataMember(Name = "left_batch")]
		public int BatchCallsLeft { get; set; }
	}

	[DataContract]
	public class LimitLeft
	{
		[DataMember(Name = "limit")]
		public int Limit { get; set; }

		[DataMember(Name = "left")]
		public int Left { get; set; }
	}
}