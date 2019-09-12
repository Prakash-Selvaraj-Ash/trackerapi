using BusTrackerApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusTrackerApi.Services
{
    public interface IBaseCrudService<TDomain>
        where TDomain: class, IDomain, IWithId
    {
        IQueryable<TDomain> ReadAll();
        TDomain Create(TDomain domain);
        TDomain Update(TDomain domain);
        TDomain ReadById(Guid id);
        void DeleteAll();
        IQueryable<TDomain> ReadByIds(Guid[] ids);
        Task<TDomain[]> ReadAllAsync(CancellationToken token);
        Task<TDomain> CreateAsync(TDomain domain, CancellationToken token);
        Task<TDomain> ReadByIdAsync(Guid id, CancellationToken token);
        Task<TDomain[]> ReadByIdsAsync(Guid[] ids, CancellationToken token);
    }
}
