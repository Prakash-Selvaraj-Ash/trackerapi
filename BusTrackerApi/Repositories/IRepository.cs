using BusTrackerApi.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusTrackerApi.Repositories
{
    public interface IRepository<TDomain>
        where TDomain: class, IDomain
    {
        IQueryable<TDomain> GetAll();
        TDomain ReadById(Guid id);
        IQueryable<TDomain> ReadByIds(Guid[] ids);
        TDomain Update(TDomain domain);
        TDomain Create(TDomain domain);
        Task<TDomain[]> GetAllAsync(CancellationToken token);
        Task<EntityEntry<TDomain>> CreateAsync(TDomain domain, CancellationToken token);
        Task<TDomain> ReadByIdAsync(Guid id, CancellationToken token);
        Task<TDomain[]> ReadByIdsAsync(Guid[] ids, CancellationToken token);
        DbSet<TDomain> Set { get; }
    }
}
