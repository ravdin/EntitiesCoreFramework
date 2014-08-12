using System;
using EntitiesCoreFramework.Utilities;

namespace EntitiesCoreFramework.jqGrid
{
    public enum WhereOperation
    {
        [StringValue("eq")]
        Equal,
        [StringValue("ne")]
        NotEqual,
        [StringValue("cn")]
        Contains
    }
}
