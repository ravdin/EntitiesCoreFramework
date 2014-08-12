using System;
using System.Collections.Generic;

namespace EntitiesCoreFramework.Utilities
{
    public interface IPagedList<T> : IList<T>
    {
        int PageNumber { get; }
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
