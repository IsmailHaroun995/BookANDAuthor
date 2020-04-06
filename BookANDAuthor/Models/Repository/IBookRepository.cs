using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookANDAuthor.Models.Repository
{
    public interface IBookRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int Id);
        void Add(TEntity entity);
        void Update(int Id,TEntity entity);
        void Delete(int Id);
    }
}
