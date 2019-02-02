using CinemaStore.Blogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Blogic.Base
{
    public interface IBlogic<TEntity>
        where TEntity : class, new()
    {
        TEntity this[int id] { get; }
        List<TEntity> GetEntries(PaginationModel model, Func<TEntity, bool> expr = null);
        List<TEntity> Entries { get; }
        TEntity  Edit(TEntity entity);
        void Delete(TEntity entity);
        
    }
}
