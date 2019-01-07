using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplication.Repository
{
    public interface IDataRepository<TEntity, U> where TEntity : class
    {
        IEnumerable<TEntity> ViewAll();
        Guid Create(TEntity b);
        Guid Edit(U id, TEntity b);
        TEntity View(U id);
        Guid Delete(U id);
    }

}
