using System;
using System.Runtime.Serialization;

namespace Saplo.Core
{
    [DataContract]
    public class RelatedGroup : IComparable<RelatedGroup>
    {
        [DataMember(Name = "group_id")]
        public int GroupID { get; set; }

        [DataMember(Name = "relevance")]
        public decimal Relevance { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

    	public int CompareTo(RelatedGroup other)
    	{
    		return Relevance.CompareTo(other.Relevance);
    	}
    }
}