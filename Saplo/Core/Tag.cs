using System;
using System.Runtime.Serialization;

namespace Saplo.Core
{
	[DataContract]
	public class Tag : IComparable<Tag>
	{
		[DataMember(Name = "tag")]
		public string Name { get; set; }

		[DataMember(Name = "category")]
		private string CategoryString { get; set; }

		public TagCategoryType Category
		{
			get
			{
				TagCategoryType tempCat;
				Enum.TryParse(CategoryString, true, out tempCat);
				return tempCat;
			}
			set { CategoryString = value.ToString(); }
		}

		[DataMember(Name = "relevance")]
		public double Relevance { get; set; }

		public int CompareTo(Tag other)
		{
			return Relevance.CompareTo(other.Relevance);
		}
	}
}