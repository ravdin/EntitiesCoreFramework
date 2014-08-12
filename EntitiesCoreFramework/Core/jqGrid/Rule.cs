using System;
using System.Runtime.Serialization;

namespace EntitiesCoreFramework.jqGrid
{
    /// <summary>
    /// Encapsulates a filtering rule from jqGrid.
    /// </summary>
    [DataContract]
    public class Rule
    {
        [DataMember]
        public string field { get; set; }
        [DataMember]
        public string op { get; set; }
        [DataMember]
        public string data { get; set; }
    }
}
