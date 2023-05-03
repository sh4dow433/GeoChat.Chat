using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoChat.Chat.Core.Repos;
public class ResponseWithPagination<TEntity> where TEntity : class, new()
{
    public IEnumerable<TEntity> Entities { get; }
    public int PageSize { get; }
    public int Page { get; }
    public int TotalNumberOfPages { get; }

    public ResponseWithPagination(IEnumerable<TEntity> entities, int pageSize, int page, int totalNumberOfPages)
    {
        Entities = entities;
        PageSize = pageSize;
        Page = page;
        TotalNumberOfPages = totalNumberOfPages;
    }
}
